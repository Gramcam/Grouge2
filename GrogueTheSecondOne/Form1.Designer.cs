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
            label1 = new Label();
            lblHealth = new Label();
            lblTreasures = new Label();
            SuspendLayout();
            // 
            // lstPlayArea
            // 
            lstPlayArea.BackColor = Color.Black;
            lstPlayArea.BorderStyle = BorderStyle.None;
            lstPlayArea.Font = new Font("Consolas", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lstPlayArea.ForeColor = Color.DarkOrange;
            lstPlayArea.ItemHeight = 32;
            lstPlayArea.Location = new Point(170, -4);
            lstPlayArea.Name = "lstPlayArea";
            lstPlayArea.SelectionMode = SelectionMode.None;
            lstPlayArea.Size = new Size(761, 384);
            lstPlayArea.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Consolas", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.ForeColor = Color.DarkOrange;
            label1.Location = new Point(57, 383);
            label1.Name = "label1";
            label1.Size = new Size(1004, 64);
            label1.TabIndex = 1;
            label1.Text = "Move using the number pad direction keys (Numpad on)\r\nTry to collect as many treasures as you can while avoiding N-emies";
            // 
            // lblHealth
            // 
            lblHealth.AutoSize = true;
            lblHealth.Font = new Font("Consolas", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblHealth.ForeColor = Color.DarkOrange;
            lblHealth.Location = new Point(12, 12);
            lblHealth.Name = "lblHealth";
            lblHealth.Size = new Size(89, 32);
            lblHealth.TabIndex = 2;
            lblHealth.Text = "Hp: 3";
            // 
            // lblTreasures
            // 
            lblTreasures.AutoSize = true;
            lblTreasures.Font = new Font("Consolas", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            lblTreasures.ForeColor = Color.DarkOrange;
            lblTreasures.Location = new Point(952, 12);
            lblTreasures.Name = "lblTreasures";
            lblTreasures.Size = new Size(179, 32);
            lblTreasures.TabIndex = 3;
            lblTreasures.Text = "Treasure: 0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaptionText;
            ClientSize = new Size(1143, 456);
            Controls.Add(lblTreasures);
            Controls.Add(lblHealth);
            Controls.Add(label1);
            Controls.Add(lstPlayArea);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            KeyDown += Form1_KeyDown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstPlayArea;
        private Label label1;
        private Label lblHealth;
        private Label lblTreasures;
    }
}