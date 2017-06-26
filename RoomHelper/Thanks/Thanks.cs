using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace RoomHelper.ThanksData
{
    public enum ThanksType
    {
        /// <summary>
        /// 礼物
        /// </summary>
        Gift = 0,
        /// <summary>
        /// 关注
        /// </summary>
        Subscibe = 1
    }

    public class Thanks
    {
        public int Id;
        public ThanksType Type;
        public string Message;
        public bool Enabled;
        public Thanks(ThanksType _type, string _message, bool _enabled)
        {
            Type = _type;
            Message = _message;
            Enabled = _enabled;
        }
    }
}
