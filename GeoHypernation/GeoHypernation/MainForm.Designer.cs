namespace GeoHypernation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.main_splitContainer = new System.Windows.Forms.SplitContainer();
            this.Doc_Panel = new GeoHypernation.DesignHelpers.RoundedPanel();
            this.previous_btn = new System.Windows.Forms.Button();
            this.next_btn = new System.Windows.Forms.Button();
            this.Pagination_Box = new System.Windows.Forms.Panel();
            this.loading_box = new System.Windows.Forms.Panel();
            this.progress_lbl = new System.Windows.Forms.Label();
            this.loading_img = new System.Windows.Forms.PictureBox();
            this.filename_lbl = new System.Windows.Forms.Label();
            this.size_lbl = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Preview_Panel = new GeoHypernation.DesignHelpers.RoundedPanel();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.Save_btn = new GeoHypernation.RoundedButton();
            this.FinnishedShape_pic = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ForIndesign_chkbx = new System.Windows.Forms.CheckBox();
            this.hyp_chkbx = new System.Windows.Forms.CheckBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pdashstart_chkbx = new System.Windows.Forms.CheckBox();
            this.start_btn = new GeoHypernation.RoundedButton();
            this.newline_chkbx = new System.Windows.Forms.CheckBox();
            this.Help_btn = new System.Windows.Forms.Button();
            this.par_chkbx = new System.Windows.Forms.CheckBox();
            this.space_chkbx = new System.Windows.Forms.CheckBox();
            this.Finnished_pic = new System.Windows.Forms.PictureBox();
            this.Doc_pic = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.type_lbl = new System.Windows.Forms.Label();
            this.Upload_panel = new System.Windows.Forms.Panel();
            this.manual_btn = new System.Windows.Forms.Button();
            this.upload_btn = new System.Windows.Forms.Button();
            this.Upload_picture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.main_splitContainer)).BeginInit();
            this.main_splitContainer.Panel1.SuspendLayout();
            this.main_splitContainer.Panel2.SuspendLayout();
            this.main_splitContainer.SuspendLayout();
            this.Doc_Panel.SuspendLayout();
            this.loading_box.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loading_img)).BeginInit();
            this.Preview_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FinnishedShape_pic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Finnished_pic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Doc_pic)).BeginInit();
            this.Upload_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Upload_picture)).BeginInit();
            this.SuspendLayout();
            // 
            // main_splitContainer
            // 
            this.main_splitContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            resources.ApplyResources(this.main_splitContainer, "main_splitContainer");
            this.main_splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.main_splitContainer.Name = "main_splitContainer";
            // 
            // main_splitContainer.Panel1
            // 
            this.main_splitContainer.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.main_splitContainer.Panel1.Controls.Add(this.Doc_Panel);
            resources.ApplyResources(this.main_splitContainer.Panel1, "main_splitContainer.Panel1");
            // 
            // main_splitContainer.Panel2
            // 
            this.main_splitContainer.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.main_splitContainer.Panel2.Controls.Add(this.Save_btn);
            this.main_splitContainer.Panel2.Controls.Add(this.FinnishedShape_pic);
            this.main_splitContainer.Panel2.Controls.Add(this.label2);
            this.main_splitContainer.Panel2.Controls.Add(this.ForIndesign_chkbx);
            this.main_splitContainer.Panel2.Controls.Add(this.hyp_chkbx);
            this.main_splitContainer.Panel2.Controls.Add(this.pictureBox2);
            this.main_splitContainer.Panel2.Controls.Add(this.pdashstart_chkbx);
            this.main_splitContainer.Panel2.Controls.Add(this.start_btn);
            this.main_splitContainer.Panel2.Controls.Add(this.newline_chkbx);
            this.main_splitContainer.Panel2.Controls.Add(this.Help_btn);
            this.main_splitContainer.Panel2.Controls.Add(this.par_chkbx);
            this.main_splitContainer.Panel2.Controls.Add(this.space_chkbx);
            this.main_splitContainer.Panel2.Controls.Add(this.Finnished_pic);
            this.main_splitContainer.Panel2.Controls.Add(this.Doc_pic);
            this.main_splitContainer.Panel2.Controls.Add(this.label1);
            this.main_splitContainer.Panel2.Controls.Add(this.type_lbl);
            resources.ApplyResources(this.main_splitContainer.Panel2, "main_splitContainer.Panel2");
            this.main_splitContainer.Panel2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            // 
            // Doc_Panel
            // 
            resources.ApplyResources(this.Doc_Panel, "Doc_Panel");
            this.Doc_Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.Doc_Panel.BorderColor = System.Drawing.Color.Yellow;
            this.Doc_Panel.BorderRadius = 25;
            this.Doc_Panel.BorderSize = 3;
            this.Doc_Panel.Controls.Add(this.previous_btn);
            this.Doc_Panel.Controls.Add(this.next_btn);
            this.Doc_Panel.Controls.Add(this.Pagination_Box);
            this.Doc_Panel.Controls.Add(this.loading_box);
            this.Doc_Panel.Controls.Add(this.filename_lbl);
            this.Doc_Panel.Controls.Add(this.size_lbl);
            this.Doc_Panel.Controls.Add(this.button1);
            this.Doc_Panel.Controls.Add(this.Preview_Panel);
            this.Doc_Panel.ForeColor = System.Drawing.Color.White;
            this.Doc_Panel.Name = "Doc_Panel";
            this.Doc_Panel.TextColor = System.Drawing.Color.White;
            // 
            // previous_btn
            // 
            resources.ApplyResources(this.previous_btn, "previous_btn");
            this.previous_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.previous_btn.FlatAppearance.BorderSize = 0;
            this.previous_btn.Name = "previous_btn";
            this.previous_btn.UseVisualStyleBackColor = true;
            this.previous_btn.Click += new System.EventHandler(this.previous_btn_Click);
            // 
            // next_btn
            // 
            resources.ApplyResources(this.next_btn, "next_btn");
            this.next_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.next_btn.FlatAppearance.BorderSize = 0;
            this.next_btn.Name = "next_btn";
            this.next_btn.UseVisualStyleBackColor = true;
            this.next_btn.Click += new System.EventHandler(this.next_btn_Click);
            // 
            // Pagination_Box
            // 
            resources.ApplyResources(this.Pagination_Box, "Pagination_Box");
            this.Pagination_Box.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.Pagination_Box.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Pagination_Box.Name = "Pagination_Box";
            // 
            // loading_box
            // 
            resources.ApplyResources(this.loading_box, "loading_box");
            this.loading_box.Controls.Add(this.progress_lbl);
            this.loading_box.Controls.Add(this.loading_img);
            this.loading_box.Name = "loading_box";
            // 
            // progress_lbl
            // 
            resources.ApplyResources(this.progress_lbl, "progress_lbl");
            this.progress_lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(43)))), ((int)(((byte)(34)))));
            this.progress_lbl.ForeColor = System.Drawing.SystemColors.Info;
            this.progress_lbl.Name = "progress_lbl";
            this.progress_lbl.UseWaitCursor = true;
            // 
            // loading_img
            // 
            this.loading_img.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(43)))), ((int)(((byte)(34)))));
            resources.ApplyResources(this.loading_img, "loading_img");
            this.loading_img.Image = global::GeoHypernation.Properties.Resources._200;
            this.loading_img.Name = "loading_img";
            this.loading_img.TabStop = false;
            this.loading_img.UseWaitCursor = true;
            // 
            // filename_lbl
            // 
            resources.ApplyResources(this.filename_lbl, "filename_lbl");
            this.filename_lbl.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.filename_lbl.Name = "filename_lbl";
            // 
            // size_lbl
            // 
            resources.ApplyResources(this.size_lbl, "size_lbl");
            this.size_lbl.ForeColor = System.Drawing.SystemColors.Info;
            this.size_lbl.Name = "size_lbl";
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.button1, "button1");
            this.button1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Close_btn_Click);
            // 
            // Preview_Panel
            // 
            resources.ApplyResources(this.Preview_Panel, "Preview_Panel");
            this.Preview_Panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Preview_Panel.BorderColor = System.Drawing.Color.Gray;
            this.Preview_Panel.BorderRadius = 0;
            this.Preview_Panel.BorderSize = 2;
            this.Preview_Panel.Controls.Add(this.webBrowser);
            this.Preview_Panel.ForeColor = System.Drawing.Color.White;
            this.Preview_Panel.Name = "Preview_Panel";
            this.Preview_Panel.TextColor = System.Drawing.Color.White;
            // 
            // webBrowser
            // 
            this.webBrowser.AllowWebBrowserDrop = false;
            resources.ApplyResources(this.webBrowser, "webBrowser");
            this.webBrowser.Name = "webBrowser";
            // 
            // Save_btn
            // 
            this.Save_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(223)))), ((int)(((byte)(145)))));
            this.Save_btn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(223)))), ((int)(((byte)(145)))));
            this.Save_btn.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.Save_btn.BorderRadius = 12;
            this.Save_btn.BorderSize = 0;
            this.Save_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Save_btn.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.Save_btn, "Save_btn");
            this.Save_btn.ForeColor = System.Drawing.Color.White;
            this.Save_btn.Name = "Save_btn";
            this.Save_btn.TextColor = System.Drawing.Color.White;
            this.Save_btn.UseVisualStyleBackColor = false;
            this.Save_btn.Click += new System.EventHandler(this.Save_btn_Click);
            // 
            // FinnishedShape_pic
            // 
            resources.ApplyResources(this.FinnishedShape_pic, "FinnishedShape_pic");
            this.FinnishedShape_pic.Name = "FinnishedShape_pic";
            this.FinnishedShape_pic.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // ForIndesign_chkbx
            // 
            resources.ApplyResources(this.ForIndesign_chkbx, "ForIndesign_chkbx");
            this.ForIndesign_chkbx.Name = "ForIndesign_chkbx";
            this.ForIndesign_chkbx.UseVisualStyleBackColor = true;
            this.ForIndesign_chkbx.Click += new System.EventHandler(this.ForIndesign_chkbx_CheckedChanged);
            // 
            // hyp_chkbx
            // 
            resources.ApplyResources(this.hyp_chkbx, "hyp_chkbx");
            this.hyp_chkbx.Checked = true;
            this.hyp_chkbx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hyp_chkbx.Name = "hyp_chkbx";
            this.hyp_chkbx.UseVisualStyleBackColor = true;
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            // 
            // pdashstart_chkbx
            // 
            resources.ApplyResources(this.pdashstart_chkbx, "pdashstart_chkbx");
            this.pdashstart_chkbx.Name = "pdashstart_chkbx";
            this.pdashstart_chkbx.UseVisualStyleBackColor = true;
            // 
            // start_btn
            // 
            this.start_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.start_btn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.start_btn.BorderColor = System.Drawing.SystemColors.ActiveCaption;
            this.start_btn.BorderRadius = 12;
            this.start_btn.BorderSize = 0;
            this.start_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.start_btn.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.start_btn, "start_btn");
            this.start_btn.ForeColor = System.Drawing.Color.White;
            this.start_btn.Name = "start_btn";
            this.start_btn.TextColor = System.Drawing.Color.White;
            this.start_btn.UseVisualStyleBackColor = false;
            this.start_btn.Click += new System.EventHandler(this.Start_btn_Click);
            // 
            // newline_chkbx
            // 
            resources.ApplyResources(this.newline_chkbx, "newline_chkbx");
            this.newline_chkbx.FlatAppearance.BorderSize = 0;
            this.newline_chkbx.FlatAppearance.CheckedBackColor = System.Drawing.Color.WhiteSmoke;
            this.newline_chkbx.Name = "newline_chkbx";
            this.newline_chkbx.UseVisualStyleBackColor = true;
            // 
            // Help_btn
            // 
            resources.ApplyResources(this.Help_btn, "Help_btn");
            this.Help_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(53)))), ((int)(((byte)(53)))));
            this.Help_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Help_btn.FlatAppearance.BorderSize = 0;
            this.Help_btn.Name = "Help_btn";
            this.Help_btn.UseVisualStyleBackColor = false;
            this.Help_btn.Click += new System.EventHandler(this.Help_btn_Click);
            // 
            // par_chkbx
            // 
            resources.ApplyResources(this.par_chkbx, "par_chkbx");
            this.par_chkbx.Name = "par_chkbx";
            this.par_chkbx.UseVisualStyleBackColor = true;
            // 
            // space_chkbx
            // 
            resources.ApplyResources(this.space_chkbx, "space_chkbx");
            this.space_chkbx.Checked = true;
            this.space_chkbx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.space_chkbx.FlatAppearance.BorderSize = 0;
            this.space_chkbx.Name = "space_chkbx";
            this.space_chkbx.UseVisualStyleBackColor = true;
            // 
            // Finnished_pic
            // 
            resources.ApplyResources(this.Finnished_pic, "Finnished_pic");
            this.Finnished_pic.Name = "Finnished_pic";
            this.Finnished_pic.TabStop = false;
            // 
            // Doc_pic
            // 
            resources.ApplyResources(this.Doc_pic, "Doc_pic");
            this.Doc_pic.Name = "Doc_pic";
            this.Doc_pic.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // type_lbl
            // 
            resources.ApplyResources(this.type_lbl, "type_lbl");
            this.type_lbl.Name = "type_lbl";
            // 
            // Upload_panel
            // 
            this.Upload_panel.Controls.Add(this.manual_btn);
            this.Upload_panel.Controls.Add(this.upload_btn);
            this.Upload_panel.Controls.Add(this.Upload_picture);
            resources.ApplyResources(this.Upload_panel, "Upload_panel");
            this.Upload_panel.Name = "Upload_panel";
            // 
            // manual_btn
            // 
            resources.ApplyResources(this.manual_btn, "manual_btn");
            this.manual_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.manual_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.manual_btn.FlatAppearance.BorderSize = 0;
            this.manual_btn.Name = "manual_btn";
            this.manual_btn.UseVisualStyleBackColor = false;
            // 
            // upload_btn
            // 
            resources.ApplyResources(this.upload_btn, "upload_btn");
            this.upload_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.upload_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.upload_btn.FlatAppearance.BorderSize = 0;
            this.upload_btn.Name = "upload_btn";
            this.upload_btn.UseVisualStyleBackColor = false;
            // 
            // Upload_picture
            // 
            this.Upload_picture.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Upload_picture.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.Upload_picture, "Upload_picture");
            this.Upload_picture.Name = "Upload_picture";
            this.Upload_picture.TabStop = false;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(43)))), ((int)(((byte)(43)))));
            this.Controls.Add(this.Upload_panel);
            this.Controls.Add(this.main_splitContainer);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.main_splitContainer.Panel1.ResumeLayout(false);
            this.main_splitContainer.Panel2.ResumeLayout(false);
            this.main_splitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.main_splitContainer)).EndInit();
            this.main_splitContainer.ResumeLayout(false);
            this.Doc_Panel.ResumeLayout(false);
            this.Doc_Panel.PerformLayout();
            this.loading_box.ResumeLayout(false);
            this.loading_box.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loading_img)).EndInit();
            this.Preview_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FinnishedShape_pic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Finnished_pic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Doc_pic)).EndInit();
            this.Upload_panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Upload_picture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer main_splitContainer;
        private System.Windows.Forms.Button Help_btn;
        private System.Windows.Forms.CheckBox ForIndesign_chkbx;
        private System.Windows.Forms.CheckBox hyp_chkbx;
        private System.Windows.Forms.CheckBox pdashstart_chkbx;
        private System.Windows.Forms.CheckBox newline_chkbx;
        private System.Windows.Forms.CheckBox par_chkbx;
        private System.Windows.Forms.CheckBox space_chkbx;
        private System.Windows.Forms.Label filename_lbl;
        private System.Windows.Forms.Label size_lbl;
        private System.Windows.Forms.Label type_lbl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox Doc_pic;
        private System.Windows.Forms.Panel loading_box;
        private System.Windows.Forms.Label progress_lbl;
        private System.Windows.Forms.PictureBox loading_img;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Panel Pagination_Box;
        private System.Windows.Forms.Button button1;
        private RoundedButton start_btn;
        private DesignHelpers.RoundedPanel Doc_Panel;
        private DesignHelpers.RoundedPanel Preview_Panel;
        private System.Windows.Forms.PictureBox pictureBox2;
        private RoundedButton Save_btn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox FinnishedShape_pic;
        private System.Windows.Forms.PictureBox Finnished_pic;
        private System.Windows.Forms.Button next_btn;
        private System.Windows.Forms.Button previous_btn;
        private System.Windows.Forms.Panel Upload_panel;
        private System.Windows.Forms.Button manual_btn;
        private System.Windows.Forms.Button upload_btn;
        private System.Windows.Forms.PictureBox Upload_picture;
    }
}

