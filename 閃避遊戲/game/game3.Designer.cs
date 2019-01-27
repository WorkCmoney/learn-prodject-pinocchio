namespace 閃避遊戲
{
    partial class game_3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(game_3));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Endtime = new System.Windows.Forms.Timer(this.components);
            this.stoppic = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.stoppic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 85;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Endtime
            // 
            this.Endtime.Tick += new System.EventHandler(this.Endtime_Tick);
            // 
            // stoppic
            // 
            this.stoppic.Image = ((System.Drawing.Image)(resources.GetObject("stoppic.Image")));
            this.stoppic.Location = new System.Drawing.Point(1180, 28);
            this.stoppic.Name = "stoppic";
            this.stoppic.Size = new System.Drawing.Size(22, 22);
            this.stoppic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.stoppic.TabIndex = 7;
            this.stoppic.TabStop = false;
            this.stoppic.Click += new System.EventHandler(this.Stop_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1220, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(22, 22);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // game_3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1280, 1024);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.stoppic);
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "game_3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "game_3";
            this.Load += new System.EventHandler(this.Game_3_Load_1);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Game_3_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Game_3_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Game_3_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.stoppic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer Endtime;
        private System.Windows.Forms.PictureBox stoppic;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}