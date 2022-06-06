using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZhaiFanhuaDemo.Tools.ClassLibrary;
using ZhaiFanhuaDemo.Tools.WordProcessing;

namespace ZhaiFanhuaDemo.Tools
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            System.Windows.Forms.Timer timer_time = new System.Windows.Forms.Timer();
            timer_time.Interval = 1000;
            timer_time.Tick += new System.EventHandler((obj, ev) => timer_time_Tick(obj, ev));
            timer_time.Enabled = true;
        }
        // 更新当前时间
        private void timer_time_Tick(object sender, EventArgs e)
        {
            label_time.Text = DateTime.Now.ToString();
        }

        private void ToolStripMenuItem_WordProcessing_Click(object sender, EventArgs e)
        {
            MainForm_WordProcessing mainForm_WordProcessing = new MainForm_WordProcessing(this);
            mainForm_WordProcessing.TopLevel = false;
            mainForm_WordProcessing.Parent = panel_content;
            mainForm_WordProcessing.Show();
        }
    }
}
