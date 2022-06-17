
namespace GeoHypernation
{
    partial class LicenseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LicenseForm));
            this.linkLabel = new System.Windows.Forms.LinkLabel();
            this.timer_lbl = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.close_btn = new GeoHypernation.RoundedButton();
            this.continue_btn = new GeoHypernation.RoundedButton();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // linkLabel
            // 
            this.linkLabel.AutoSize = true;
            this.linkLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.linkLabel.Font = new System.Drawing.Font("FiraGO", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.linkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
            this.linkLabel.LinkColor = System.Drawing.Color.CornflowerBlue;
            this.linkLabel.Location = new System.Drawing.Point(14, 19);
            this.linkLabel.Name = "linkLabel";
            this.linkLabel.Size = new System.Drawing.Size(224, 14);
            this.linkLabel.TabIndex = 2;
            this.linkLabel.TabStop = true;
            this.linkLabel.Text = "ლიცენზიის შესაძენად დააჭირეთ აქ";
            this.linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // timer_lbl
            // 
            this.timer_lbl.AutoSize = true;
            this.timer_lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.timer_lbl.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.timer_lbl.Location = new System.Drawing.Point(240, 192);
            this.timer_lbl.Name = "timer_lbl";
            this.timer_lbl.Size = new System.Drawing.Size(13, 13);
            this.timer_lbl.TabIndex = 4;
            this.timer_lbl.Text = "9";
            this.timer_lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(454, 269);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // close_btn
            // 
            this.close_btn.BackColor = System.Drawing.Color.Gray;
            this.close_btn.BackgroundColor = System.Drawing.Color.Gray;
            this.close_btn.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.close_btn.BorderRadius = 8;
            this.close_btn.BorderSize = 0;
            this.close_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close_btn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.close_btn.FlatAppearance.BorderSize = 0;
            this.close_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.close_btn.Font = new System.Drawing.Font("Gilroy GEO Heavy", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close_btn.ForeColor = System.Drawing.Color.White;
            this.close_btn.Location = new System.Drawing.Point(272, 14);
            this.close_btn.Name = "close_btn";
            this.close_btn.Size = new System.Drawing.Size(75, 24);
            this.close_btn.TabIndex = 6;
            this.close_btn.Text = "დახურვა";
            this.close_btn.TextColor = System.Drawing.Color.White;
            this.close_btn.UseVisualStyleBackColor = false;
            this.close_btn.Click += new System.EventHandler(this.close_btn_Click);
            // 
            // continue_btn
            // 
            this.continue_btn.BackColor = System.Drawing.Color.Gray;
            this.continue_btn.BackgroundColor = System.Drawing.Color.Gray;
            this.continue_btn.BorderColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.continue_btn.BorderRadius = 8;
            this.continue_btn.BorderSize = 0;
            this.continue_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.continue_btn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.continue_btn.Enabled = false;
            this.continue_btn.FlatAppearance.BorderSize = 0;
            this.continue_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.continue_btn.Font = new System.Drawing.Font("Gilroy GEO Heavy", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.continue_btn.ForeColor = System.Drawing.Color.White;
            this.continue_btn.Location = new System.Drawing.Point(356, 14);
            this.continue_btn.Name = "continue_btn";
            this.continue_btn.Size = new System.Drawing.Size(84, 24);
            this.continue_btn.TabIndex = 6;
            this.continue_btn.Text = "გაგრძელება";
            this.continue_btn.TextColor = System.Drawing.Color.White;
            this.continue_btn.UseVisualStyleBackColor = false;
            this.continue_btn.Click += new System.EventHandler(this.close_btn_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.panel1.Controls.Add(this.continue_btn);
            this.panel1.Controls.Add(this.linkLabel);
            this.panel1.Controls.Add(this.close_btn);
            this.panel1.Location = new System.Drawing.Point(0, 220);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(454, 49);
            this.panel1.TabIndex = 7;
            // 
            // LicenseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 269);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.timer_lbl);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LicenseForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ლიცეზია";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.LinkLabel linkLabel;
        private System.Windows.Forms.Label timer_lbl;
        private System.Windows.Forms.PictureBox pictureBox1;
        private RoundedButton close_btn;
        private RoundedButton continue_btn;
        private System.Windows.Forms.Panel panel1;
    }
}