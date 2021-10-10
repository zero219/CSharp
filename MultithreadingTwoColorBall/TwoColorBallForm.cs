using MultithreadingTwoColorBall.Helpers;
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

namespace MultithreadingTwoColorBall
{
    public partial class TwoColorBall : Form
    {
        /// <summary>
        /// 蓝球集合
        /// </summary>
        private string[] BlueNums =
        {
            "01","02","03","04","05","06","07","08","09","10",
            "11","12","13","14","15","16"
        };

        /// <summary>
        /// 红球集合
        /// </summary>
        private string[] RedNums =
        {
            "01","02","03","04","05","06","07","08","09","10",
            "11","12","13","14","15","16","17","18","19","20",
            "21","22","23","24","25","26","27","28","29","30",
            "31","32","33"
        };

        private bool IsGoOn = true;//判断字段
        public TwoColorBall()
        {
            InitializeComponent();
            this.btn_Start.Enabled = true;
            this.btn_Stop.Enabled = false;
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            try
            {
                this.IsGoOn = true;
                //初始化
                this.btn_Start.Enabled = false;
                this.l_Red_1.Text = "00";
                this.l_Red_2.Text = "00";
                this.l_Red_3.Text = "00";
                this.l_Red_4.Text = "00";
                this.l_Red_5.Text = "00";
                this.l_Red_6.Text = "00";
                this.l_Blue_1.Text = "00";

                Thread.Sleep(2000);
                TaskFactory taskFactory = new TaskFactory();
                List<Task> tasks = new List<Task>();
                foreach (Control c in this.gb_Area.Controls)
                {
                    if (c is Label)
                    {
                        Label lbl = (Label)c;
                        tasks.Add(taskFactory.StartNew(() =>
                        {
                            while (this.IsGoOn)
                            {
                                this.ProcessLabel(lbl);
                            }
                            Console.WriteLine("结束当前循环");
                        }));
                    }
                }
                this.btn_Stop.Enabled = true;//正确的时机打开
                taskFactory.ContinueWhenAll(tasks.ToArray(), tList => this.MessageShow());
            }
            catch (Exception ex)
            {
                Console.WriteLine("双色球启动出现异常：{0}", ex.Message);
            }
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            this.IsGoOn = false;
            this.btn_Start.Enabled = true;
            this.btn_Stop.Enabled = false;
        }
        private static object updateLock = new object();
        private void ProcessLabel(Label label)
        {
            //蓝球
            if (label.Name.Contains("blue"))
            {
                int index = new RandomHelper().GetNumber(0, 16);
                string text = this.BlueNums[index];
                UpdateLabel(label, text);
            }
            else
            {
                int index = new RandomHelper().GetNumber(0, 33);
                string text = this.RedNums[index];
                lock (updateLock)
                {
                    List<string> list = this.GetRedNums();
                    if (list.Contains(text))//如果重复
                    {
                        return;//放弃
                    }
                    this.UpdateLabel(label, text);
                }
            }
        }

        /// <summary>
        /// 获取当前红球界面上的球号码
        /// </summary>
        /// <returns></returns>
        private List<string> GetRedNums()
        {
            List<string> list = new List<string>();
            foreach (Control c in this.gb_Area.Controls)
            {
                if (c is Label && ((Label)c).Name.Contains("red"))
                {
                    list.Add(((Label)c).Text);
                }
            }
            return list;
        }
        /// <summary>
        /// 更新界面
        /// </summary>
        /// <param name="label"></param>
        /// <param name="text"></param>
        private void UpdateLabel(Label label, string text)
        {
            if (label.InvokeRequired)
            {
                this.Invoke(new Action(() =>
                {
                    label.Text = text;
                }));
            }
            else
            {
                label.Text = text;
            }
        }
        /// <summary>
        /// 打印结果
        /// </summary>
        private void MessageShow()
        {
            MessageBox.Show(string.Format("本期双色球结果是 {0} {1} {2} {3} {4} {5}  {6}",
                   l_Red_1.Text, l_Red_2.Text, l_Red_3.Text, l_Red_4.Text, l_Red_5.Text, l_Red_6.Text, l_Blue_1.Text));
        }
    }
}
