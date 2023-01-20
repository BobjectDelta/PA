namespace Daifugo
{
    partial class GameForm
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
            this.skipBtn = new System.Windows.Forms.Button();
            this.playCardsBtn = new System.Windows.Forms.Button();
            this.movesComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nextPlayerBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // skipBtn
            // 
            this.skipBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.skipBtn.Location = new System.Drawing.Point(12, 350);
            this.skipBtn.Name = "skipBtn";
            this.skipBtn.Size = new System.Drawing.Size(75, 35);
            this.skipBtn.TabIndex = 0;
            this.skipBtn.Text = "Skip";
            this.skipBtn.UseVisualStyleBackColor = true;
            this.skipBtn.Click += new System.EventHandler(this.skipBtn_Click);
            // 
            // playCardsBtn
            // 
            this.playCardsBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.playCardsBtn.Location = new System.Drawing.Point(93, 350);
            this.playCardsBtn.Name = "playCardsBtn";
            this.playCardsBtn.Size = new System.Drawing.Size(140, 35);
            this.playCardsBtn.TabIndex = 1;
            this.playCardsBtn.Text = "Play cards";
            this.playCardsBtn.UseVisualStyleBackColor = true;
            this.playCardsBtn.Click += new System.EventHandler(this.playCardsBtn_Click);
            // 
            // movesComboBox
            // 
            this.movesComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.movesComboBox.FormattingEnabled = true;
            this.movesComboBox.Location = new System.Drawing.Point(816, 350);
            this.movesComboBox.Name = "movesComboBox";
            this.movesComboBox.Size = new System.Drawing.Size(150, 37);
            this.movesComboBox.TabIndex = 2;
            this.movesComboBox.Text = "Moves";
            this.movesComboBox.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Window;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "Players\' cards:\r\n";
            // 
            // nextPlayerBtn
            // 
            this.nextPlayerBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nextPlayerBtn.Location = new System.Drawing.Point(816, 294);
            this.nextPlayerBtn.Name = "nextPlayerBtn";
            this.nextPlayerBtn.Size = new System.Drawing.Size(150, 50);
            this.nextPlayerBtn.TabIndex = 4;
            this.nextPlayerBtn.Text = "Next player";
            this.nextPlayerBtn.UseVisualStyleBackColor = true;
            this.nextPlayerBtn.Click += new System.EventHandler(this.nextPlayerBtn_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Green;
            this.ClientSize = new System.Drawing.Size(978, 544);
            this.Controls.Add(this.nextPlayerBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.movesComboBox);
            this.Controls.Add(this.playCardsBtn);
            this.Controls.Add(this.skipBtn);
            this.Name = "GameForm";
            this.Text = "Game";
            this.Load += new System.EventHandler(this.Game_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button skipBtn;
        private System.Windows.Forms.Button playCardsBtn;
        private System.Windows.Forms.ComboBox movesComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button nextPlayerBtn;
    }
}