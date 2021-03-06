﻿namespace GeneticAlgorithm
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
            this.setOutputDataB = new System.Windows.Forms.Button();
            this.setInputDataB = new System.Windows.Forms.Button();
            this.tablesCB = new System.Windows.Forms.ComboBox();
            this.openFileB = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.showResultMessageB = new System.Windows.Forms.Button();
            this.openStructureB = new System.Windows.Forms.Button();
            this.buildNetworkB = new System.Windows.Forms.Button();
            this.predictionSizeNUD = new System.Windows.Forms.NumericUpDown();
            this.computeDeviceCB = new System.Windows.Forms.ComboBox();
            this.errorThresholdDSNUD = new System.Windows.Forms.NumericUpDown();
            this.errorThresholdTSNUD = new System.Windows.Forms.NumericUpDown();
            this.stagnationDSNUD = new System.Windows.Forms.NumericUpDown();
            this.stagnationTSNUD = new System.Windows.Forms.NumericUpDown();
            this.seedNUD = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.testB = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.inputOutputDGV = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.graphCB = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.predictionDGV = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.graphBuilder1 = new NeuralNetworkWF.GraphBuilder();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.predictionSizeNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorThresholdDSNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorThresholdTSNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stagnationDSNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stagnationTSNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seedNUD)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputOutputDGV)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
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
            // setOutputDataB
            // 
            this.setOutputDataB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.setOutputDataB.Location = new System.Drawing.Point(110, 112);
            this.setOutputDataB.Name = "setOutputDataB";
            this.setOutputDataB.Size = new System.Drawing.Size(96, 23);
            this.setOutputDataB.TabIndex = 3;
            this.setOutputDataB.Text = "Показатель";
            this.setOutputDataB.UseVisualStyleBackColor = true;
            this.setOutputDataB.Click += new System.EventHandler(this.setOutputDataB_Click);
            // 
            // setInputDataB
            // 
            this.setInputDataB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.setInputDataB.Location = new System.Drawing.Point(110, 83);
            this.setInputDataB.Name = "setInputDataB";
            this.setInputDataB.Size = new System.Drawing.Size(96, 23);
            this.setInputDataB.TabIndex = 3;
            this.setInputDataB.Text = "Факторы";
            this.setInputDataB.UseVisualStyleBackColor = true;
            this.setInputDataB.Click += new System.EventHandler(this.setInputDataB_Click);
            // 
            // tablesCB
            // 
            this.tablesCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tablesCB.FormattingEnabled = true;
            this.tablesCB.Location = new System.Drawing.Point(85, 54);
            this.tablesCB.Name = "tablesCB";
            this.tablesCB.Size = new System.Drawing.Size(121, 21);
            this.tablesCB.TabIndex = 2;
            this.tablesCB.SelectedIndexChanged += new System.EventHandler(this.tablesCB_SelectedIndexChanged);
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Выбрать данные: ";
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Открыть файл: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.showResultMessageB);
            this.groupBox2.Controls.Add(this.openStructureB);
            this.groupBox2.Controls.Add(this.buildNetworkB);
            this.groupBox2.Controls.Add(this.predictionSizeNUD);
            this.groupBox2.Controls.Add(this.computeDeviceCB);
            this.groupBox2.Controls.Add(this.errorThresholdDSNUD);
            this.groupBox2.Controls.Add(this.errorThresholdTSNUD);
            this.groupBox2.Controls.Add(this.stagnationDSNUD);
            this.groupBox2.Controls.Add(this.stagnationTSNUD);
            this.groupBox2.Controls.Add(this.seedNUD);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 168);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(212, 270);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Настройка нейронной сети";
            // 
            // showResultMessageB
            // 
            this.showResultMessageB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.showResultMessageB.Enabled = false;
            this.showResultMessageB.Location = new System.Drawing.Point(6, 241);
            this.showResultMessageB.Name = "showResultMessageB";
            this.showResultMessageB.Size = new System.Drawing.Size(200, 23);
            this.showResultMessageB.TabIndex = 5;
            this.showResultMessageB.Text = "Показать результаты";
            this.showResultMessageB.UseVisualStyleBackColor = true;
            this.showResultMessageB.Click += new System.EventHandler(this.showResultMessageB_Click);
            // 
            // openStructureB
            // 
            this.openStructureB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.openStructureB.Location = new System.Drawing.Point(6, 167);
            this.openStructureB.Name = "openStructureB";
            this.openStructureB.Size = new System.Drawing.Size(200, 23);
            this.openStructureB.TabIndex = 4;
            this.openStructureB.Text = "Параметры";
            this.openStructureB.UseVisualStyleBackColor = true;
            this.openStructureB.Click += new System.EventHandler(this.openStructureB_Click);
            // 
            // buildNetworkB
            // 
            this.buildNetworkB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buildNetworkB.Location = new System.Drawing.Point(6, 196);
            this.buildNetworkB.Name = "buildNetworkB";
            this.buildNetworkB.Size = new System.Drawing.Size(200, 39);
            this.buildNetworkB.TabIndex = 3;
            this.buildNetworkB.Text = "Построить нейронную сеть";
            this.buildNetworkB.UseVisualStyleBackColor = true;
            this.buildNetworkB.Click += new System.EventHandler(this.buildNetworkB_Click);
            // 
            // predictionSizeNUD
            // 
            this.predictionSizeNUD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.predictionSizeNUD.Location = new System.Drawing.Point(131, 107);
            this.predictionSizeNUD.Name = "predictionSizeNUD";
            this.predictionSizeNUD.Size = new System.Drawing.Size(75, 20);
            this.predictionSizeNUD.TabIndex = 1;
            this.predictionSizeNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.predictionSizeNUD.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // computeDeviceCB
            // 
            this.computeDeviceCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.computeDeviceCB.FormattingEnabled = true;
            this.computeDeviceCB.Location = new System.Drawing.Point(85, 133);
            this.computeDeviceCB.Name = "computeDeviceCB";
            this.computeDeviceCB.Size = new System.Drawing.Size(121, 21);
            this.computeDeviceCB.TabIndex = 2;
            this.computeDeviceCB.SelectedIndexChanged += new System.EventHandler(this.computeDeviceCB_SelectedIndexChanged);
            // 
            // errorThresholdDSNUD
            // 
            this.errorThresholdDSNUD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.errorThresholdDSNUD.DecimalPlaces = 3;
            this.errorThresholdDSNUD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.errorThresholdDSNUD.Location = new System.Drawing.Point(131, 263);
            this.errorThresholdDSNUD.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.errorThresholdDSNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.errorThresholdDSNUD.Name = "errorThresholdDSNUD";
            this.errorThresholdDSNUD.Size = new System.Drawing.Size(75, 20);
            this.errorThresholdDSNUD.TabIndex = 1;
            this.errorThresholdDSNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.errorThresholdDSNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.errorThresholdDSNUD.Visible = false;
            // 
            // errorThresholdTSNUD
            // 
            this.errorThresholdTSNUD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.errorThresholdTSNUD.DecimalPlaces = 3;
            this.errorThresholdTSNUD.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.errorThresholdTSNUD.Location = new System.Drawing.Point(131, 81);
            this.errorThresholdTSNUD.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.errorThresholdTSNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.errorThresholdTSNUD.Name = "errorThresholdTSNUD";
            this.errorThresholdTSNUD.Size = new System.Drawing.Size(75, 20);
            this.errorThresholdTSNUD.TabIndex = 1;
            this.errorThresholdTSNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.errorThresholdTSNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            // 
            // stagnationDSNUD
            // 
            this.stagnationDSNUD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stagnationDSNUD.Location = new System.Drawing.Point(131, 211);
            this.stagnationDSNUD.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.stagnationDSNUD.Name = "stagnationDSNUD";
            this.stagnationDSNUD.Size = new System.Drawing.Size(75, 20);
            this.stagnationDSNUD.TabIndex = 1;
            this.stagnationDSNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.stagnationDSNUD.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.stagnationDSNUD.Visible = false;
            // 
            // stagnationTSNUD
            // 
            this.stagnationTSNUD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stagnationTSNUD.Location = new System.Drawing.Point(131, 55);
            this.stagnationTSNUD.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.stagnationTSNUD.Name = "stagnationTSNUD";
            this.stagnationTSNUD.Size = new System.Drawing.Size(75, 20);
            this.stagnationTSNUD.TabIndex = 1;
            this.stagnationTSNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.stagnationTSNUD.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // seedNUD
            // 
            this.seedNUD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.seedNUD.Location = new System.Drawing.Point(131, 29);
            this.seedNUD.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.seedNUD.Name = "seedNUD";
            this.seedNUD.Size = new System.Drawing.Size(75, 20);
            this.seedNUD.TabIndex = 1;
            this.seedNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.seedNUD.Value = new decimal(new int[] {
            2017,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 265);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(93, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Ошибка (выход): ";
            this.label10.Visible = false;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 136);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(73, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Устройство: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(102, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Размер прогноза: ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 83);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Ошибка: ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 213);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Стагнация (выход): ";
            this.label8.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Стагнация: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Генератор чисел:  ";
            // 
            // testB
            // 
            this.testB.Location = new System.Drawing.Point(12, 500);
            this.testB.Name = "testB";
            this.testB.Size = new System.Drawing.Size(85, 23);
            this.testB.TabIndex = 4;
            this.testB.Text = "Test Button";
            this.testB.UseVisualStyleBackColor = true;
            this.testB.Visible = false;
            this.testB.Click += new System.EventHandler(this.testB_Click);
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
            this.tabControl1.Size = new System.Drawing.Size(772, 538);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.inputOutputDGV);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(764, 512);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Исходные данные";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // inputOutputDGV
            // 
            this.inputOutputDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputOutputDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.inputOutputDGV.Location = new System.Drawing.Point(6, 6);
            this.inputOutputDGV.Name = "inputOutputDGV";
            this.inputOutputDGV.Size = new System.Drawing.Size(752, 500);
            this.inputOutputDGV.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.graphBuilder1);
            this.tabPage2.Controls.Add(this.graphCB);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(764, 512);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Графики";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // graphCB
            // 
            this.graphCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.graphCB.FormattingEnabled = true;
            this.graphCB.Location = new System.Drawing.Point(387, 6);
            this.graphCB.Name = "graphCB";
            this.graphCB.Size = new System.Drawing.Size(371, 21);
            this.graphCB.TabIndex = 2;
            this.graphCB.SelectedIndexChanged += new System.EventHandler(this.graphCB_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "График: ";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.predictionDGV);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(764, 512);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Прогноз данных";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // predictionDGV
            // 
            this.predictionDGV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.predictionDGV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.predictionDGV.Location = new System.Drawing.Point(6, 6);
            this.predictionDGV.Name = "predictionDGV";
            this.predictionDGV.Size = new System.Drawing.Size(752, 500);
            this.predictionDGV.TabIndex = 0;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // graphBuilder1
            // 
            this.graphBuilder1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.graphBuilder1.Location = new System.Drawing.Point(6, 33);
            this.graphBuilder1.MinimumSize = new System.Drawing.Size(100, 100);
            this.graphBuilder1.Name = "graphBuilder1";
            this.graphBuilder1.Size = new System.Drawing.Size(752, 473);
            this.graphBuilder1.TabIndex = 0;
            this.graphBuilder1.Text = "graphBuilder1";
            this.graphBuilder1.Click += new System.EventHandler(this.graphBuilder1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 563);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.testB);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Генетический алгоритм обучения нейронной сети";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.predictionSizeNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorThresholdDSNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorThresholdTSNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stagnationDSNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stagnationTSNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seedNUD)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.inputOutputDGV)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
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
        private System.Windows.Forms.NumericUpDown stagnationTSNUD;
        private System.Windows.Forms.NumericUpDown seedNUD;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView inputOutputDGV;
        private NeuralNetworkWF.GraphBuilder graphBuilder1;
        private System.Windows.Forms.DataGridView predictionDGV;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.NumericUpDown predictionSizeNUD;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox graphCB;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown stagnationDSNUD;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown errorThresholdTSNUD;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown errorThresholdDSNUD;
        private System.Windows.Forms.ComboBox computeDeviceCB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button openStructureB;
        private System.Windows.Forms.Button testB;
        private System.Windows.Forms.Button showResultMessageB;
    }
}

