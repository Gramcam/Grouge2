namespace GrogueTheSecondOne
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
            lstPlayArea = new ListBox();
            btnTestChange = new Button();
            SuspendLayout();
            // 
            // lstPlayArea
            // 
            lstPlayArea.BackColor = Color.Black;
            lstPlayArea.Font = new Font("Consolas", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lstPlayArea.ForeColor = Color.DarkOrange;
            lstPlayArea.ItemHeight = 32;
            lstPlayArea.Location = new Point(0, 0);
            lstPlayArea.Name = "lstPlayArea";
            lstPlayArea.Size = new Size(755, 452);
            lstPlayArea.TabIndex = 0;
            // 
            // btnTestChange
            // 
            btnTestChange.Location = new Point(888, 274);
            btnTestChange.Name = "btnTestChange";
            btnTestChange.Size = new Size(75, 23);
            btnTestChange.TabIndex = 1;
            btnTestChange.Text = "button1";
            btnTestChange.UseVisualStyleBackColor = true;
            btnTestChange.Click += btnTestChange_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1060, 612);
            Controls.Add(btnTestChange);
            Controls.Add(lstPlayArea);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListBox lstPlayArea;
        private Button btnTestChange;
    }
}