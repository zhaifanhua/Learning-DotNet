using Microsoft.VisualBasic;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using ZhaiFanhuaDemo.Tools.ClassLibrary;

namespace ZhaiFanhuaDemo.Tools.WordProcessing
{
    public partial class MainForm_WordProcessing : Form
    {
        private MainForm _mainForm;

        public MainForm_WordProcessing(MainForm mainform)
        {
            InitializeComponent();
            InitRichTextBoxContextMenu(richTextBox_in);
            InitRichTextBoxContextMenu(richTextBox_out);
            _mainForm = mainform;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        // RichTextBox实现鼠标右键(剪切，复制，粘贴)功能
        private static void InitRichTextBoxContextMenu(RichTextBox textBox)
        {
            // 创建全选子菜单
            var selectAllMenuItem = new System.Windows.Forms.MenuItem("全选");
            selectAllMenuItem.Click += (sender, eventArgs) => textBox.SelectAll();
            // 创建剪切子菜单
            var cutMenuItem = new System.Windows.Forms.MenuItem("剪切");
            cutMenuItem.Click += (sender, eventArgs) => textBox.Cut();
            // 创建复制子菜单
            var copyMenuItem = new System.Windows.Forms.MenuItem("复制");
            copyMenuItem.Click += (sender, eventArgs) => textBox.Copy();
            // 创建粘贴子菜单
            var pasteMenuItem = new System.Windows.Forms.MenuItem("粘贴");
            pasteMenuItem.Click += (sender, eventArgs) => textBox.Paste();
            // 创建右键菜单并将子菜单加入到右键菜单中
            var contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add(selectAllMenuItem);
            contextMenu.MenuItems.Add(cutMenuItem);
            contextMenu.MenuItems.Add(copyMenuItem);
            contextMenu.MenuItems.Add(pasteMenuItem);
            textBox.ContextMenu = contextMenu;
        }

        // 简体转换繁体
        private void button_jian_fan_Click(object sender, EventArgs e)
        {
            groupBox_out.Text = "转换后的繁体为";
            _mainForm.label_task.Text = this.Text + "，简体转换繁体";
            Transformation("Traditional");
        }

        // 繁体转换简体
        private void button_fan_jian_Click(object sender, EventArgs e)
        {
            groupBox_out.Text = "转换后的简体为";
            _mainForm.label_task.Text = this.Text + "，繁体转换简体";
            Transformation("Simplified");
        }

        // 转换方法
        private void Transformation(string type)
        {
            // 输出框重置
            richTextBox_out.Text = "";
            // 输入框内容
            string str = richTextBox_in.Text.Trim();
            StringBuilder sb = new StringBuilder();
            Thread thread = new Thread(new ThreadStart(() =>
            {
                ProgressHelper progressHelper = new ProgressHelper(_mainForm.label_progress, _mainForm.progressbar_progress, str.Length, 0);
                // 遍历每个字符
                for (int i = 0; i < str.Length; i++)
                {
                    progressHelper.Processing(i + 1);
                    // 三种方法判断是否为汉字 汉字的ASCII码大于127
                    if ((int)str[i] > 127 && (str[i] >= 0x4e00 && str[i] <= 0x9fbb) && (Regex.IsMatch(str[i].ToString(), @"[\u4e00-\u9fbb]")))
                    {
                        // 判断转换类型
                        switch (type)
                        {
                            case "Traditional":
                                // 把简体字转换成繁体字
                                sb.Append(Strings.StrConv(str[i].ToString(), VbStrConv.TraditionalChinese, 0));
                                break;

                            case "Simplified":
                                // 把繁体字转换成简体字
                                sb.Append(Strings.StrConv(str[i].ToString(), VbStrConv.SimplifiedChinese, 0));
                                break;
                        }
                    }
                    else
                    {
                        // 拼接字符
                        sb.Append(str[i]);
                    }
                }
                richTextBox_out.Text = sb.ToString();
            }));
            thread.Start();
        }

        // 文本对比
        private void btn_duibifenxi_Click(object sender, EventArgs e)
        {
            groupBox_out.Text = "文本对比";
            _mainForm.label_task.Text = this.Text + "，文本对比";
            string text_old = richTextBox_in.Text.Trim();
            string text_new = richTextBox_out.Text.Trim();
            DiffTextClass diffTextClass = new DiffTextClass();
            DiffTextClass.Item [] lalal = diffTextClass.DiffText(text_old, text_new);
            MessageBox.Show(lalal.Length.ToString());
            foreach (var Item in lalal)
            {
                MessageBox.Show(Item.StartA.ToString());
                MessageBox.Show(Item.StartB.ToString());
                MessageBox.Show(Item.deletedA.ToString());
                MessageBox.Show(Item.insertedB.ToString());
            }
        }
    }
}