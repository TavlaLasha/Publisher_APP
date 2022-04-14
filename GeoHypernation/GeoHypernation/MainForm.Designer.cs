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
            this.docBox = new System.Windows.Forms.GroupBox();
            this.loading_img = new System.Windows.Forms.PictureBox();
            this.close_btn = new System.Windows.Forms.Label();
            this.start_btn = new System.Windows.Forms.Button();
            this.filename_lbl = new System.Windows.Forms.Label();
            this.size_lbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.type_lbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.docBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loading_img)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // docBox
            // 
            this.docBox.Controls.Add(this.loading_img);
            this.docBox.Controls.Add(this.close_btn);
            this.docBox.Controls.Add(this.start_btn);
            this.docBox.Controls.Add(this.filename_lbl);
            this.docBox.Controls.Add(this.size_lbl);
            this.docBox.Controls.Add(this.label3);
            this.docBox.Controls.Add(this.type_lbl);
            this.docBox.Controls.Add(this.label2);
            this.docBox.Controls.Add(this.label1);
            this.docBox.Controls.Add(this.pictureBox1);
            this.docBox.Location = new System.Drawing.Point(11, 4);
            this.docBox.Name = "docBox";
            this.docBox.Size = new System.Drawing.Size(208, 191);
            this.docBox.TabIndex = 0;
            this.docBox.TabStop = false;
            this.docBox.Visible = false;
            // 
            // loading_img
            // 
            this.loading_img.Image = global::GeoHypernation.Properties.Resources._200;
            this.loading_img.Location = new System.Drawing.Point(-15, -4);
            this.loading_img.Name = "loading_img";
            this.loading_img.Size = new System.Drawing.Size(236, 207);
            this.loading_img.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.loading_img.TabIndex = 5;
            this.loading_img.TabStop = false;
            this.loading_img.Visible = false;
            // 
            // close_btn
            // 
            this.close_btn.AutoSize = true;
            this.close_btn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.close_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.close_btn.Location = new System.Drawing.Point(183, 9);
            this.close_btn.Name = "close_btn";
            this.close_btn.Size = new System.Drawing.Size(23, 22);
            this.close_btn.TabIndex = 4;
            this.close_btn.Text = "X";
            this.close_btn.Click += new System.EventHandler(this.close_btn_Click);
            // 
            // start_btn
            // 
            this.start_btn.Location = new System.Drawing.Point(117, 155);
            this.start_btn.Name = "start_btn";
            this.start_btn.Size = new System.Drawing.Size(85, 30);
            this.start_btn.TabIndex = 3;
            this.start_btn.Text = "Start";
            this.start_btn.UseVisualStyleBackColor = true;
            this.start_btn.Click += new System.EventHandler(this.start_btn_Click);
            // 
            // filename_lbl
            // 
            this.filename_lbl.AutoSize = true;
            this.filename_lbl.Location = new System.Drawing.Point(130, 60);
            this.filename_lbl.MaximumSize = new System.Drawing.Size(75, 95);
            this.filename_lbl.Name = "filename_lbl";
            this.filename_lbl.Size = new System.Drawing.Size(19, 13);
            this.filename_lbl.TabIndex = 2;
            this.filename_lbl.Text = "----";
            // 
            // size_lbl
            // 
            this.size_lbl.AutoSize = true;
            this.size_lbl.Location = new System.Drawing.Point(133, 42);
            this.size_lbl.Name = "size_lbl";
            this.size_lbl.Size = new System.Drawing.Size(19, 13);
            this.size_lbl.TabIndex = 2;
            this.size_lbl.Text = "----";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(102, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "File:";
            // 
            // type_lbl
            // 
            this.type_lbl.AutoSize = true;
            this.type_lbl.Location = new System.Drawing.Point(135, 24);
            this.type_lbl.Name = "type_lbl";
            this.type_lbl.Size = new System.Drawing.Size(19, 13);
            this.type_lbl.TabIndex = 2;
            this.type_lbl.Text = "----";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(102, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Size:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(101, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Type:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GeoHypernation.Properties.Resources.microsoft_word_document_icon;
            this.pictureBox1.Location = new System.Drawing.Point(5, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(94, 90);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(44, 73);
            this.label4.MaximumSize = new System.Drawing.Size(140, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 40);
            this.label4.TabIndex = 2;
            this.label4.Text = "Drag and Drop Or Browse Document";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 204);
            this.Controls.Add(this.docBox);
            this.Controls.Add(this.label4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Georgian Hypernation";
            this.Click += new System.EventHandler(this.MainForm_Click);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.docBox.ResumeLayout(false);
            this.docBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loading_img)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox docBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label filename_lbl;
        private System.Windows.Forms.Label size_lbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label type_lbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button start_btn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label close_btn;
        private System.Windows.Forms.PictureBox loading_img;
    }
}

