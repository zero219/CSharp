
namespace MultithreadingTwoColorBall
{
    partial class TwoColorBall
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gb_Area = new System.Windows.Forms.GroupBox();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.btn_Start = new System.Windows.Forms.Button();
            this.l_Blue_1 = new System.Windows.Forms.Label();
            this.l_Red_6 = new System.Windows.Forms.Label();
            this.l_Red_5 = new System.Windows.Forms.Label();
            this.l_Red_4 = new System.Windows.Forms.Label();
            this.l_Red_3 = new System.Windows.Forms.Label();
            this.l_Red_2 = new System.Windows.Forms.Label();
            this.l_Red_1 = new System.Windows.Forms.Label();
            this.gb_Area.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_Area
            // 
            this.gb_Area.Controls.Add(this.btn_Stop);
            this.gb_Area.Controls.Add(this.btn_Start);
            this.gb_Area.Controls.Add(this.l_Blue_1);
            this.gb_Area.Controls.Add(this.l_Red_6);
            this.gb_Area.Controls.Add(this.l_Red_5);
            this.gb_Area.Controls.Add(this.l_Red_4);
            this.gb_Area.Controls.Add(this.l_Red_3);
            this.gb_Area.Controls.Add(this.l_Red_2);
            this.gb_Area.Controls.Add(this.l_Red_1);
            this.gb_Area.Location = new System.Drawing.Point(121, 78);
            this.gb_Area.Name = "gb_Area";
            this.gb_Area.Size = new System.Drawing.Size(562, 272);
            this.gb_Area.TabIndex = 0;
            this.gb_Area.TabStop = false;
            this.gb_Area.Text = "双色球区域";
            // 
            // btn_Stop
            // 
            this.btn_Stop.Location = new System.Drawing.Point(269, 162);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(75, 23);
            this.btn_Stop.TabIndex = 8;
            this.btn_Stop.Text = "停止";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(115, 162);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(75, 23);
            this.btn_Start.TabIndex = 7;
            this.btn_Start.Text = "开始";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // l_Blue_1
            // 
            this.l_Blue_1.AutoSize = true;
            this.l_Blue_1.ForeColor = System.Drawing.Color.Blue;
            this.l_Blue_1.Location = new System.Drawing.Point(396, 96);
            this.l_Blue_1.Name = "l_Blue_1";
            this.l_Blue_1.Size = new System.Drawing.Size(22, 17);
            this.l_Blue_1.TabIndex = 6;
            this.l_Blue_1.Text = "00";
            // 
            // l_Red_6
            // 
            this.l_Red_6.AutoSize = true;
            this.l_Red_6.ForeColor = System.Drawing.Color.Red;
            this.l_Red_6.Location = new System.Drawing.Point(322, 96);
            this.l_Red_6.Name = "l_Red_6";
            this.l_Red_6.Size = new System.Drawing.Size(22, 17);
            this.l_Red_6.TabIndex = 5;
            this.l_Red_6.Text = "00";
            // 
            // l_Red_5
            // 
            this.l_Red_5.AutoSize = true;
            this.l_Red_5.ForeColor = System.Drawing.Color.Red;
            this.l_Red_5.Location = new System.Drawing.Point(262, 96);
            this.l_Red_5.Name = "l_Red_5";
            this.l_Red_5.Size = new System.Drawing.Size(22, 17);
            this.l_Red_5.TabIndex = 4;
            this.l_Red_5.Text = "00";
            // 
            // l_Red_4
            // 
            this.l_Red_4.AutoSize = true;
            this.l_Red_4.ForeColor = System.Drawing.Color.Red;
            this.l_Red_4.Location = new System.Drawing.Point(207, 96);
            this.l_Red_4.Name = "l_Red_4";
            this.l_Red_4.Size = new System.Drawing.Size(22, 17);
            this.l_Red_4.TabIndex = 3;
            this.l_Red_4.Text = "00";
            // 
            // l_Red_3
            // 
            this.l_Red_3.AutoSize = true;
            this.l_Red_3.ForeColor = System.Drawing.Color.Red;
            this.l_Red_3.Location = new System.Drawing.Point(148, 96);
            this.l_Red_3.Name = "l_Red_3";
            this.l_Red_3.Size = new System.Drawing.Size(22, 17);
            this.l_Red_3.TabIndex = 2;
            this.l_Red_3.Text = "00";
            // 
            // l_Red_2
            // 
            this.l_Red_2.AutoSize = true;
            this.l_Red_2.ForeColor = System.Drawing.Color.Red;
            this.l_Red_2.Location = new System.Drawing.Point(94, 96);
            this.l_Red_2.Name = "l_Red_2";
            this.l_Red_2.Size = new System.Drawing.Size(22, 17);
            this.l_Red_2.TabIndex = 1;
            this.l_Red_2.Text = "00";
            // 
            // l_Red_1
            // 
            this.l_Red_1.AutoSize = true;
            this.l_Red_1.ForeColor = System.Drawing.Color.Red;
            this.l_Red_1.Location = new System.Drawing.Point(35, 96);
            this.l_Red_1.Name = "l_Red_1";
            this.l_Red_1.Size = new System.Drawing.Size(22, 17);
            this.l_Red_1.TabIndex = 0;
            this.l_Red_1.Text = "00";
            // 
            // TwoColorBall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gb_Area);
            this.Name = "TwoColorBall";
            this.Text = "双色球";
            this.gb_Area.ResumeLayout(false);
            this.gb_Area.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_Area;
        private System.Windows.Forms.Label l_Blue_1;
        private System.Windows.Forms.Label l_Red_6;
        private System.Windows.Forms.Label l_Red_5;
        private System.Windows.Forms.Label l_Red_4;
        private System.Windows.Forms.Label l_Red_3;
        private System.Windows.Forms.Label l_Red_2;
        private System.Windows.Forms.Label l_Red_1;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_Stop;
    }
}

