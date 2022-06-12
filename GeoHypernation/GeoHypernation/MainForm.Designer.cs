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
            this.loading_img = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.Save_btn = new System.Windows.Forms.Button();
            this.Pagination_Box = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.type_lbl = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.page_lbl = new System.Windows.Forms.Label();
            this.size_lbl = new System.Windows.Forms.Label();
            this.filename_lbl = new System.Windows.Forms.Label();
            this.start_btn = new System.Windows.Forms.Button();
            this.close_btn = new System.Windows.Forms.Label();
            this.space_chkbx = new System.Windows.Forms.CheckBox();
            this.hypcl_chkbx = new System.Windows.Forms.CheckBox();
            this.par_chkbx = new System.Windows.Forms.CheckBox();
            this.tab_chkbx = new System.Windows.Forms.CheckBox();
            this.newline_chkbx = new System.Windows.Forms.CheckBox();
            this.pdashstart_chkbx = new System.Windows.Forms.CheckBox();
            this.hyp_chkbx = new System.Windows.Forms.CheckBox();
            this.ForIndesign_chkbx = new System.Windows.Forms.CheckBox();
            this.progress_lbl = new System.Windows.Forms.Label();
            this.docBox = new System.Windows.Forms.GroupBox();
            this.progress_groupBox = new System.Windows.Forms.GroupBox();
            this.loading_picture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.loading_img)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.docBox.SuspendLayout();
            this.progress_groupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loading_picture)).BeginInit();
            this.SuspendLayout();
            // 
            // loading_img
            // 
            resources.ApplyResources(this.loading_img, "loading_img");
            this.loading_img.Image = global::GeoHypernation.Properties.Resources._200;
            this.loading_img.Name = "loading_img";
            this.loading_img.TabStop = false;
            this.loading_img.UseWaitCursor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // webBrowser
            // 
            this.webBrowser.AllowWebBrowserDrop = false;
            resources.ApplyResources(this.webBrowser, "webBrowser");
            this.webBrowser.Name = "webBrowser";
            // 
            // Save_btn
            // 
            resources.ApplyResources(this.Save_btn, "Save_btn");
            this.Save_btn.Name = "Save_btn";
            this.Save_btn.UseVisualStyleBackColor = true;
            this.Save_btn.Click += new System.EventHandler(this.Save_btn_Click);
            // 
            // Pagination_Box
            // 
            resources.ApplyResources(this.Pagination_Box, "Pagination_Box");
            this.Pagination_Box.Name = "Pagination_Box";
            this.Pagination_Box.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GeoHypernation.Properties.Resources.microsoft_word_document_icon;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // type_lbl
            // 
            resources.ApplyResources(this.type_lbl, "type_lbl");
            this.type_lbl.Name = "type_lbl";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // page_lbl
            // 
            resources.ApplyResources(this.page_lbl, "page_lbl");
            this.page_lbl.Name = "page_lbl";
            // 
            // size_lbl
            // 
            resources.ApplyResources(this.size_lbl, "size_lbl");
            this.size_lbl.Name = "size_lbl";
            // 
            // filename_lbl
            // 
            resources.ApplyResources(this.filename_lbl, "filename_lbl");
            this.filename_lbl.Name = "filename_lbl";
            // 
            // start_btn
            // 
            resources.ApplyResources(this.start_btn, "start_btn");
            this.start_btn.Name = "start_btn";
            this.start_btn.UseVisualStyleBackColor = true;
            this.start_btn.Click += new System.EventHandler(this.Start_btn_Click);
            // 
            // close_btn
            // 
            resources.ApplyResources(this.close_btn, "close_btn");
            this.close_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close_btn.Name = "close_btn";
            this.close_btn.Click += new System.EventHandler(this.Close_btn_Click);
            // 
            // space_chkbx
            // 
            resources.ApplyResources(this.space_chkbx, "space_chkbx");
            this.space_chkbx.Checked = true;
            this.space_chkbx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.space_chkbx.Name = "space_chkbx";
            this.space_chkbx.UseVisualStyleBackColor = true;
            // 
            // hypcl_chkbx
            // 
            resources.ApplyResources(this.hypcl_chkbx, "hypcl_chkbx");
            this.hypcl_chkbx.Name = "hypcl_chkbx";
            this.hypcl_chkbx.UseVisualStyleBackColor = true;
            // 
            // par_chkbx
            // 
            resources.ApplyResources(this.par_chkbx, "par_chkbx");
            this.par_chkbx.Name = "par_chkbx";
            this.par_chkbx.UseVisualStyleBackColor = true;
            // 
            // tab_chkbx
            // 
            resources.ApplyResources(this.tab_chkbx, "tab_chkbx");
            this.tab_chkbx.Name = "tab_chkbx";
            this.tab_chkbx.UseVisualStyleBackColor = true;
            // 
            // newline_chkbx
            // 
            resources.ApplyResources(this.newline_chkbx, "newline_chkbx");
            this.newline_chkbx.Name = "newline_chkbx";
            this.newline_chkbx.UseVisualStyleBackColor = true;
            // 
            // pdashstart_chkbx
            // 
            resources.ApplyResources(this.pdashstart_chkbx, "pdashstart_chkbx");
            this.pdashstart_chkbx.Name = "pdashstart_chkbx";
            this.pdashstart_chkbx.UseVisualStyleBackColor = true;
            // 
            // hyp_chkbx
            // 
            resources.ApplyResources(this.hyp_chkbx, "hyp_chkbx");
            this.hyp_chkbx.Checked = true;
            this.hyp_chkbx.CheckState = System.Windows.Forms.CheckState.Checked;
            this.hyp_chkbx.Name = "hyp_chkbx";
            this.hyp_chkbx.UseVisualStyleBackColor = true;
            // 
            // ForIndesign_chkbx
            // 
            resources.ApplyResources(this.ForIndesign_chkbx, "ForIndesign_chkbx");
            this.ForIndesign_chkbx.Name = "ForIndesign_chkbx";
            this.ForIndesign_chkbx.UseVisualStyleBackColor = true;
            this.ForIndesign_chkbx.CheckedChanged += new System.EventHandler(this.ForIndesign_chkbx_CheckedChanged);
            // 
            // progress_lbl
            // 
            resources.ApplyResources(this.progress_lbl, "progress_lbl");
            this.progress_lbl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(43)))), ((int)(((byte)(34)))));
            this.progress_lbl.ForeColor = System.Drawing.SystemColors.Info;
            this.progress_lbl.Name = "progress_lbl";
            this.progress_lbl.UseWaitCursor = true;
            // 
            // docBox
            // 
            this.docBox.Controls.Add(this.ForIndesign_chkbx);
            this.docBox.Controls.Add(this.hyp_chkbx);
            this.docBox.Controls.Add(this.pdashstart_chkbx);
            this.docBox.Controls.Add(this.newline_chkbx);
            this.docBox.Controls.Add(this.tab_chkbx);
            this.docBox.Controls.Add(this.par_chkbx);
            this.docBox.Controls.Add(this.hypcl_chkbx);
            this.docBox.Controls.Add(this.space_chkbx);
            this.docBox.Controls.Add(this.close_btn);
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
            // progress_groupBox
            // 
            resources.ApplyResources(this.progress_groupBox, "progress_groupBox");
            this.progress_groupBox.Controls.Add(this.progress_lbl);
            this.progress_groupBox.Controls.Add(this.loading_img);
            this.progress_groupBox.Name = "progress_groupBox";
            this.progress_groupBox.TabStop = false;
            this.progress_groupBox.UseWaitCursor = true;
            // 
            // loading_picture
            // 
            resources.ApplyResources(this.loading_picture, "loading_picture");
            this.loading_picture.Name = "loading_picture";
            this.loading_picture.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.progress_groupBox);
            this.Controls.Add(this.Pagination_Box);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.Save_btn);
            this.Controls.Add(this.docBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.loading_picture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Click += new System.EventHandler(this.MainForm_Click);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.loading_img)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.docBox.ResumeLayout(false);
            this.docBox.PerformLayout();
            this.progress_groupBox.ResumeLayout(false);
            this.progress_groupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loading_picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox loading_img;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Button Save_btn;
        private System.Windows.Forms.GroupBox Pagination_Box;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label type_lbl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label page_lbl;
        private System.Windows.Forms.Label size_lbl;
        private System.Windows.Forms.Label filename_lbl;
        private System.Windows.Forms.Button start_btn;
        private System.Windows.Forms.Label close_btn;
        private System.Windows.Forms.CheckBox space_chkbx;
        private System.Windows.Forms.CheckBox hypcl_chkbx;
        private System.Windows.Forms.CheckBox par_chkbx;
        private System.Windows.Forms.CheckBox tab_chkbx;
        private System.Windows.Forms.CheckBox newline_chkbx;
        private System.Windows.Forms.CheckBox pdashstart_chkbx;
        private System.Windows.Forms.CheckBox hyp_chkbx;
        private System.Windows.Forms.CheckBox ForIndesign_chkbx;
        private System.Windows.Forms.Label progress_lbl;
        private System.Windows.Forms.GroupBox docBox;
        private System.Windows.Forms.GroupBox progress_groupBox;
        private System.Windows.Forms.PictureBox loading_picture;
    }
}

