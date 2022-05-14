namespace HistoryMap
{
    partial class SelectDynasty
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
            this.btPRC = new System.Windows.Forms.Button();
            this.btDongHan = new System.Windows.Forms.Button();
            this.btQin = new System.Windows.Forms.Button();
            this.btXiHan = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // btPRC
            // 
            this.btPRC.Location = new System.Drawing.Point(12, 10);
            this.btPRC.Name = "btPRC";
            this.btPRC.Size = new System.Drawing.Size(214, 123);
            this.btPRC.TabIndex = 0;
            this.btPRC.Text = "中华人民共和国";
            this.toolTip1.SetToolTip(this.btPRC, "中华人民共和国");
            this.btPRC.UseVisualStyleBackColor = true;
            this.btPRC.Click += new System.EventHandler(this.btXiHan_Click);
            // 
            // btDongHan
            // 
            this.btDongHan.Location = new System.Drawing.Point(230, 140);
            this.btDongHan.Name = "btDongHan";
            this.btDongHan.Size = new System.Drawing.Size(214, 123);
            this.btDongHan.TabIndex = 1;
            this.btDongHan.Text = "东汉";
            this.toolTip1.SetToolTip(this.btDongHan, "东汉");
            this.btDongHan.UseVisualStyleBackColor = true;
            this.btDongHan.Click += new System.EventHandler(this.btXiHan_Click);
            // 
            // btQin
            // 
            this.btQin.Location = new System.Drawing.Point(230, 10);
            this.btQin.Name = "btQin";
            this.btQin.Size = new System.Drawing.Size(214, 123);
            this.btQin.TabIndex = 2;
            this.btQin.Text = "秦";
            this.toolTip1.SetToolTip(this.btQin, "秦");
            this.btQin.UseVisualStyleBackColor = true;
            this.btQin.Click += new System.EventHandler(this.btXiHan_Click);
            // 
            // btXiHan
            // 
            this.btXiHan.Location = new System.Drawing.Point(12, 140);
            this.btXiHan.Name = "btXiHan";
            this.btXiHan.Size = new System.Drawing.Size(214, 123);
            this.btXiHan.TabIndex = 3;
            this.btXiHan.Text = "西汉";
            this.toolTip1.SetToolTip(this.btXiHan, "西汉");
            this.btXiHan.UseVisualStyleBackColor = true;
            this.btXiHan.Click += new System.EventHandler(this.btXiHan_Click);
            // 
            // SelectDynasty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 273);
            this.Controls.Add(this.btXiHan);
            this.Controls.Add(this.btQin);
            this.Controls.Add(this.btDongHan);
            this.Controls.Add(this.btPRC);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectDynasty";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择朝代";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btPRC;
        private System.Windows.Forms.Button btDongHan;
        private System.Windows.Forms.Button btQin;
        private System.Windows.Forms.Button btXiHan;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}