namespace BangUI
{
    partial class frmBang
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
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(76, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "ReCreate";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // frmBang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 866);
            this.Controls.Add(this.button3);
            this.KeyPreview = true;
            this.Name = "frmBang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bang";
            this.Load += new System.EventHandler(this.frmBang_Load);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.frmBang_MouseUp);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.frmBang_MouseDoubleClick);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmBang_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmBang_MouseDown);
            this.Resize += new System.EventHandler(this.frmBang_Resize);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmBang_MouseMove);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmBang_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button3;
    }
}

