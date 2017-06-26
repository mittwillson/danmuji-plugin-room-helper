using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoomHelper
{
    public partial class Settings : Form
    {
        public BindingString GiftMessage { get; set; } = "感谢 {user} 送的 {gift}";
        public BindingString SubcibeMessage { get; set; } = "感谢 {user} 的关注";
        public Settings()
        {
            InitializeComponent();
            // thanksBindingList.Add(new ThanksData.Thanks(ThanksData.ThanksType.gift, "123123", true));
            this.giftThankMessage.DataBindings.Add("Text", this.GiftMessage, "Value", false,
                DataSourceUpdateMode.OnPropertyChanged);
            this.subcibeThankMessage.DataBindings.Add("Text", this.SubcibeMessage, "Value", false, 
                DataSourceUpdateMode.OnPropertyChanged);
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void Settings_Load(object sender, EventArgs e)
        {
        }

        private void giftThankMessage_TextChanged(object sender, EventArgs e)
        {
        }
    }

    public class BindingString : INotifyPropertyChanged
    {
        private string _value;
        public string Value 
        {
            get => _value;
            set
            {
                _value = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("Value"));
            }
        }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void InvokePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            handler?.Invoke(this, e);
        }

        public BindingString(string value)
        {
            this._value = value;
        }
        
        public static implicit operator BindingString(string v)
        {
            return new BindingString(v);
        }

        #endregion
    }
}
