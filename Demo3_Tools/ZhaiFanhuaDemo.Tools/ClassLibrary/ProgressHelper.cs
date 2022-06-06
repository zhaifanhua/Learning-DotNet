using System;
using System.Windows.Forms;

namespace ZhaiFanhuaDemo.Tools.ClassLibrary
{
    public class ProgressHelper
    {
        #region 字段
        private Label _progressLable;
        private ProgressBar _progressBar;
        private int _sumStep=100;
        private int _processingStep=0;
        #endregion

        #region 属性
        /// <summary>
        /// 绑定Label控件
        /// </summary>
        public Label Label
        {
            get { return _progressLable; }
            set { _progressLable = value; }
        }

        /// <summary>
        /// 绑定ProgressBar控件
        /// </summary>
        public ProgressBar ProgressBar
        {
            get { return _progressBar; }
            set { _progressBar = value; }
        }

        /// <summary>
        /// 总步数
        /// </summary>
        public int SumStep
        {
            get { return _sumStep; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("总步数只能为大于0的整数。");
                }
                else
                {
                    _sumStep = value;
                }
            }
        }

        /// <summary>
        /// 当前步数
        /// </summary>
        public int ProcessingStep
        {
            get { return _processingStep; }
            set { _processingStep = value; }
        }
        #endregion

        #region 公有方法
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="sunStep">总步数</param>
        /// <param name="processingStep">当前步数</param>
        /// <param name="lable">Label控件</param>
        /// <param name="progressBar">ProgressBar控件</param>
        public ProgressHelper(Label lable, ProgressBar progressBar, int sunStep = 100, int processingStep = 0)
        {
            Label = lable;
            ProgressBar = progressBar;
            SumStep = sunStep;
            ProcessingStep = processingStep;
            ProgressBar.Maximum = SumStep;
            Processing(ProcessingStep);
        }

        /// <summary>
        /// 刷新当前正在处理步数
        /// </summary>
        /// <param name="processingStep">当前步数</param>
        public void Processing(int processingStep) {
            ProcessingStep = processingStep;
            ChangeControl();
        }
        #endregion

        #region 私有方法
        private delegate void SetPercentageInfo();
        private delegate void SetProcessingStep();

        /// <summary>
        /// 更新控件占比及步数
        /// </summary>
        private void ChangeControl()
        {
            if (Label.InvokeRequired)
                Label.Invoke(new SetPercentageInfo(ChangeControl));
            else
                Label.Text = (Convert.ToDouble(ProcessingStep) / Convert.ToDouble(SumStep)).ToString("P");
            if (ProgressBar.InvokeRequired)
                ProgressBar.Invoke(new SetProcessingStep(ChangeControl));
            else
                ProgressBar.Value = ProcessingStep;
        }
        #endregion
    }
}
