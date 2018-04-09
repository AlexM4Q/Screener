namespace Screener.WinApp
{
    partial class ScreenViewer
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
            this.FpsSlider = new System.Windows.Forms.TrackBar();
            this.FpsLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.FpsSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // FpsSlider
            // 
            this.FpsSlider.Location = new System.Drawing.Point(462, 404);
            this.FpsSlider.Maximum = 60;
            this.FpsSlider.Name = "FpsSlider";
            this.FpsSlider.Size = new System.Drawing.Size(260, 45);
            this.FpsSlider.TabIndex = 1;
            this.FpsSlider.Scroll += new System.EventHandler(this.OnFpsSliderScroll);
            // 
            // FpsLabel
            // 
            this.FpsLabel.AutoSize = true;
            this.FpsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FpsLabel.Location = new System.Drawing.Point(728, 404);
            this.FpsLabel.Name = "FpsLabel";
            this.FpsLabel.Size = new System.Drawing.Size(29, 31);
            this.FpsLabel.TabIndex = 2;
            this.FpsLabel.Text = "0";
            this.FpsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ScreenViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.FpsLabel);
            this.Controls.Add(this.FpsSlider);
            this.Name = "ScreenViewer";
            this.Text = "ScreenViewer";
            ((System.ComponentModel.ISupportInitialize)(this.FpsSlider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar FpsSlider;
        private System.Windows.Forms.Label FpsLabel;
    }
}