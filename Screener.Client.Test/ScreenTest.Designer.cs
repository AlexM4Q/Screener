namespace Screener.Client.Test
{
    partial class ScreenTest
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
            this.ScreenViewer = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ScreenViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // ScreenViewer
            // 
            this.ScreenViewer.Location = new System.Drawing.Point(12, 12);
            this.ScreenViewer.Name = "ScreenViewer";
            this.ScreenViewer.Size = new System.Drawing.Size(951, 607);
            this.ScreenViewer.TabIndex = 0;
            this.ScreenViewer.TabStop = false;
            // 
            // ScreenTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 631);
            this.Controls.Add(this.ScreenViewer);
            this.Name = "ScreenTest";
            this.Text = "ScreenTest";
            ((System.ComponentModel.ISupportInitialize)(this.ScreenViewer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox ScreenViewer;
    }
}