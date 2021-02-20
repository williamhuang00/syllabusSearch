namespace RubricSearch
{
    partial class Form1
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
            this.lblKeyWord = new System.Windows.Forms.Label();
            this.txtKeyWord = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.lstbFolder = new System.Windows.Forms.ListBox();
            this.txtDirectory = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblKeyWord
            // 
            this.lblKeyWord.AutoSize = true;
            this.lblKeyWord.Location = new System.Drawing.Point(12, 22);
            this.lblKeyWord.Name = "lblKeyWord";
            this.lblKeyWord.Size = new System.Drawing.Size(149, 13);
            this.lblKeyWord.TabIndex = 0;
            this.lblKeyWord.Text = "Enter a key word to search for";
            // 
            // txtKeyWord
            // 
            this.txtKeyWord.Location = new System.Drawing.Point(167, 19);
            this.txtKeyWord.Name = "txtKeyWord";
            this.txtKeyWord.Size = new System.Drawing.Size(100, 20);
            this.txtKeyWord.TabIndex = 1;
            this.txtKeyWord.TextChanged += new System.EventHandler(this.txtKeyWord_TextChanged);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(500, 456);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(15, 212);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(560, 238);
            this.txtOutput.TabIndex = 4;
            // 
            // lstbFolder
            // 
            this.lstbFolder.FormattingEnabled = true;
            this.lstbFolder.Location = new System.Drawing.Point(15, 126);
            this.lstbFolder.Name = "lstbFolder";
            this.lstbFolder.Size = new System.Drawing.Size(560, 69);
            this.lstbFolder.TabIndex = 5;
            this.lstbFolder.SelectedIndexChanged += new System.EventHandler(this.lstbFolder_SelectedIndexChanged);
            // 
            // txtDirectory
            // 
            this.txtDirectory.Location = new System.Drawing.Point(15, 92);
            this.txtDirectory.Name = "txtDirectory";
            this.txtDirectory.Size = new System.Drawing.Size(146, 23);
            this.txtDirectory.TabIndex = 6;
            this.txtDirectory.Text = "Add Directory";
            this.txtDirectory.UseVisualStyleBackColor = true;
            this.txtDirectory.Click += new System.EventHandler(this.txtDirectory_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 491);
            this.Controls.Add(this.txtDirectory);
            this.Controls.Add(this.lstbFolder);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtKeyWord);
            this.Controls.Add(this.lblKeyWord);
            this.Name = "Form1";
            this.Text = "SyllabusSearch";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblKeyWord;
        private System.Windows.Forms.TextBox txtKeyWord;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.ListBox lstbFolder;
        private System.Windows.Forms.Button txtDirectory;
    }
}

