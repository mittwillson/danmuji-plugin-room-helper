namespace RoomHelper
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.enableHelper = new System.Windows.Forms.CheckBox();
            this.subcibeThankMessage = new System.Windows.Forms.TextBox();
            this.mainBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.giftThankMessage = new System.Windows.Forms.TextBox();
            this.settingsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingsBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.enableHelper);
            this.groupBox1.Controls.Add(this.subcibeThankMessage);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.giftThankMessage);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(742, 615);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "礼物关注感谢";
            // 
            // enableHelper
            // 
            this.enableHelper.AutoSize = true;
            this.enableHelper.Checked = true;
            this.enableHelper.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableHelper.Location = new System.Drawing.Point(39, 546);
            this.enableHelper.Name = "enableHelper";
            this.enableHelper.Size = new System.Drawing.Size(210, 28);
            this.enableHelper.TabIndex = 4;
            this.enableHelper.Text = "开启直播间助手";
            this.enableHelper.UseVisualStyleBackColor = true;
            // 
            // subcibeThankMessage
            // 
            this.subcibeThankMessage.Location = new System.Drawing.Point(39, 366);
            this.subcibeThankMessage.Multiline = true;
            this.subcibeThankMessage.Name = "subcibeThankMessage";
            this.subcibeThankMessage.Size = new System.Drawing.Size(658, 132);
            this.subcibeThankMessage.TabIndex = 3;
            // 
            // mainBindingSource
            // 
            this.mainBindingSource.DataSource = typeof(RoomHelper.Main);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(33, 308);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 33);
            this.label2.TabIndex = 2;
            this.label2.Text = "关注";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(33, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 33);
            this.label1.TabIndex = 1;
            this.label1.Text = "礼物";
            // 
            // giftThankMessage
            // 
            this.giftThankMessage.Location = new System.Drawing.Point(39, 130);
            this.giftThankMessage.Multiline = true;
            this.giftThankMessage.Name = "giftThankMessage";
            this.giftThankMessage.Size = new System.Drawing.Size(658, 120);
            this.giftThankMessage.TabIndex = 0;
            this.giftThankMessage.TextChanged += new System.EventHandler(this.giftThankMessage_TextChanged);
            // 
            // settingsBindingSource
            // 
            this.settingsBindingSource.DataSource = typeof(RoomHelper.Settings);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 639);
            this.Controls.Add(this.groupBox1);
            this.Name = "Settings";
            this.Text = "直播间助手-设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.Load += new System.EventHandler(this.Settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.settingsBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox giftThankMessage;
        private System.Windows.Forms.TextBox subcibeThankMessage;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox enableHelper;
        private System.Windows.Forms.BindingSource mainBindingSource;
        private System.Windows.Forms.BindingSource settingsBindingSource;
    }
}