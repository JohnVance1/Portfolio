namespace ValueEditorTool
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
            this.button1 = new System.Windows.Forms.Button();
            this.PlayerHealthValue = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.playerAttackValue = new System.Windows.Forms.NumericUpDown();
            this.zombieAttackValue = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.zombieHealthValue = new System.Windows.Forms.NumericUpDown();
            this.bossAttackValue = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.bossHealthValue = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.zombieCount = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.PlayerHealthValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerAttackValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zombieAttackValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zombieHealthValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bossAttackValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bossHealthValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zombieCount)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(375, 275);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(141, 54);
            this.button1.TabIndex = 0;
            this.button1.Text = "Confirm Changes";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PlayerHealthValue
            // 
            this.PlayerHealthValue.Location = new System.Drawing.Point(31, 107);
            this.PlayerHealthValue.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.PlayerHealthValue.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.PlayerHealthValue.Name = "PlayerHealthValue";
            this.PlayerHealthValue.Size = new System.Drawing.Size(120, 20);
            this.PlayerHealthValue.TabIndex = 1;
            this.PlayerHealthValue.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Player Health";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(31, 248);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(68, 17);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Unlimited";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(31, 271);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(49, 17);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.Text = "1000";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(31, 294);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(43, 17);
            this.radioButton3.TabIndex = 5;
            this.radioButton3.Text = "500";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 223);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Ammunition";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Ricky vs. Zombies";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Player Attack";
            // 
            // playerAttackValue
            // 
            this.playerAttackValue.Location = new System.Drawing.Point(31, 166);
            this.playerAttackValue.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.playerAttackValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.playerAttackValue.Name = "playerAttackValue";
            this.playerAttackValue.Size = new System.Drawing.Size(120, 20);
            this.playerAttackValue.TabIndex = 9;
            this.playerAttackValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // zombieAttackValue
            // 
            this.zombieAttackValue.Location = new System.Drawing.Point(197, 166);
            this.zombieAttackValue.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.zombieAttackValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.zombieAttackValue.Name = "zombieAttackValue";
            this.zombieAttackValue.Size = new System.Drawing.Size(120, 20);
            this.zombieAttackValue.TabIndex = 13;
            this.zombieAttackValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(194, 150);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Zombie Attack";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(194, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Zombie Health";
            // 
            // zombieHealthValue
            // 
            this.zombieHealthValue.Location = new System.Drawing.Point(197, 107);
            this.zombieHealthValue.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.zombieHealthValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.zombieHealthValue.Name = "zombieHealthValue";
            this.zombieHealthValue.Size = new System.Drawing.Size(120, 20);
            this.zombieHealthValue.TabIndex = 10;
            this.zombieHealthValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // bossAttackValue
            // 
            this.bossAttackValue.Location = new System.Drawing.Point(366, 166);
            this.bossAttackValue.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.bossAttackValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.bossAttackValue.Name = "bossAttackValue";
            this.bossAttackValue.Size = new System.Drawing.Size(120, 20);
            this.bossAttackValue.TabIndex = 17;
            this.bossAttackValue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(363, 150);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Boss Attack";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(363, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(64, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Boss Health";
            // 
            // bossHealthValue
            // 
            this.bossHealthValue.Location = new System.Drawing.Point(366, 107);
            this.bossHealthValue.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.bossHealthValue.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.bossHealthValue.Name = "bossHealthValue";
            this.bossHealthValue.Size = new System.Drawing.Size(120, 20);
            this.bossHealthValue.TabIndex = 14;
            this.bossHealthValue.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(194, 211);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 19;
            this.label9.Text = "Zombie Count";
            // 
            // zombieCount
            // 
            this.zombieCount.Location = new System.Drawing.Point(197, 227);
            this.zombieCount.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.zombieCount.Name = "zombieCount";
            this.zombieCount.Size = new System.Drawing.Size(120, 20);
            this.zombieCount.TabIndex = 18;
            this.zombieCount.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 390);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.zombieCount);
            this.Controls.Add(this.bossAttackValue);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.bossHealthValue);
            this.Controls.Add(this.zombieAttackValue);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.zombieHealthValue);
            this.Controls.Add(this.playerAttackValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PlayerHealthValue);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.PlayerHealthValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.playerAttackValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zombieAttackValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zombieHealthValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bossAttackValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bossHealthValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zombieCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NumericUpDown PlayerHealthValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown playerAttackValue;
        private System.Windows.Forms.NumericUpDown zombieAttackValue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown zombieHealthValue;
        private System.Windows.Forms.NumericUpDown bossAttackValue;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown bossHealthValue;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown zombieCount;
    }
}

