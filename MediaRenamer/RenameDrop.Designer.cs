namespace MediaRenamer
{
    partial class RenameDrop
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
            this.progressLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.progressTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.dropTargetImg = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dropTargetImg)).BeginInit();
            this.SuspendLayout();
            // 
            // progressLayout
            // 
            this.progressLayout.AutoSize = true;
            this.progressLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.progressLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.progressLayout.Location = new System.Drawing.Point(0, 43);
            this.progressLayout.Margin = new System.Windows.Forms.Padding(0);
            this.progressLayout.Name = "progressLayout";
            this.progressLayout.Size = new System.Drawing.Size(0, 0);
            this.progressLayout.TabIndex = 0;
            // 
            // dropTargetImg
            // 
            this.dropTargetImg.BackColor = System.Drawing.Color.Transparent;
            this.dropTargetImg.Image = global::MediaRenamer.Properties.Resources.dropTarget;
            this.dropTargetImg.Location = new System.Drawing.Point(0, 0);
            this.dropTargetImg.Margin = new System.Windows.Forms.Padding(0);
            this.dropTargetImg.Name = "dropTargetImg";
            this.dropTargetImg.Size = new System.Drawing.Size(60, 40);
            this.dropTargetImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.dropTargetImg.TabIndex = 1;
            this.dropTargetImg.TabStop = false;
            // 
            // RenameDrop
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(58, 40);
            this.Controls.Add(this.dropTargetImg);
            this.Controls.Add(this.progressLayout);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(64, 256);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(64, 64);
            this.Name = "RenameDrop";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "MediaRenamer";
            this.TopMost = true;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.RenameDrop_Paint);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.RenameDrop_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.RenameDrop_DragEnter);
            this.DragOver += new System.Windows.Forms.DragEventHandler(this.RenameDrop_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.dropTargetImg)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel progressLayout;
        private System.Windows.Forms.ToolTip progressTooltip;
        private System.Windows.Forms.PictureBox dropTargetImg;



    }
}