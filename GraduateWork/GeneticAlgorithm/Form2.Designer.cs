namespace GeneticAlgorithm
{
    partial class Form2
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.normalizationMaxNUD = new System.Windows.Forms.NumericUpDown();
            this.normalizationMinNUD = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.activationForOutputCB = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.activationForHiddenCB = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.hiddenLayersTB = new System.Windows.Forms.TextBox();
            this.saveB = new System.Windows.Forms.Button();
            this.cancelB = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.normalizationMaxNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.normalizationMinNUD)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.normalizationMaxNUD);
            this.groupBox1.Controls.Add(this.normalizationMinNUD);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.hiddenLayersTB);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(240, 242);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Структура нейросети";
            // 
            // normalizationMaxNUD
            // 
            this.normalizationMaxNUD.DecimalPlaces = 1;
            this.normalizationMaxNUD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.normalizationMaxNUD.Location = new System.Drawing.Point(129, 44);
            this.normalizationMaxNUD.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.normalizationMaxNUD.Minimum = new decimal(new int[] {
            6,
            0,
            0,
            65536});
            this.normalizationMaxNUD.Name = "normalizationMaxNUD";
            this.normalizationMaxNUD.Size = new System.Drawing.Size(51, 20);
            this.normalizationMaxNUD.TabIndex = 4;
            this.normalizationMaxNUD.Value = new decimal(new int[] {
            7,
            0,
            0,
            65536});
            // 
            // normalizationMinNUD
            // 
            this.normalizationMinNUD.DecimalPlaces = 1;
            this.normalizationMinNUD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.normalizationMinNUD.Location = new System.Drawing.Point(36, 44);
            this.normalizationMinNUD.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            65536});
            this.normalizationMinNUD.Name = "normalizationMinNUD";
            this.normalizationMinNUD.Size = new System.Drawing.Size(51, 20);
            this.normalizationMinNUD.TabIndex = 4;
            this.normalizationMinNUD.Value = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.activationForOutputCB);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.activationForHiddenCB);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(3, 120);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(231, 116);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Функции активации: ";
            // 
            // activationForOutputCB
            // 
            this.activationForOutputCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.activationForOutputCB.FormattingEnabled = true;
            this.activationForOutputCB.Items.AddRange(new object[] {
            "Тождество (f(x)=x)",
            "Логист. сигмоид",
            "Гиперб. тангенс"});
            this.activationForOutputCB.Location = new System.Drawing.Point(84, 73);
            this.activationForOutputCB.Name = "activationForOutputCB";
            this.activationForOutputCB.Size = new System.Drawing.Size(141, 21);
            this.activationForOutputCB.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(118, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Для выходного слоя: ";
            // 
            // activationForHiddenCB
            // 
            this.activationForHiddenCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.activationForHiddenCB.FormattingEnabled = true;
            this.activationForHiddenCB.Items.AddRange(new object[] {
            "Тождество (f(x)=x)",
            "Логист. сигмоид",
            "Гиперб. тангенс"});
            this.activationForHiddenCB.Location = new System.Drawing.Point(84, 32);
            this.activationForHiddenCB.Name = "activationForHiddenCB";
            this.activationForHiddenCB.Size = new System.Drawing.Size(141, 21);
            this.activationForHiddenCB.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Для скрытых слоев: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(100, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "До ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "От ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Интервал нормализации: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Размеры скрытых слоев: ";
            // 
            // hiddenLayersTB
            // 
            this.hiddenLayersTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hiddenLayersTB.Location = new System.Drawing.Point(9, 92);
            this.hiddenLayersTB.Name = "hiddenLayersTB";
            this.hiddenLayersTB.Size = new System.Drawing.Size(225, 20);
            this.hiddenLayersTB.TabIndex = 0;
            // 
            // saveB
            // 
            this.saveB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveB.Location = new System.Drawing.Point(99, 259);
            this.saveB.Name = "saveB";
            this.saveB.Size = new System.Drawing.Size(74, 23);
            this.saveB.TabIndex = 1;
            this.saveB.Text = "ОК";
            this.saveB.UseVisualStyleBackColor = true;
            this.saveB.Click += new System.EventHandler(this.saveB_Click);
            // 
            // cancelB
            // 
            this.cancelB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelB.Location = new System.Drawing.Point(179, 259);
            this.cancelB.Name = "cancelB";
            this.cancelB.Size = new System.Drawing.Size(74, 23);
            this.cancelB.TabIndex = 1;
            this.cancelB.Text = "Отмена";
            this.cancelB.UseVisualStyleBackColor = true;
            this.cancelB.Click += new System.EventHandler(this.cancelB_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 294);
            this.ControlBox = false;
            this.Controls.Add(this.cancelB);
            this.Controls.Add(this.saveB);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form2";
            this.Text = "Параметры";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.normalizationMaxNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.normalizationMinNUD)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox activationForOutputCB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox activationForHiddenCB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox hiddenLayersTB;
        private System.Windows.Forms.Button saveB;
        private System.Windows.Forms.Button cancelB;
        private System.Windows.Forms.NumericUpDown normalizationMaxNUD;
        private System.Windows.Forms.NumericUpDown normalizationMinNUD;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}