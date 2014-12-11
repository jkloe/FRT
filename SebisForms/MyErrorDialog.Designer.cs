namespace SebisControls
{
    partial class MyErrorDialog
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
            this.errorLog = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // errorLog
            // 
            this.errorLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorLog.Location = new System.Drawing.Point(0, 0);
            this.errorLog.Multiline = true;
            this.errorLog.Name = "errorLog";
            this.errorLog.ReadOnly = true;
            this.errorLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.errorLog.Size = new System.Drawing.Size(330, 302);
            this.errorLog.TabIndex = 3;
            // 
            // MyErrorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 302);
            this.Controls.Add(this.errorLog);
            this.Name = "MyErrorDialog";
            this.Text = "MyErrorDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox errorLog;
    }
}