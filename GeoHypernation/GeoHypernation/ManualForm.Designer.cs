
namespace GeoHypernation
{
    partial class ManualForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManualForm));
            this.hyp_chkbx = new System.Windows.Forms.CheckBox();
            this.pdashstart_chkbx = new System.Windows.Forms.CheckBox();
            this.par_chkbx = new System.Windows.Forms.CheckBox();
            this.space_chkbx = new System.Windows.Forms.CheckBox();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.info_btn = new System.Windows.Forms.Button();
            this.progress_lbl = new System.Windows.Forms.Label();
            this.Text_Panel = new GeoHypernation.DesignHelpers.RoundedPanel();
            this.copy_btn = new System.Windows.Forms.Button();
            this.paste_btn = new System.Windows.Forms.Button();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.line = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.Start_btn = new GeoHypernation.RoundedButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.Text_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // hyp_chkbx
            // 
            this.hyp_chkbx.AutoSize = true;
            this.hyp_chkbx.Checked = true;
            this.hyp_chkbx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hyp_chkbx.Font = new System.Drawing.Font("FiraGO", 10F);
            this.hyp_chkbx.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.hyp_chkbx.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.hyp_chkbx.Location = new System.Drawing.Point(5, 197);
            this.hyp_chkbx.Name = "hyp_chkbx";
            this.hyp_chkbx.Size = new System.Drawing.Size(110, 21);
            this.hyp_chkbx.TabIndex = 9;
            this.hyp_chkbx.Text = "დამარცვლა";
            this.hyp_chkbx.UseVisualStyleBackColor = true;
            // 
            // pdashstart_chkbx
            // 
            this.pdashstart_chkbx.AutoSize = true;
            this.pdashstart_chkbx.Font = new System.Drawing.Font("FiraGO", 10F);
            this.pdashstart_chkbx.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.pdashstart_chkbx.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.pdashstart_chkbx.Location = new System.Drawing.Point(5, 170);
            this.pdashstart_chkbx.Name = "pdashstart_chkbx";
            this.pdashstart_chkbx.Size = new System.Drawing.Size(154, 21);
            this.pdashstart_chkbx.TabIndex = 10;
            this.pdashstart_chkbx.Text = "ქართული დეფისი";
            this.pdashstart_chkbx.UseVisualStyleBackColor = true;
            // 
            // par_chkbx
            // 
            this.par_chkbx.AutoSize = true;
            this.par_chkbx.Font = new System.Drawing.Font("FiraGO", 10F);
            this.par_chkbx.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.par_chkbx.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.par_chkbx.Location = new System.Drawing.Point(5, 142);
            this.par_chkbx.Name = "par_chkbx";
            this.par_chkbx.Size = new System.Drawing.Size(282, 21);
            this.par_chkbx.TabIndex = 12;
            this.par_chkbx.Text = "ორმაგი პარაგრაფების გასუფთავება";
            this.par_chkbx.UseVisualStyleBackColor = true;
            // 
            // space_chkbx
            // 
            this.space_chkbx.AutoSize = true;
            this.space_chkbx.Checked = true;
            this.space_chkbx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.space_chkbx.FlatAppearance.BorderSize = 0;
            this.space_chkbx.Font = new System.Drawing.Font("FiraGO", 10F);
            this.space_chkbx.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.space_chkbx.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.space_chkbx.Location = new System.Drawing.Point(5, 114);
            this.space_chkbx.Name = "space_chkbx";
            this.space_chkbx.Size = new System.Drawing.Size(181, 21);
            this.space_chkbx.TabIndex = 13;
            this.space_chkbx.Text = "ჰარების გასუფთავება";
            this.space_chkbx.UseVisualStyleBackColor = true;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.info_btn);
            this.splitContainer.Panel1.Controls.Add(this.progress_lbl);
            this.splitContainer.Panel1.Controls.Add(this.Text_Panel);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.pictureBox2);
            this.splitContainer.Panel2.Controls.Add(this.Start_btn);
            this.splitContainer.Panel2.Controls.Add(this.hyp_chkbx);
            this.splitContainer.Panel2.Controls.Add(this.space_chkbx);
            this.splitContainer.Panel2.Controls.Add(this.pdashstart_chkbx);
            this.splitContainer.Panel2.Controls.Add(this.par_chkbx);
            this.splitContainer.Size = new System.Drawing.Size(875, 351);
            this.splitContainer.SplitterDistance = 579;
            this.splitContainer.TabIndex = 14;
            // 
            // info_btn
            // 
            this.info_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("info_btn.BackgroundImage")));
            this.info_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.info_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.info_btn.FlatAppearance.BorderSize = 0;
            this.info_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.info_btn.Location = new System.Drawing.Point(22, 13);
            this.info_btn.Name = "info_btn";
            this.info_btn.Size = new System.Drawing.Size(30, 27);
            this.info_btn.TabIndex = 3;
            this.info_btn.UseVisualStyleBackColor = true;
            this.info_btn.Click += new System.EventHandler(this.info_btn_Click);
            // 
            // progress_lbl
            // 
            this.progress_lbl.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.progress_lbl.AutoSize = true;
            this.progress_lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.progress_lbl.ForeColor = System.Drawing.SystemColors.Info;
            this.progress_lbl.Location = new System.Drawing.Point(235, 23);
            this.progress_lbl.Name = "progress_lbl";
            this.progress_lbl.Size = new System.Drawing.Size(0, 17);
            this.progress_lbl.TabIndex = 2;
            // 
            // Text_Panel
            // 
            this.Text_Panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Text_Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.Text_Panel.BorderColor = System.Drawing.Color.Yellow;
            this.Text_Panel.BorderRadius = 30;
            this.Text_Panel.BorderSize = 3;
            this.Text_Panel.Controls.Add(this.copy_btn);
            this.Text_Panel.Controls.Add(this.paste_btn);
            this.Text_Panel.Controls.Add(this.richTextBox);
            this.Text_Panel.Controls.Add(this.line);
            this.Text_Panel.ForeColor = System.Drawing.Color.White;
            this.Text_Panel.Location = new System.Drawing.Point(22, 49);
            this.Text_Panel.Name = "Text_Panel";
            this.Text_Panel.Size = new System.Drawing.Size(554, 264);
            this.Text_Panel.TabIndex = 0;
            this.Text_Panel.TextColor = System.Drawing.Color.White;
            // 
            // copy_btn
            // 
            this.copy_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.copy_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.copy_btn.FlatAppearance.BorderSize = 0;
            this.copy_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.copy_btn.Font = new System.Drawing.Font("FiraGO", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.copy_btn.ForeColor = System.Drawing.Color.Gainsboro;
            this.copy_btn.Image = ((System.Drawing.Image)(resources.GetObject("copy_btn.Image")));
            this.copy_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.copy_btn.Location = new System.Drawing.Point(431, 14);
            this.copy_btn.Name = "copy_btn";
            this.copy_btn.Size = new System.Drawing.Size(95, 30);
            this.copy_btn.TabIndex = 2;
            this.copy_btn.Text = "მონიშვნა";
            this.copy_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.copy_btn.UseVisualStyleBackColor = true;
            this.copy_btn.Click += new System.EventHandler(this.copy_btn_Click);
            // 
            // paste_btn
            // 
            this.paste_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.paste_btn.FlatAppearance.BorderSize = 0;
            this.paste_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.paste_btn.Font = new System.Drawing.Font("FiraGO", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paste_btn.ForeColor = System.Drawing.Color.Gainsboro;
            this.paste_btn.Image = ((System.Drawing.Image)(resources.GetObject("paste_btn.Image")));
            this.paste_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.paste_btn.Location = new System.Drawing.Point(27, 14);
            this.paste_btn.Name = "paste_btn";
            this.paste_btn.Size = new System.Drawing.Size(153, 30);
            this.paste_btn.TabIndex = 2;
            this.paste_btn.Text = "მონიშნულის ჩასმა";
            this.paste_btn.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.paste_btn.UseVisualStyleBackColor = true;
            this.paste_btn.Click += new System.EventHandler(this.paste_btn_Click);
            // 
            // richTextBox
            // 
            this.richTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.richTextBox.Location = new System.Drawing.Point(27, 51);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(502, 206);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            this.richTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox_KeyDown);
            // 
            // line
            // 
            this.line.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.line.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.line.Location = new System.Drawing.Point(27, 45);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(502, 1);
            this.line.TabIndex = 0;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(80, 250);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(171, 89);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            // 
            // Start_btn
            // 
            this.Start_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(223)))), ((int)(((byte)(145)))));
            this.Start_btn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(223)))), ((int)(((byte)(145)))));
            this.Start_btn.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.Start_btn.BorderRadius = 12;
            this.Start_btn.BorderSize = 0;
            this.Start_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Start_btn.FlatAppearance.BorderSize = 0;
            this.Start_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Start_btn.Font = new System.Drawing.Font("Gilroy GEO Heavy", 12F, System.Drawing.FontStyle.Bold);
            this.Start_btn.ForeColor = System.Drawing.Color.White;
            this.Start_btn.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Start_btn.Location = new System.Drawing.Point(91, 65);
            this.Start_btn.Name = "Start_btn";
            this.Start_btn.Size = new System.Drawing.Size(104, 31);
            this.Start_btn.TabIndex = 8;
            this.Start_btn.Text = "დაწყება";
            this.Start_btn.TextColor = System.Drawing.Color.White;
            this.Start_btn.UseVisualStyleBackColor = false;
            this.Start_btn.Click += new System.EventHandler(this.Start_btn_Click);
            // 
            // ManualForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.ClientSize = new System.Drawing.Size(875, 351);
            this.Controls.Add(this.splitContainer);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(650, 390);
            this.Name = "ManualForm";
            this.Text = "ჩამშვები — მექანიკური";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.Text_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DesignHelpers.RoundedPanel Text_Panel;
        private RoundedButton Start_btn;
        private System.Windows.Forms.CheckBox hyp_chkbx;
        private System.Windows.Forms.CheckBox pdashstart_chkbx;
        private System.Windows.Forms.CheckBox par_chkbx;
        private System.Windows.Forms.CheckBox space_chkbx;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Panel line;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label progress_lbl;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button info_btn;
        private System.Windows.Forms.Button paste_btn;
        private System.Windows.Forms.Button copy_btn;
    }
}