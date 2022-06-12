
namespace GeoHypernation
{
    partial class HelpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpForm));
            this.Info_listBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // Info_listBox
            // 
            this.Info_listBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Info_listBox.Location = new System.Drawing.Point(0, 0);
            this.Info_listBox.Name = "Info_listBox";
            this.Info_listBox.ReadOnly = true;
            this.Info_listBox.Size = new System.Drawing.Size(344, 281);
            this.Info_listBox.TabIndex = 1;
            this.Info_listBox.Text = "";
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 281);
            this.Controls.Add(this.Info_listBox);
            this.Cursor = System.Windows.Forms.Cursors.Help;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HelpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "დახმარება";
            this.Load += new System.EventHandler(this.HelpForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox Info_listBox;
    }
}