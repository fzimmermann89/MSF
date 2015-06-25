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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
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
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.betaUpDown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.sigmaUpDown = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.deltaUpDown = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.epsilonUpDown = new System.Windows.Forms.NumericUpDown();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmsChart)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.betaUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sigmaUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deltaUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.epsilonUpDown)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // elementHost1
            // 
            this.elementHost1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elementHost1.BackColor = System.Drawing.Color.Black;
            this.elementHost1.Location = new System.Drawing.Point(133, 6);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(599, 314);
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
            this.label1.Location = new System.Drawing.Point(12, 224);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Adjecency Matrix";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 412);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "gefundene Cluster";
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
            1000000,
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
            chartArea3.Name = "ChartArea1";
            this.rmsChart.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.rmsChart.Legends.Add(legend3);
            this.rmsChart.Location = new System.Drawing.Point(10, 340);
            this.rmsChart.Name = "rmsChart";
            this.rmsChart.Size = new System.Drawing.Size(722, 189);
            this.rmsChart.TabIndex = 15;
            this.rmsChart.Text = "chart1";
            // 
            // clusterBox
            // 
            this.clusterBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.clusterBox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.clusterBox.Location = new System.Drawing.Point(12, 428);
            this.clusterBox.Name = "clusterBox";
            this.clusterBox.ReadOnly = true;
            this.clusterBox.Size = new System.Drawing.Size(133, 149);
            this.clusterBox.TabIndex = 16;
            this.clusterBox.Text = "";
            // 
            // matrixBox
            // 
            this.matrixBox.Location = new System.Drawing.Point(12, 252);
            this.matrixBox.Name = "matrixBox";
            this.matrixBox.Size = new System.Drawing.Size(133, 153);
            this.matrixBox.TabIndex = 17;
            this.matrixBox.Text = resources.GetString("matrixBox.Text");
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.epsilonUpDown);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.deltaUpDown);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.sigmaUpDown);
            this.groupBox1.Controls.Add(this.betaUpDown);
            this.groupBox1.Location = new System.Drawing.Point(12, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(133, 134);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Settings";
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
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(9, 99);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 19);
            this.label12.TabIndex = 26;
            this.label12.Text = "ε =";
            // 
            // epsilonUpDown
            // 
            this.epsilonUpDown.DecimalPlaces = 3;
            this.epsilonUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.epsilonUpDown.Location = new System.Drawing.Point(45, 101);
            this.epsilonUpDown.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.epsilonUpDown.Name = "epsilonUpDown";
            this.epsilonUpDown.Size = new System.Drawing.Size(51, 20);
            this.epsilonUpDown.TabIndex = 25;
            this.epsilonUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.epsilonUpDown.ValueChanged += new System.EventHandler(this.epsilonUpDown_ValueChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(151, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(754, 561);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage1
            // 
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
            this.tabPage1.Size = new System.Drawing.Size(746, 535);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Graph View";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(746, 535);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Kevin";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rmsChart)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.betaUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sigmaUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deltaUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.epsilonUpDown)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
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
        private System.Windows.Forms.NumericUpDown epsilonUpDown;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown deltaUpDown;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown sigmaUpDown;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;

       }
}