namespace SebisControls {
    partial class MyImageViewer {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.imagePanel = new System.Windows.Forms.Panel();
            this.currentImageViewer = new System.Windows.Forms.PictureBox();
            this.selectedImageLabel = new System.Windows.Forms.Label();
            this.fitHeightButton = new System.Windows.Forms.Button();
            this.fitWidthButton = new System.Windows.Forms.Button();
            this.loupeCb = new System.Windows.Forms.CheckBox();
            this.origSizeButton = new System.Windows.Forms.Button();
            this.zoomInButton = new System.Windows.Forms.Button();
            this.fit2PageButton = new System.Windows.Forms.Button();
            this.zoomOutButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.imagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentImageViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // imagePanel
            // 
            this.imagePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.imagePanel.AutoScroll = true;
            this.imagePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imagePanel.Controls.Add(this.currentImageViewer);
            this.imagePanel.Location = new System.Drawing.Point(0, 48);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.Size = new System.Drawing.Size(751, 372);
            this.imagePanel.TabIndex = 77;
            // 
            // currentImageViewer
            // 
            this.currentImageViewer.BackColor = System.Drawing.SystemColors.Control;
            this.currentImageViewer.Location = new System.Drawing.Point(-1, -1);
            this.currentImageViewer.Name = "currentImageViewer";
            this.currentImageViewer.Size = new System.Drawing.Size(100, 50);
            this.currentImageViewer.TabIndex = 0;
            this.currentImageViewer.TabStop = false;
            this.currentImageViewer.Paint += new System.Windows.Forms.PaintEventHandler(this.currentImageViewer_Paint);
            this.currentImageViewer.MouseDown += new System.Windows.Forms.MouseEventHandler(this.currentImageViewer_MouseDown);
            this.currentImageViewer.MouseEnter += new System.EventHandler(this.currentImageViewer_MouseEnter);
            this.currentImageViewer.MouseLeave += new System.EventHandler(this.currentImageViewer_MouseLeave);
            this.currentImageViewer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.currentImageViewer_MouseMove);
            this.currentImageViewer.MouseUp += new System.Windows.Forms.MouseEventHandler(this.currentImageViewer_MouseUp);
            // 
            // selectedImageLabel
            // 
            this.selectedImageLabel.AutoSize = true;
            this.selectedImageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectedImageLabel.Location = new System.Drawing.Point(3, 28);
            this.selectedImageLabel.Name = "selectedImageLabel";
            this.selectedImageLabel.Size = new System.Drawing.Size(124, 17);
            this.selectedImageLabel.TabIndex = 73;
            this.selectedImageLabel.Text = "Selected Image:";
            // 
            // fitHeightButton
            // 
            this.fitHeightButton.AutoSize = true;
            this.fitHeightButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fitHeightButton.Image = global::SebisForms.Properties.Resources.arrow_up_down;
            this.fitHeightButton.Location = new System.Drawing.Point(115, 2);
            this.fitHeightButton.Name = "fitHeightButton";
            this.fitHeightButton.Size = new System.Drawing.Size(22, 22);
            this.fitHeightButton.TabIndex = 81;
            this.toolTip1.SetToolTip(this.fitHeightButton, "Fit To Height");
            this.fitHeightButton.UseVisualStyleBackColor = true;
            this.fitHeightButton.Click += new System.EventHandler(this.fitHeightButton_Click);
            // 
            // fitWidthButton
            // 
            this.fitWidthButton.AutoSize = true;
            this.fitWidthButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fitWidthButton.Image = global::SebisForms.Properties.Resources.arrow_left_right;
            this.fitWidthButton.Location = new System.Drawing.Point(87, 2);
            this.fitWidthButton.Name = "fitWidthButton";
            this.fitWidthButton.Size = new System.Drawing.Size(22, 22);
            this.fitWidthButton.TabIndex = 80;
            this.toolTip1.SetToolTip(this.fitWidthButton, "Fit To Width");
            this.fitWidthButton.UseVisualStyleBackColor = true;
            this.fitWidthButton.Click += new System.EventHandler(this.fitWidthButton_Click);
            // 
            // loupeCb
            // 
            this.loupeCb.Appearance = System.Windows.Forms.Appearance.Button;
            this.loupeCb.AutoSize = true;
            this.loupeCb.Image = global::SebisForms.Properties.Resources.zoom;
            this.loupeCb.Location = new System.Drawing.Point(171, 2);
            this.loupeCb.Name = "loupeCb";
            this.loupeCb.Size = new System.Drawing.Size(22, 22);
            this.loupeCb.TabIndex = 79;
            this.toolTip1.SetToolTip(this.loupeCb, "Loupe");
            this.loupeCb.UseVisualStyleBackColor = true;
            this.loupeCb.CheckedChanged += new System.EventHandler(this.loupeCb_CheckedChanged);
            // 
            // origSizeButton
            // 
            this.origSizeButton.AutoSize = true;
            this.origSizeButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.origSizeButton.Image = global::SebisForms.Properties.Resources.arrow_out;
            this.origSizeButton.Location = new System.Drawing.Point(143, 2);
            this.origSizeButton.Name = "origSizeButton";
            this.origSizeButton.Size = new System.Drawing.Size(22, 22);
            this.origSizeButton.TabIndex = 78;
            this.toolTip1.SetToolTip(this.origSizeButton, "Original Size");
            this.origSizeButton.UseVisualStyleBackColor = true;
            this.origSizeButton.Click += new System.EventHandler(this.origSizeButton_Click);
            // 
            // zoomInButton
            // 
            this.zoomInButton.AutoSize = true;
            this.zoomInButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.zoomInButton.Image = global::SebisForms.Properties.Resources.zoom_in;
            this.zoomInButton.Location = new System.Drawing.Point(3, 2);
            this.zoomInButton.Name = "zoomInButton";
            this.zoomInButton.Size = new System.Drawing.Size(22, 22);
            this.zoomInButton.TabIndex = 74;
            this.toolTip1.SetToolTip(this.zoomInButton, "Zoom In");
            this.zoomInButton.UseVisualStyleBackColor = true;
            this.zoomInButton.Click += new System.EventHandler(this.zoomInButton_Click);
            // 
            // fit2PageButton
            // 
            this.fit2PageButton.AutoSize = true;
            this.fit2PageButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.fit2PageButton.Image = global::SebisForms.Properties.Resources.arrow_in;
            this.fit2PageButton.Location = new System.Drawing.Point(59, 1);
            this.fit2PageButton.Name = "fit2PageButton";
            this.fit2PageButton.Size = new System.Drawing.Size(22, 22);
            this.fit2PageButton.TabIndex = 76;
            this.toolTip1.SetToolTip(this.fit2PageButton, "Fit To Page");
            this.fit2PageButton.UseVisualStyleBackColor = true;
            this.fit2PageButton.Click += new System.EventHandler(this.fit2PageButton_Click);
            // 
            // zoomOutButton
            // 
            this.zoomOutButton.AutoSize = true;
            this.zoomOutButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.zoomOutButton.Image = global::SebisForms.Properties.Resources.zoom_out;
            this.zoomOutButton.Location = new System.Drawing.Point(31, 2);
            this.zoomOutButton.Name = "zoomOutButton";
            this.zoomOutButton.Size = new System.Drawing.Size(22, 22);
            this.zoomOutButton.TabIndex = 75;
            this.toolTip1.SetToolTip(this.zoomOutButton, "Zoom Out");
            this.zoomOutButton.UseVisualStyleBackColor = true;
            this.zoomOutButton.Click += new System.EventHandler(this.zoomOutButton_Click);
            // 
            // MyImageViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fitHeightButton);
            this.Controls.Add(this.fitWidthButton);
            this.Controls.Add(this.loupeCb);
            this.Controls.Add(this.origSizeButton);
            this.Controls.Add(this.imagePanel);
            this.Controls.Add(this.zoomInButton);
            this.Controls.Add(this.fit2PageButton);
            this.Controls.Add(this.zoomOutButton);
            this.Controls.Add(this.selectedImageLabel);
            this.DoubleBuffered = true;
            this.Name = "MyImageViewer";
            this.Size = new System.Drawing.Size(751, 420);
            this.imagePanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.currentImageViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button fitHeightButton;
        private System.Windows.Forms.Button fitWidthButton;
        private System.Windows.Forms.CheckBox loupeCb;
        private System.Windows.Forms.Button origSizeButton;
        private System.Windows.Forms.Button zoomInButton;
        private System.Windows.Forms.Button fit2PageButton;
        private System.Windows.Forms.Button zoomOutButton;
        private System.Windows.Forms.Panel imagePanel;
        private System.Windows.Forms.Label selectedImageLabel;
        private System.Windows.Forms.PictureBox currentImageViewer;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
