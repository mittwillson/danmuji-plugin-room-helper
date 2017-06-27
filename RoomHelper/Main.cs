using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LoginCenter.API;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using BilibiliDM_PluginFramework;
using Newtonsoft.Json.Linq;


namespace RoomHelper
{
    public class Main : BilibiliDM_PluginFramework.DMPlugin
    {
        private readonly Settings _settingsForm = new Settings();

        public string GiftMessage
        {
            get => (this._settingsForm.GiftMessage ?? "").Value;
            set => this._settingsForm.GiftMessage = value;
        }

        public string SubcibeMessage
        {
            get => (this._settingsForm.SubcibeMessage ?? "").Value;
            set => this._settingsForm.SubcibeMessage = value;
        }

        private bool _listenNewFans = false;

        public bool ListenNewFans
        {
            get => this._listenNewFans;
            set
            {
                if (value && this._enabled)
                {
                    Task.Run(() => StartListen());
                }

                if (!value && this._runListenThread != null && this._runListenThread.IsAlive)
                {
                    Task.Run(() => StopListen());
                }
            }
        }

        private bool _enabled = false;

        private int _masterId = 0;
        private List<int> _lastUidList = new List<int>();
        private readonly List<int> _newFansList = new List<int>();
        private int _lastAddTime = 0;
        private Thread _runListenThread = null;
        public Main()
        {
            this.Connected += Main_Connected;
            this.Disconnected += Main_Disconnected;
            this.ReceivedDanmaku += Main_ReceivedDanmaku;
            this.ReceivedRoomCount += Main_ReceivedRoomCount;
            this.PluginAuth = "拓海真一";
            this.PluginName = "直播间助手";
            this.PluginCont = "MittWillson@icloud.com";
            this.PluginVer = "0.0.1";
            this.PluginDesc = "可能用于替代房管助手部分功能的插件";
            this.ListenNewFans = this._enabled = base.Status;
        }

        private void Main_ReceivedRoomCount(object sender, BilibiliDM_PluginFramework.ReceivedRoomCountArgs e)
        {

        }

        private void Main_ReceivedDanmaku(object sender, BilibiliDM_PluginFramework.ReceivedDanmakuArgs e)
        {
            var danmu = e.Danmaku;

            if (danmu.MsgType != BilibiliDM_PluginFramework.MsgTypeEnum.GiftSend) return;

            var tmpDir = new Dictionary<string, string>()
            {
                {"user", danmu.UserName},
                {"gift", danmu.GiftName},
                {"giftCount", danmu.GiftCount.ToString()}
            };
            SendDanmu(FormatMessage(this.GiftMessage, tmpDir));
        }

        private void Main_Disconnected(object sender, BilibiliDM_PluginFramework.DisconnectEvtArgs e)
        {
            this.ListenNewFans = false;
        }

        private void Main_Connected(object sender, BilibiliDM_PluginFramework.ConnectedEvtArgs e)
        {
            this.ListenNewFans = true;
        }

        protected bool StartListen()
        {
            if (this.ListenNewFans) return true;
            if (!CheckAuthorization()) return false;
            this._listenNewFans = true;
            try
            {
                if (!RoomId.HasValue)
                {
                    throw new Exception("没有房间号信息");
                }
                var liveInfoString = LoginCenterAPI.getLiveInfo(RoomId.Value);
                var liveInfoJObject = JObject.Parse(liveInfoString);
                _masterId = liveInfoJObject?["data"]["MASTERID"].ToObject<int>() ?? throw new Exception("None liveinfo");
                this._runListenThread = new Thread(new ThreadStart(_StartListenNewFans));
                this._runListenThread.Start();
                return true;
            }
            catch (Exception exception)
            {
                AddDM("获取主播信息错误，请查看日志");
                Log("获取主播信息错误，此插件将不会工作，请重新连接直播间。\r\n错误信息：" + exception.Message);
            }

            this._listenNewFans = false;
            return false;
        }

        protected bool StopListen()
        {
            if (!this.ListenNewFans) return true;
            this._listenNewFans = false;
            try
            {
                this._runListenThread.Abort();
                this._lastUidList.Clear();
                
            }
            catch (Exception)
            {
                // ignored
            }

            return true;
        }

