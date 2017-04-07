namespace GeneticAlgorithm
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileB = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tablesCB = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.setInputDataB = new System.Windows.Forms.Button();
            this.setOutputDataB = new System.Windows.Forms.Button();
            this.buildNetworkB = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.populationSizeNUD = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.inputOutputDGV = new System.Windows.Forms.DataGridView();
            this.graphBuilder1 = new NeuralNetworkWF.GraphBuilder();
            this.predictionDGV = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.populationSizeNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputOutputDGV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.predictionDGV)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.setOutputDataB);
            this.groupBox1.Controls.Add(this.setInputDataB);
            this.groupBox1.Controls.Add(this.tablesCB);
            this.groupBox1.Controls.Add(this.openFileB);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 14);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(212, 148);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Подготовка данных";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox2.Controls.Add(this.numericUpDown2);
            this.groupBox2.Controls.Add(this.populationSizeNUD);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 168);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(212, 224);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Настройка нейронной сети";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(230, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(482, 408);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.inputOutputDGV);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(474, 382);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Исходные данные";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.graphBuilder1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(474, 382);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Графики";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.predictionDGV);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(474, 382);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Прогноз данных";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Открыть файл: ";
            // 
            // openFileB
            // 
            this.openFileB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openFileB.Location = new System.Drawing.Point(131, 21);
            this.openFileB.Name = "openFileB";
            this.openFileB.Size = new System.Drawing.Size(75, 23);
            this.openFileB.TabIndex = 1;
            this.openFileB.Text = "Обзор";
            this.openFileB.UseVisualStyleBackColor = true;
            this.openFileB.Click += new System.EventHandler(this.openFileB_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Лист: ";
            // 
            // tablesCB
            // 
            this.tablesCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tablesCB.FormattingEnabled = true;
            this.tablesCB.Location = new System.Drawing.Point(85, 54);
            this.tablesCB.Name = "tablesCB";
            this.tablesCB.Size = new System.Drawing.Size(121, 21);
            this.tablesCB.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Выбрать данные: ";
            // 
            // setInputDataB
            // 
            this.setInputDataB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.setInputDataB.Location = new System.Drawing.Point(131, 83);
            this.setInputDataB.Name = "setInputDataB";
            this.setInputDataB.Size = new System.Drawing.Size(75, 23);
            this.setInputDataB.TabIndex = 3;
            this.setInputDataB.Text = "Вход";
            this.setInputDataB.UseVisualStyleBackColor = true;
            // 
            // setOutputDataB
            // 
            this.setOutputDataB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.setOutputDataB.Location = new System.Drawing.Point(131, 112);
            this.setOutputDataB.Name = "setOutputDataB";
            this.setOutputDataB.Size = new System.Drawing.Size(75, 23);
            this.setOutputDataB.TabIndex = 3;
            this.setOutputDataB.Text = "Выход";
            this.setOutputDataB.UseVisualStyleBackColor = true;
            // 
            // buildNetworkB
            // 
            this.buildNetworkB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buildNetworkB.Location = new System.Drawing.Point(12, 398);
            this.buildNetworkB.Name = "buildNetworkB";
            this.buildNetworkB.Size = new System.Drawing.Size(199, 23);
            this.buildNetworkB.TabIndex = 3;
            this.buildNetworkB.Text = "Построить нейронную сеть";
            this.buildNetworkB.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Размер популяции:  ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Выходные нейроны:  ";
            // 
            // populationSizeNUD
            // 
            this.populationSizeNUD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.populationSizeNUD.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.populationSizeNUD.Location = new System.Drawing.Point(131, 32);
            this.populationSizeNUD.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.populationSizeNUD.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.populationSizeNUD.Name = "populationSizeNUD";
            this.populationSizeNUD.Size = new System.Drawing.Size(75, 20);
            this.populationSizeNUD.TabIndex = 1;
            this.populationSizeNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.populationSizeNUD.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numericUpDown2.Location = new System.Drawing.Point(131, 65);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(75, 20);
            this.numericUpDown2.TabIndex = 1;
            this.numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // inputOutputDGV
            // 
            this.inputOutputDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputOutputDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inputOutputDGV.Location = new System.Drawing.Point(6, 6);
            this.inputOutputDGV.Name = "inputOutputDGV";
            this.inputOutputDGV.Size = new System.Drawing.Size(462, 370);
            this.inputOutputDGV.TabIndex = 0;
            // 
            // graphBuilder1
            // 
            this.graphBuilder1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphBuilder1.Location = new System.Drawing.Point(6, 6);
            this.graphBuilder1.MinimumSize = new System.Drawing.Size(100, 100);
            this.graphBuilder1.Name = "graphBuilder1";
            this.graphBuilder1.Size = new System.Drawing.Size(462, 370);
            this.graphBuilder1.TabIndex = 0;
            this.graphBuilder1.Text = "graphBuilder1";
            // 
            // predictionDGV
            // 
            this.predictionDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.predictionDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.predictionDGV.Location = new System.Drawing.Point(6, 6);
            this.predictionDGV.Name = "predictionDGV";
            this.predictionDGV.Size = new System.Drawing.Size(462, 370);
            this.predictionDGV.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(724, 433);
            this.Controls.Add(this.buildNetworkB);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Генетический алгоритм обучения нейронной сети";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.populationSizeNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.inputOutputDGV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.predictionDGV)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button openFileB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox tablesCB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button setOutputDataB;
        private System.Windows.Forms.Button setInputDataB;
        private System.Windows.Forms.Button buildNetworkB;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown populationSizeNUD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView inputOutputDGV;
        private NeuralNetworkWF.GraphBuilder graphBuilder1;
        private System.Windows.Forms.DataGridView predictionDGV;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

