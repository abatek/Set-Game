namespace SetServer
{
    partial class MainForm
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
            this.lbReceive = new System.Windows.Forms.ListBox();
            this.txtSend = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtConnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbReceive
            // 
            this.lbReceive.FormattingEnabled = true;
            this.lbReceive.Location = new System.Drawing.Point(12, 51);
            this.lbReceive.Name = "lbReceive";
            this.lbReceive.Size = new System.Drawing.Size(105, 173);
            this.lbReceive.TabIndex = 5;
            // 
            // txtSend
            // 
            this.txtSend.Location = new System.Drawing.Point(93, 14);
            this.txtSend.Name = "txtSend";
            this.txtSend.Size = new System.Drawing.Size(100, 20);
            this.txtSend.TabIndex = 4;
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(12, 12);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtConnect
            // 
            this.txtConnect.Location = new System.Drawing.Point(123, 51);
            this.txtConnect.Name = "txtConnect";
            this.txtConnect.Size = new System.Drawing.Size(75, 23);
            this.txtConnect.TabIndex = 6;
            this.txtConnect.Text = "Connect";
            this.txtConnect.UseVisualStyleBackColor = true;
            this.txtConnect.Click += new System.EventHandler(this.txtConnect_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(216, 238);
            this.Controls.Add(this.txtConnect);
            this.Controls.Add(this.lbReceive);
            this.Controls.Add(this.txtSend);
            this.Controls.Add(this.btnSend);
            this.Name = "MainForm";
            this.Text = "Server";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListBox lbReceive;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button txtConnect;
        public System.Windows.Forms.TextBox txtSend;
    }
}