        private void _StartListenNewFans()
        {
            var errorCount = 0;
            while (true)
            {
                Thread.Sleep(1000);
                if (!this.ListenNewFans || !this._enabled)
                {
                    break;
                }

                if (_masterId == 0)
                {
                    continue;
                }

                JObject fansListJObject;

                try
                {
                    fansListJObject = JObject.Parse(LoginCenterAPI.httpGet(@"http://space.bilibili.com/ajax/friend/GetFansList?page=1&mid=" + _masterId));
                    errorCount = 0;
                }
                catch (Exception exception)
                {
                    if (errorCount > 3)
                    {
                        base.Stop();
                        AddDM("插件错误，请查看日志");
                        Log("超过三次出现网络错误，已自动禁用插件，错误信息：" + exception.Message);
                        break;
                    }
                    errorCount++;
                    continue;
                }

                try
                {
                    if (fansListJObject["status"].ToObject<bool>() && ((JArray) fansListJObject["data"]["list"]).Count > 0 )
                    {
                        var fansList = fansListJObject["data"]["list"].OrderBy((token => token["addtime"]));
                        var newFansCount = 0;
                        var refreshList = false;

                        if (_lastUidList.Count > 0)
                        {
                            foreach (var fan in fansList)
                            {
                                var userId = fan["fid"].ToObject<int>();
                                var addtime = fan["addtime"].ToObject<int>();

                                // 条件判断不存在上次列表
                                if (_lastUidList.Find((uid => userId == uid)) != 0) continue;
                                // 可能有人取消关注了
                                if (_newFansList.Find((uid) => uid == userId) != 0) continue;
                                if (_lastAddTime >= addtime)
                                {
                                    refreshList = true;
                                    continue;
                                }
                                
                                // 添加新粉丝
                                _newFansList.Add(userId);

                                // 更新最后关注时间
                                _lastAddTime = addtime;

                                 // New Fan
                                 var fanName = fan["uname"].ToObject<string>();
                                this.SendDanmu(this.FormatMessage(this.SubcibeMessage, new Dictionary<string, string>()
                                {
                                    { "user", fanName }
                                }));
                                newFansCount++;
                            }
                        }
                        
                        // 避免刷关注
                        if (newFansCount > 0 || _lastUidList.Count == 0 || refreshList == true)
                        {
                            // 填充新粉丝列表
                            _lastUidList = fansList.Select((token => token["fid"].ToObject<int>())).ToList();
                            if (fansList.Any())
                            {
                                _lastAddTime = fansList.ElementAt(0)["addtime"].ToObject<int>();
                            }
                            refreshList = false;
                        }
                    }
                }
                catch(Exception exception)
                {
                    AddDM("插件错误，请查看日志");
                    Log("已自动禁用插件，错误信息：" + exception.Message);
                }
                
                // Listen New Fans
                // http://space.bilibili.com/ajax/friend/GetFansList?mid=9215725&page=1&_=1498407406333
                
            }
            this._listenNewFans = false;
        }

        protected static void CatchPluginError(Action function)
        {
            try
            {
                function.Invoke();
            }
            catch (LoginCenter.API.PluginNotAuthorizedException)
            {
                MessageBox.Show("需要先授权\"登录中心\"访问权限", "错误");
            }
        }

        public override void Admin()
        {
            base.Admin();
            Task.Run(() =>
            {
                if (!CheckAuthorization()) return;
                _settingsForm.ShowDialog();
            });
        }

        protected bool CheckAuthorization()
        {
            if (!LoginCenterAPI.checkAuthorization())
            {
                if (LoginCenterAPI.doAuthorization(this).Result != AuthorizationResult.Success)
                {
                    MessageBox.Show("需要授权\"登录中心\"访问权限才能继续");
                    return false;
                }
            }
            return true;
        }

        public string FormatMessage(string message, Dictionary<string, string> args)
        {
            var formatedMessage = message;
            foreach(var arg in args)
            {
                var name = arg.Key;
                var value = arg.Value;
                var argName = "{" + name + "}";
                var foundArg = formatedMessage.IndexOf(argName, StringComparison.Ordinal) != -1;
                if(foundArg)
                {
                    formatedMessage = formatedMessage.Replace(argName, value);
                }
            }
            return formatedMessage;
        }

        public void SendDanmu(string message)
        {
            if(this.RoomId.HasValue)
            {
                LoginCenterAPI.sendMessage(this.RoomId.Value, message);
            } else
            {
                Log("尚未连接房间");
            }
           
        }
        
        public override void Stop()
        {
            base.Stop();
            this.ListenNewFans = this._enabled = false;
        }

        public override void Start()
        {
            base.Start();
            this.ListenNewFans = this._enabled = true;

        }
    }
}