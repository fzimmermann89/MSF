namespace ClusterNum
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.layoutButton = new System.Windows.Forms.Button();
            this.initGraphButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.iterationTimer = new System.Windows.Forms.Timer(this.components);
            this.runButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.iterateButton = new System.Windows.Forms.Button();
            this.initIteratorButton = new System.Windows.Forms.Button();
            this.rmsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.clusterBox = new System.Windows.Forms.RichTextBox();
            this.matrixBox = new System.Windows.Forms.RichTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.pertUpDown = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.noiseUpDown = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.deltaUpDown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.sigmaUpDown = new System.Windows.Forms.NumericUpDown();
            this.betaUpDown = new System.Windows.Forms.NumericUpDown();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.betaRmsChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.betaLjapChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.recUpDown = new System.Windows.Forms.NumericUpDown();
            this.preUpDown = new System.Windows.Forms.NumericUpDown();
            this.betaRunButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.stepsUpDown = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.betaMaxUpDown = new System.Windows.Forms.NumericUpDown();
            this.betaMinUpDown = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmsChart)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pertUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.noiseUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deltaUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sigmaUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.betaUpDown)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.betaRmsChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.betaLjapChart)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.preUpDown)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stepsUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.betaMaxUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.betaMinUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // elementHost1
            // 
            this.elementHost1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elementHost1.BackColor = System.Drawing.Color.Transparent;
            this.elementHost1.Location = new System.Drawing.Point(133, 6);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(584, 314);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // layoutButton
            // 
            this.layoutButton.Enabled = false;
            this.layoutButton.Location = new System.Drawing.Point(10, 297);
            this.layoutButton.Name = "layoutButton";
            this.layoutButton.Size = new System.Drawing.Size(83, 23);
            this.layoutButton.TabIndex = 1;
            this.layoutButton.Text = "Relayout";
            this.layoutButton.UseVisualStyleBackColor = true;
            this.layoutButton.Click += new System.EventHandler(this.layoutButton_Click);
            // 
            // initGraphButton
            // 
            this.initGraphButton.Location = new System.Drawing.Point(12, 5);
            this.initGraphButton.Name = "initGraphButton";
            this.initGraphButton.Size = new System.Drawing.Size(75, 23);
            this.initGraphButton.TabIndex = 3;
            this.initGraphButton.Text = "Init Graph";
            this.initGraphButton.UseVisualStyleBackColor = true;
            this.initGraphButton.Click += new System.EventHandler(this.initGraphButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 208);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Adjecency Matrix";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 410);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Cluster";
            // 
            // iterationTimer
            // 
            this.iterationTimer.Interval = 200;
            this.iterationTimer.Tick += new System.EventHandler(this.iterationTimer_Tick);
            // 
            // runButton
            // 
            this.runButton.Enabled = false;
            this.runButton.Location = new System.Drawing.Point(10, 119);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(117, 23);
            this.runButton.TabIndex = 8;
            this.runButton.Text = "Run Simulation";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Timer: ";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown1.Location = new System.Drawing.Point(13, 93);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            6000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(72, 20);
            this.numericUpDown1.TabIndex = 11;
            this.numericUpDown1.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(91, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "ms";
            // 
            // iterateButton
            // 
            this.iterateButton.Enabled = false;
            this.iterateButton.Location = new System.Drawing.Point(10, 35);
            this.iterateButton.Name = "iterateButton";
            this.iterateButton.Size = new System.Drawing.Size(75, 23);
            this.iterateButton.TabIndex = 13;
            this.iterateButton.Text = "Iterate";
            this.iterateButton.UseVisualStyleBackColor = true;
            this.iterateButton.Click += new System.EventHandler(this.iterateButton_Click);
            // 
            // initIteratorButton
            // 
            this.initIteratorButton.Enabled = false;
            this.initIteratorButton.Location = new System.Drawing.Point(10, 6);
            this.initIteratorButton.Name = "initIteratorButton";
            this.initIteratorButton.Size = new System.Drawing.Size(75, 23);
            this.initIteratorButton.TabIndex = 14;
            this.initIteratorButton.Text = "Init Iterator";
            this.initIteratorButton.UseVisualStyleBackColor = true;
            this.initIteratorButton.Click += new System.EventHandler(this.initIteratorButton_Click);
            // 
            // rmsChart
            // 
            this.rmsChart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rmsChart.BackColor = System.Drawing.SystemColors.Control;
            chartArea1.AxisX.Title = "Iteration";
            chartArea1.AxisY.ScrollBar.IsPositionedInside = false;
            chartArea1.AxisY.Title = " Δx";
            chartArea1.BackColor = System.Drawing.SystemColors.Control;
            chartArea1.CursorY.Interval = 0.01D;
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.CursorY.IsUserSelectionEnabled = true;
            chartArea1.Name = "ChartArea1";
            this.rmsChart.ChartAreas.Add(chartArea1);
            legend1.BackColor = System.Drawing.SystemColors.Control;
            legend1.Name = "Legend1";
            this.rmsChart.Legends.Add(legend1);
            this.rmsChart.Location = new System.Drawing.Point(10, 340);
            this.rmsChart.Name = "rmsChart";
            this.rmsChart.Size = new System.Drawing.Size(707, 189);
            this.rmsChart.TabIndex = 15;
            this.rmsChart.Text = "chart1";
            // 
            // clusterBox
            // 
            this.clusterBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.clusterBox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.clusterBox.Location = new System.Drawing.Point(12, 429);
            this.clusterBox.Name = "clusterBox";
            this.clusterBox.ReadOnly = true;
            this.clusterBox.Size = new System.Drawing.Size(148, 148);
            this.clusterBox.TabIndex = 16;
            this.clusterBox.Text = "";
            // 
            // matrixBox
            // 
            this.matrixBox.Location = new System.Drawing.Point(12, 236);
            this.matrixBox.Name = "matrixBox";
            this.matrixBox.Size = new System.Drawing.Size(148, 160);
            this.matrixBox.TabIndex = 17;
            this.matrixBox.Text = resources.GetString("matrixBox.Text");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.pertUpDown);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.noiseUpDown);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.deltaUpDown);
            this.groupBox1.Controls.Add(this.label21);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.sigmaUpDown);
            this.groupBox1.Controls.Add(this.betaUpDown);
            this.groupBox1.Location = new System.Drawing.Point(12, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(133, 169);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Settings";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(9, 130);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(34, 13);
            this.label18.TabIndex = 28;
            this.label18.Text = " Δx(0)";
            // 
            // pertUpDown
            // 
            this.pertUpDown.DecimalPlaces = 3;
            this.pertUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.pertUpDown.Location = new System.Drawing.Point(45, 127);
            this.pertUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.pertUpDown.Name = "pertUpDown";
            this.pertUpDown.Size = new System.Drawing.Size(51, 20);
            this.pertUpDown.TabIndex = 27;
            this.pertUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.pertUpDown.ValueChanged += new System.EventHandler(this.pertUpDown_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 104);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(34, 13);
            this.label12.TabIndex = 26;
            this.label12.Text = "Noise";
            // 
            // noiseUpDown
            // 
            this.noiseUpDown.DecimalPlaces = 3;
            this.noiseUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.noiseUpDown.Location = new System.Drawing.Point(45, 101);
            this.noiseUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.noiseUpDown.Name = "noiseUpDown";
            this.noiseUpDown.Size = new System.Drawing.Size(51, 20);
            this.noiseUpDown.TabIndex = 25;
            this.noiseUpDown.ValueChanged += new System.EventHandler(this.noiseUpDown_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(9, 73);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 19);
            this.label10.TabIndex = 23;
            this.label10.Text = "δ =";
            // 
            // deltaUpDown
            // 
            this.deltaUpDown.DecimalPlaces = 3;
            this.deltaUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.deltaUpDown.Location = new System.Drawing.Point(45, 75);
            this.deltaUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.deltaUpDown.Name = "deltaUpDown";
            this.deltaUpDown.Size = new System.Drawing.Size(51, 20);
            this.deltaUpDown.TabIndex = 22;
            this.deltaUpDown.Value = new decimal(new int[] {
            525,
            0,
            0,
            196608});
            this.deltaUpDown.ValueChanged += new System.EventHandler(this.deltaUpDown_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(102, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 19);
            this.label7.TabIndex = 21;
            this.label7.Text = "π";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(102, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 19);
            this.label6.TabIndex = 12;
            this.label6.Text = "π";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(9, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 19);
            this.label8.TabIndex = 20;
            this.label8.Text = "σ =";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "β =";
            // 
            // sigmaUpDown
            // 
            this.sigmaUpDown.DecimalPlaces = 3;
            this.sigmaUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.sigmaUpDown.Location = new System.Drawing.Point(45, 49);
            this.sigmaUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.sigmaUpDown.Name = "sigmaUpDown";
            this.sigmaUpDown.Size = new System.Drawing.Size(51, 20);
            this.sigmaUpDown.TabIndex = 19;
            this.sigmaUpDown.Value = new decimal(new int[] {
            67,
            0,
            0,
            131072});
            this.sigmaUpDown.ValueChanged += new System.EventHandler(this.sigmaUpDown_ValueChanged);
            // 
            // betaUpDown
            // 
            this.betaUpDown.DecimalPlaces = 3;
            this.betaUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.betaUpDown.Location = new System.Drawing.Point(45, 23);
            this.betaUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.betaUpDown.Name = "betaUpDown";
            this.betaUpDown.Size = new System.Drawing.Size(51, 20);
            this.betaUpDown.TabIndex = 0;
            this.betaUpDown.Value = new decimal(new int[] {
            72,
            0,
            0,
            131072});
            this.betaUpDown.ValueChanged += new System.EventHandler(this.betaUpDown_ValueChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(166, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(739, 561);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.elementHost1);
            this.tabPage1.Controls.Add(this.rmsChart);
            this.tabPage1.Controls.Add(this.layoutButton);
            this.tabPage1.Controls.Add(this.iterateButton);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.initIteratorButton);
            this.tabPage1.Controls.Add(this.numericUpDown1);
            this.tabPage1.Controls.Add(this.runButton);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(731, 535);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Graph View";
            this.tabPage1.Enter += new System.EventHandler(this.tabPage1_Enter);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.betaRunButton);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(731, 535);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "β-Variation";
            this.tabPage2.Enter += new System.EventHandler(this.tabPage2_Enter);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(6, 129);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.betaRmsChart);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.betaLjapChart);
            this.splitContainer1.Size = new System.Drawing.Size(719, 400);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 25;
            // 
            // betaRmsChart
            // 
            this.betaRmsChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.betaRmsChart.BackColor = System.Drawing.Color.Transparent;
            chartArea2.AxisX.Title = "β/π";
            chartArea2.AxisY.ScrollBar.IsPositionedInside = false;
            chartArea2.AxisY.Title = "<RMS>";
            chartArea2.BackColor = System.Drawing.Color.Transparent;
            chartArea2.CursorY.Interval = 0.01D;
            chartArea2.CursorY.IsUserEnabled = true;
            chartArea2.CursorY.IsUserSelectionEnabled = true;
            chartArea2.Name = "ChartArea1";
            this.betaRmsChart.ChartAreas.Add(chartArea2);
            legend2.BackColor = System.Drawing.Color.Transparent;
            legend2.Name = "Legend1";
            this.betaRmsChart.Legends.Add(legend2);
            this.betaRmsChart.Location = new System.Drawing.Point(3, 3);
            this.betaRmsChart.Name = "betaRmsChart";
            this.betaRmsChart.Size = new System.Drawing.Size(716, 189);
            this.betaRmsChart.TabIndex = 21;
            this.betaRmsChart.Text = "chart1";
            // 
            // betaLjapChart
            // 
            this.betaLjapChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.betaLjapChart.BackColor = System.Drawing.Color.Transparent;
            chartArea3.AxisX.Title = "β/π";
            chartArea3.AxisY.Interval = 0.5D;
            chartArea3.AxisY.MajorGrid.Interval = 0.5D;
            chartArea3.AxisY.MajorTickMark.Interval = 0.5D;
            chartArea3.AxisY.Maximum = 1D;
            chartArea3.AxisY.Minimum = -1D;
            chartArea3.AxisY.ScrollBar.IsPositionedInside = false;
            chartArea3.AxisY.Title = "Max. Lyapunov";
            chartArea3.BackColor = System.Drawing.Color.Transparent;
            chartArea3.CursorY.Interval = 0.01D;
            chartArea3.CursorY.IsUserEnabled = true;
            chartArea3.CursorY.IsUserSelectionEnabled = true;
            chartArea3.Name = "ChartArea1";
            this.betaLjapChart.ChartAreas.Add(chartArea3);
            legend3.BackColor = System.Drawing.Color.Transparent;
            legend3.Name = "Legend1";
            this.betaLjapChart.Legends.Add(legend3);
            this.betaLjapChart.Location = new System.Drawing.Point(3, 3);
            this.betaLjapChart.Name = "betaLjapChart";
            this.betaLjapChart.Size = new System.Drawing.Size(716, 189);
            this.betaLjapChart.TabIndex = 22;
            this.betaLjapChart.Text = "chart1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.label20);
            this.groupBox3.Controls.Add(this.recUpDown);
            this.groupBox3.Controls.Add(this.preUpDown);
            this.groupBox3.Location = new System.Drawing.Point(145, 10);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(152, 113);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Time Settings";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(102, 27);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(34, 13);
            this.label17.TabIndex = 24;
            this.label17.Text = "Steps";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(102, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "Steps";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(6, 51);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(27, 13);
            this.label19.TabIndex = 20;
            this.label19.Text = "Rec";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(6, 25);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(23, 13);
            this.label20.TabIndex = 11;
            this.label20.Text = "Pre";
            // 
            // recUpDown
            // 
            this.recUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.recUpDown.Location = new System.Drawing.Point(45, 49);
            this.recUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.recUpDown.Name = "recUpDown";
            this.recUpDown.Size = new System.Drawing.Size(51, 20);
            this.recUpDown.TabIndex = 19;
            this.recUpDown.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // preUpDown
            // 
            this.preUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.preUpDown.Location = new System.Drawing.Point(45, 23);
            this.preUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.preUpDown.Name = "preUpDown";
            this.preUpDown.Size = new System.Drawing.Size(51, 20);
            this.preUpDown.TabIndex = 0;
            this.preUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // betaRunButton
            // 
            this.betaRunButton.Enabled = false;
            this.betaRunButton.Location = new System.Drawing.Point(303, 19);
            this.betaRunButton.Name = "betaRunButton";
            this.betaRunButton.Size = new System.Drawing.Size(113, 23);
            this.betaRunButton.TabIndex = 20;
            this.betaRunButton.Text = "Run Simulation";
            this.betaRunButton.UseVisualStyleBackColor = true;
            this.betaRunButton.Click += new System.EventHandler(this.betaRunButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.stepsUpDown);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.betaMaxUpDown);
            this.groupBox2.Controls.Add(this.betaMinUpDown);
            this.groupBox2.Location = new System.Drawing.Point(6, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(133, 113);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " β-Settings";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(6, 77);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(34, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "Steps";
            // 
            // stepsUpDown
            // 
            this.stepsUpDown.Location = new System.Drawing.Point(45, 75);
            this.stepsUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.stepsUpDown.Name = "stepsUpDown";
            this.stepsUpDown.Size = new System.Drawing.Size(51, 20);
            this.stepsUpDown.TabIndex = 22;
            this.stepsUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(102, 47);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(17, 19);
            this.label13.TabIndex = 21;
            this.label13.Text = "π";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(102, 21);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(17, 19);
            this.label14.TabIndex = 12;
            this.label14.Text = "π";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(6, 51);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(27, 13);
            this.label15.TabIndex = 20;
            this.label15.Text = "Max";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(6, 25);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(24, 13);
            this.label16.TabIndex = 11;
            this.label16.Text = "Min";
            // 
            // betaMaxUpDown
            // 
            this.betaMaxUpDown.DecimalPlaces = 3;
            this.betaMaxUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.betaMaxUpDown.Location = new System.Drawing.Point(45, 49);
            this.betaMaxUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.betaMaxUpDown.Name = "betaMaxUpDown";
            this.betaMaxUpDown.Size = new System.Drawing.Size(51, 20);
            this.betaMaxUpDown.TabIndex = 19;
            this.betaMaxUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // betaMinUpDown
            // 
            this.betaMinUpDown.DecimalPlaces = 3;
            this.betaMinUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.betaMinUpDown.Location = new System.Drawing.Point(45, 23);
            this.betaMinUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.betaMinUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.betaMinUpDown.Name = "betaMinUpDown";
            this.betaMinUpDown.Size = new System.Drawing.Size(51, 20);
            this.betaMinUpDown.TabIndex = 0;
            this.betaMinUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(102, 127);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(17, 19);
            this.label21.TabIndex = 21;
            this.label21.Text = "π";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 585);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.matrixBox);
            this.Controls.Add(this.clusterBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.initGraphButton);
            this.Name = "MainForm";
            this.Text = "ClusterNum";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmsChart)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pertUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.noiseUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deltaUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sigmaUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.betaUpDown)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.betaRmsChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.betaLjapChart)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.recUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.preUpDown)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stepsUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.betaMaxUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.betaMinUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private System.Windows.Forms.Button layoutButton;
        private System.Windows.Forms.Button initGraphButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer iterationTimer;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button iterateButton;
        private System.Windows.Forms.Button initIteratorButton;
        private System.Windows.Forms.DataVisualization.Charting.Chart rmsChart;
        private System.Windows.Forms.RichTextBox clusterBox;
        private System.Windows.Forms.RichTextBox matrixBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown betaUpDown;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown noiseUpDown;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown deltaUpDown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown sigmaUpDown;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataVisualization.Charting.Chart betaLjapChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart betaRmsChart;
        private System.Windows.Forms.Button betaRunButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown stepsUpDown;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown betaMaxUpDown;
        private System.Windows.Forms.NumericUpDown betaMinUpDown;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.NumericUpDown recUpDown;
        private System.Windows.Forms.NumericUpDown preUpDown;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown pertUpDown;
        private System.Windows.Forms.Label label21;

       }
}