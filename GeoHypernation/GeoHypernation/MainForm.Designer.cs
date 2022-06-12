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
            this.Help_btn = new System.Windows.Forms.Button();
            this.docBox = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ForIndesign_chkbx = new System.Windows.Forms.CheckBox();
            this.hyp_chkbx = new System.Windows.Forms.CheckBox();
            this.pdashstart_chkbx = new System.Windows.Forms.CheckBox();
            this.newline_chkbx = new System.Windows.Forms.CheckBox();
            this.tab_chkbx = new System.Windows.Forms.CheckBox();
            this.par_chkbx = new System.Windows.Forms.CheckBox();
            this.space_chkbx = new System.Windows.Forms.CheckBox();
            this.start_btn = new System.Windows.Forms.Button();
            this.filename_lbl = new System.Windows.Forms.Label();
            this.size_lbl = new System.Windows.Forms.Label();
            this.page_lbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.type_lbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.preview_splitContainer = new System.Windows.Forms.SplitContainer();
            this.loading_box = new System.Windows.Forms.Panel();
            this.progress_lbl = new System.Windows.Forms.Label();
            this.loading_img = new System.Windows.Forms.PictureBox();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.Pagination_Box = new System.Windows.Forms.Panel();
            this.Save_btn = new System.Windows.Forms.Button();
            this.Upload_picture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.main_splitContainer)).BeginInit();
            this.main_splitContainer.Panel1.SuspendLayout();
            this.main_splitContainer.Panel2.SuspendLayout();
            this.main_splitContainer.SuspendLayout();
            this.docBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.preview_splitContainer)).BeginInit();
            this.preview_splitContainer.Panel1.SuspendLayout();
            this.preview_splitContainer.Panel2.SuspendLayout();
            this.preview_splitContainer.SuspendLayout();
            this.loading_box.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loading_img)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Upload_picture)).BeginInit();
            this.SuspendLayout();
            // 
            // main_splitContainer
            // 
            resources.ApplyResources(this.main_splitContainer, "main_splitContainer");
            this.main_splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.main_splitContainer.Name = "main_splitContainer";
            // 
            // main_splitContainer.Panel1
            // 
            this.main_splitContainer.Panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.main_splitContainer.Panel1.Controls.Add(this.Help_btn);
            this.main_splitContainer.Panel1.Controls.Add(this.docBox);
            // 
            // main_splitContainer.Panel2
            // 
            this.main_splitContainer.Panel2.Controls.Add(this.preview_splitContainer);
            // 
            // Help_btn
            // 
            resources.ApplyResources(this.Help_btn, "Help_btn");
            this.Help_btn.BackColor = System.Drawing.SystemColors.ControlDark;
            this.Help_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Help_btn.FlatAppearance.BorderSize = 0;
            this.Help_btn.Name = "Help_btn";
            this.Help_btn.UseVisualStyleBackColor = false;
            this.Help_btn.Click += new System.EventHandler(this.Help_btn_Click);
            // 
            // docBox
            // 
            this.docBox.Controls.Add(this.button1);
            this.docBox.Controls.Add(this.ForIndesign_chkbx);
            this.docBox.Controls.Add(this.hyp_chkbx);
            this.docBox.Controls.Add(this.pdashstart_chkbx);
            this.docBox.Controls.Add(this.newline_chkbx);
            this.docBox.Controls.Add(this.tab_chkbx);
            this.docBox.Controls.Add(this.par_chkbx);
            this.docBox.Controls.Add(this.space_chkbx);
            this.docBox.Controls.Add(this.start_btn);
            this.docBox.Controls.Add(this.filename_lbl);
            this.docBox.Controls.Add(this.size_lbl);
            this.docBox.Controls.Add(this.page_lbl);
            this.docBox.Controls.Add(this.label3);
            this.docBox.Controls.Add(this.label6);
            this.docBox.Controls.Add(this.type_lbl);
            this.docBox.Controls.Add(this.label2);
            this.docBox.Controls.Add(this.label5);
            this.docBox.Controls.Add(this.label1);
            this.docBox.Controls.Add(this.pictureBox1);
            resources.ApplyResources(this.docBox, "docBox");
            this.docBox.Name = "docBox";
            this.docBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Close_btn_Click);
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
            // pdashstart_chkbx
            // 
            resources.ApplyResources(this.pdashstart_chkbx, "pdashstart_chkbx");
            this.pdashstart_chkbx.Name = "pdashstart_chkbx";
            this.pdashstart_chkbx.UseVisualStyleBackColor = true;
            // 
            // newline_chkbx
            // 
            resources.ApplyResources(this.newline_chkbx, "newline_chkbx");
            this.newline_chkbx.Name = "newline_chkbx";
            this.newline_chkbx.UseVisualStyleBackColor = true;
            // 
            // tab_chkbx
            // 
            resources.ApplyResources(this.tab_chkbx, "tab_chkbx");
            this.tab_chkbx.Name = "tab_chkbx";
            this.tab_chkbx.UseVisualStyleBackColor = true;
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
            this.space_chkbx.Name = "space_chkbx";
            this.space_chkbx.UseVisualStyleBackColor = true;
            // 
            // start_btn
            // 
            this.start_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.start_btn, "start_btn");
            this.start_btn.Name = "start_btn";
            this.start_btn.UseVisualStyleBackColor = true;
            this.start_btn.Click += new System.EventHandler(this.Start_btn_Click);
            // 
            // filename_lbl
            // 
            resources.ApplyResources(this.filename_lbl, "filename_lbl");
            this.filename_lbl.Name = "filename_lbl";
            // 
            // size_lbl
            // 
            resources.ApplyResources(this.size_lbl, "size_lbl");
            this.size_lbl.Name = "size_lbl";
            // 
            // page_lbl
            // 
            resources.ApplyResources(this.page_lbl, "page_lbl");
            this.page_lbl.Name = "page_lbl";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // type_lbl
            // 
            resources.ApplyResources(this.type_lbl, "type_lbl");
            this.type_lbl.Name = "type_lbl";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GeoHypernation.Properties.Resources.microsoft_word_document_icon;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // preview_splitContainer
            // 
            resources.ApplyResources(this.preview_splitContainer, "preview_splitContainer");
            this.preview_splitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.preview_splitContainer.Name = "preview_splitContainer";
            // 
            // preview_splitContainer.Panel1
            // 
            this.preview_splitContainer.Panel1.Controls.Add(this.loading_box);
            this.preview_splitContainer.Panel1.Controls.Add(this.webBrowser);
            // 
            // preview_splitContainer.Panel2
            // 
            this.preview_splitContainer.Panel2.Controls.Add(this.Pagination_Box);
            this.preview_splitContainer.Panel2.Controls.Add(this.Save_btn);
            // 
            // loading_box
            // 
            this.loading_box.Controls.Add(this.progress_lbl);
            this.loading_box.Controls.Add(this.loading_img);
            resources.ApplyResources(this.loading_box, "loading_box");
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
            resources.ApplyResources(this.loading_img, "loading_img");
            this.loading_img.Image = global::GeoHypernation.Properties.Resources._200;
            this.loading_img.Name = "loading_img";
            this.loading_img.TabStop = false;
            this.loading_img.UseWaitCursor = true;
            // 
            // webBrowser
            // 
            this.webBrowser.AllowWebBrowserDrop = false;
            resources.ApplyResources(this.webBrowser, "webBrowser");
            this.webBrowser.Name = "webBrowser";
            // 
            // Pagination_Box
            // 
            resources.ApplyResources(this.Pagination_Box, "Pagination_Box");
            this.Pagination_Box.BackColor = System.Drawing.SystemColors.ControlLight;
            this.Pagination_Box.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Pagination_Box.Name = "Pagination_Box";
            // 
            // Save_btn
            // 
            resources.ApplyResources(this.Save_btn, "Save_btn");
            this.Save_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Save_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Save_btn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Save_btn.FlatAppearance.BorderSize = 3;
            this.Save_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gray;
            this.Save_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.Save_btn.Name = "Save_btn";
            this.Save_btn.UseVisualStyleBackColor = true;
            this.Save_btn.Click += new System.EventHandler(this.Save_btn_Click);
            // 
            // Upload_picture
            // 
            this.Upload_picture.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.Upload_picture, "Upload_picture");
            this.Upload_picture.Name = "Upload_picture";
            this.Upload_picture.TabStop = false;
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Upload_picture);
            this.Controls.Add(this.main_splitContainer);
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.main_splitContainer.Panel1.ResumeLayout(false);
            this.main_splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.main_splitContainer)).EndInit();
            this.main_splitContainer.ResumeLayout(false);
            this.docBox.ResumeLayout(false);
            this.docBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.preview_splitContainer.Panel1.ResumeLayout(false);
            this.preview_splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.preview_splitContainer)).EndInit();
            this.preview_splitContainer.ResumeLayout(false);
            this.loading_box.ResumeLayout(false);
            this.loading_box.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loading_img)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Upload_picture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer main_splitContainer;
        private System.Windows.Forms.Button Help_btn;
        private System.Windows.Forms.GroupBox docBox;
        private System.Windows.Forms.CheckBox ForIndesign_chkbx;
        private System.Windows.Forms.CheckBox hyp_chkbx;
        private System.Windows.Forms.CheckBox pdashstart_chkbx;
        private System.Windows.Forms.CheckBox newline_chkbx;
        private System.Windows.Forms.CheckBox tab_chkbx;
        private System.Windows.Forms.CheckBox par_chkbx;
        private System.Windows.Forms.CheckBox space_chkbx;
        private System.Windows.Forms.Button start_btn;
        private System.Windows.Forms.Label filename_lbl;
        private System.Windows.Forms.Label size_lbl;
        private System.Windows.Forms.Label page_lbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label type_lbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.SplitContainer preview_splitContainer;
        private System.Windows.Forms.Panel loading_box;
        private System.Windows.Forms.Label progress_lbl;
        private System.Windows.Forms.PictureBox loading_img;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Panel Pagination_Box;
        private System.Windows.Forms.Button Save_btn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox Upload_picture;
    }
}

