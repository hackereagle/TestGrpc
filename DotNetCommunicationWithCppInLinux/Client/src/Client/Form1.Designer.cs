namespace Client
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ptbDisplay = new PictureBox();
            btnGetImage = new Button();
            ((System.ComponentModel.ISupportInitialize)ptbDisplay).BeginInit();
            SuspendLayout();
            // 
            // ptbDisplay
            // 
            ptbDisplay.BackColor = SystemColors.Window;
            ptbDisplay.Location = new Point(23, 22);
            ptbDisplay.Name = "ptbDisplay";
            ptbDisplay.Size = new Size(443, 406);
            ptbDisplay.TabIndex = 0;
            ptbDisplay.TabStop = false;
            // 
            // btnGetImage
            // 
            btnGetImage.Location = new Point(499, 22);
            btnGetImage.Name = "btnGetImage";
            btnGetImage.Size = new Size(89, 30);
            btnGetImage.TabIndex = 1;
            btnGetImage.Text = "Get Image";
            btnGetImage.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(647, 450);
            Controls.Add(btnGetImage);
            Controls.Add(ptbDisplay);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)ptbDisplay).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox ptbDisplay;
        private Button btnGetImage;
    }
}