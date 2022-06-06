
namespace ZhaiFanhuaDemo.Tools.WordProcessing
{
    partial class MainForm_WordProcessing
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm_WordProcessing));
            this.richTextBox_in = new System.Windows.Forms.RichTextBox();
            this.richTextBox_out = new System.Windows.Forms.RichTextBox();
            this.groupBox_in = new System.Windows.Forms.GroupBox();
            this.groupBox_out = new System.Windows.Forms.GroupBox();
            this.button_jian_fan = new System.Windows.Forms.Button();
            this.button_fan_jian = new System.Windows.Forms.Button();
            this.groupBox_control = new System.Windows.Forms.GroupBox();
            this.btn_duibifenxi = new System.Windows.Forms.Button();
            this.groupBox_in.SuspendLayout();
            this.groupBox_out.SuspendLayout();
            this.groupBox_control.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBox_in
            // 
            this.richTextBox_in.AutoWordSelection = true;
            this.richTextBox_in.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_in.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_in.EnableAutoDragDrop = true;
            this.richTextBox_in.Location = new System.Drawing.Point(3, 17);
            this.richTextBox_in.Name = "richTextBox_in";
            this.richTextBox_in.Size = new System.Drawing.Size(582, 546);
            this.richTextBox_in.TabIndex = 0;
            this.richTextBox_in.Text = "";
            // 
            // richTextBox_out
            // 
            this.richTextBox_out.AutoWordSelection = true;
            this.richTextBox_out.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_out.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox_out.EnableAutoDragDrop = true;
            this.richTextBox_out.Location = new System.Drawing.Point(3, 17);
            this.richTextBox_out.Name = "richTextBox_out";
            this.richTextBox_out.Size = new System.Drawing.Size(590, 546);
            this.richTextBox_out.TabIndex = 1;
            this.richTextBox_out.Text = "";
            // 
            // groupBox_in
            // 
            this.groupBox_in.Controls.Add(this.richTextBox_in);
            this.groupBox_in.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox_in.Location = new System.Drawing.Point(0, 0);
            this.groupBox_in.Name = "groupBox_in";
            this.groupBox_in.Size = new System.Drawing.Size(588, 566);
            this.groupBox_in.TabIndex = 3;
            this.groupBox_in.TabStop = false;
            this.groupBox_in.Text = "请输入要转换的中文";
            // 
            // groupBox_out
            // 
            this.groupBox_out.Controls.Add(this.richTextBox_out);
            this.groupBox_out.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox_out.Location = new System.Drawing.Point(638, 0);
            this.groupBox_out.Name = "groupBox_out";
            this.groupBox_out.Size = new System.Drawing.Size(596, 566);
            this.groupBox_out.TabIndex = 4;
            this.groupBox_out.TabStop = false;
            this.groupBox_out.Text = "转换后的结果";
            // 
            // button_jian_fan
            // 
            this.button_jian_fan.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_jian_fan.BackgroundImage")));
            this.button_jian_fan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_jian_fan.Location = new System.Drawing.Point(6, 20);
            this.button_jian_fan.Name = "button_jian_fan";
            this.button_jian_fan.Size = new System.Drawing.Size(40, 40);
            this.button_jian_fan.TabIndex = 5;
            this.button_jian_fan.UseVisualStyleBackColor = true;
            this.button_jian_fan.Click += new System.EventHandler(this.button_jian_fan_Click);
            // 
            // button_fan_jian
            // 
            this.button_fan_jian.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button_fan_jian.BackgroundImage")));
            this.button_fan_jian.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button_fan_jian.Location = new System.Drawing.Point(6, 66);
            this.button_fan_jian.Name = "button_fan_jian";
            this.button_fan_jian.Size = new System.Drawing.Size(40, 40);
            this.button_fan_jian.TabIndex = 6;
            this.button_fan_jian.UseVisualStyleBackColor = true;
            this.button_fan_jian.Click += new System.EventHandler(this.button_fan_jian_Click);
            // 
            // groupBox_control
            // 
            this.groupBox_control.Controls.Add(this.btn_duibifenxi);
            this.groupBox_control.Controls.Add(this.button_jian_fan);
            this.groupBox_control.Controls.Add(this.button_fan_jian);
            this.groupBox_control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox_control.Location = new System.Drawing.Point(588, 0);
            this.groupBox_control.Name = "groupBox_control";
            this.groupBox_control.Size = new System.Drawing.Size(50, 566);
            this.groupBox_control.TabIndex = 7;
            this.groupBox_control.TabStop = false;
            this.groupBox_control.Text = "操作";
            // 
            // btn_duibifenxi
            // 
            this.btn_duibifenxi.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_duibifenxi.BackgroundImage")));
            this.btn_duibifenxi.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_duibifenxi.Location = new System.Drawing.Point(6, 112);
            this.btn_duibifenxi.Name = "btn_duibifenxi";
            this.btn_duibifenxi.Size = new System.Drawing.Size(40, 40);
            this.btn_duibifenxi.TabIndex = 7;
            this.btn_duibifenxi.UseVisualStyleBackColor = true;
            this.btn_duibifenxi.Click += new System.EventHandler(this.btn_duibifenxi_Click);
            // 
            // MainForm_WordProcessing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1234, 566);
            this.Controls.Add(this.groupBox_control);
            this.Controls.Add(this.groupBox_out);
            this.Controls.Add(this.groupBox_in);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm_WordProcessing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "文字处理工具";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox_in.ResumeLayout(false);
            this.groupBox_out.ResumeLayout(false);
            this.groupBox_control.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox_in;
        private System.Windows.Forms.RichTextBox richTextBox_out;
        private System.Windows.Forms.GroupBox groupBox_in;
        private System.Windows.Forms.GroupBox groupBox_out;
        private System.Windows.Forms.Button button_jian_fan;
        private System.Windows.Forms.Button button_fan_jian;
        private System.Windows.Forms.GroupBox groupBox_control;
        private System.Windows.Forms.Button btn_duibifenxi;
    }
}