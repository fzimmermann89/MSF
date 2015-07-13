using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GraphSharp;
using QuickGraph;
using GraphSharp.Controls;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using Accord.Math;


namespace ClusterNum
{
    public partial class MainForm : Form
    {

        private NodeGraph graph;
        private Vertex[] vertices;
        private double[,] adjmatrix;
        private double[,] TMat;
        private int[][] cluster;

        public GraphSharpControl GraphControl { get; set; }

        public NumIterator iterator;
        NumVariator variator;
        Thread variatorThread;
        delegate void callbackDelegate(NumVariator.result result);
        private int stepsDone = 0;

        double beta = 0.72 * Math.PI;
        double sigma = 0.67 * Math.PI;
        double delta = 0.525;
        double noise = 0.0;
        double pertubation = 0.01 * Math.PI;

        Series[] gseries;
        Series[] betarmsseries;
        Series[] betaljapseries;



        public MainForm()
        {
            InitializeComponent();
            Application.EnableVisualStyles();





        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            networkDropdown.SelectedIndex = 1;
        }

        private void layoutButton_Click(object sender, EventArgs e)
        {
            GraphControl.layout.Relayout();
        }

        private void initGraphButton_Click(object sender, EventArgs e)
        {

            graph = new NodeGraph();
            try
            {
                adjmatrix = Helper.MatrixFromString(matrixBox.Text);
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int nodeCount = adjmatrix.GetLength(0);

            vertices = new Vertex[nodeCount];
            for (int k = 0; k < nodeCount; k++)
            {
                vertices[k] = new Vertex(k.ToString(), 1.0, 0);

                graph.AddVertex(vertices[k]);
            }

            //dreadnaut für orbits starten
            string dreadnautcmd = "n=" + nodeCount + " g ";
            for (int irow = 0; irow < nodeCount; irow++)
            {
                for (int icol = 0; icol < nodeCount; icol++)
                {
                    if (adjmatrix[irow, icol] != 0)
                    {
                        graph.AddEdge(new Edge<Vertex>(vertices[irow], vertices[icol]));
                        dreadnautcmd += icol + " ";
                    }
                }
                dreadnautcmd += ";";
            }
            dreadnautcmd += "x o q";
            string path = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "dreadnaut.exe");
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = path;
            Process process = new Process();
            process.StartInfo = startInfo;
            try
            {
                System.IO.File.WriteAllBytes(path, ClusterNum.Properties.Resources.dreadnaut);
                process.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("dreadnaut konnte nicht gestartet werden", "dreadnaut-Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            process.StandardInput.WriteLine(dreadnautcmd);
            process.WaitForExit();
            System.IO.File.Delete(path);
            string ergebnis = process.StandardOutput.ReadToEnd();

            //cluster auslesen
            cluster = Helper.dreadnaut2cluster(ergebnis);

            rmsChart.Series.Clear();
            gseries = new Series[nodeCount];
            clusterBox.Text = "";

            for (int nodenum = 0; nodenum < nodeCount; nodenum++)
            {
                gseries[nodenum] = new Series();
                gseries[nodenum].IsVisibleInLegend = false;
                gseries[nodenum].ChartArea = "ChartArea1";
                rmsChart.Series.Add(gseries[nodenum]);
            }

            for (int i = 0; i < cluster.Length; i++)
            {
                clusterBox.Text += "Cluster " + i + " mit Knoten: ";
                foreach (int k in cluster[i])
                {
                    clusterBox.Text += +k + " ";
                    vertices[k].Cluster = i;
                }
                clusterBox.Text += "\r\n";

                System.Windows.Media.Color coltmp = Vertex.cluster_colors[i % Vertex.cluster_colors.Length];
                foreach (int nodenum in cluster[i])
                {
                    if (nodenum == cluster[i][0])
                    {
                        gseries[nodenum].LegendText = "Cluster " + i.ToString();
                        gseries[nodenum].IsVisibleInLegend = true;
                    }
                    gseries[nodenum].BorderWidth = 2;
                    gseries[nodenum].ChartType = SeriesChartType.FastLine;
                    gseries[nodenum].Color = Color.FromArgb(255, coltmp.R, coltmp.G, coltmp.B);
                }
            }


            //Tmat erstellen
            if (networkDropdown.SelectedIndex == 3)
            { //nutze relativkoordinaten wenn custom ausgewählt
                TMat = Helper.TMat(cluster);
            }
            else
            {//nutze eingespeicherte
                TMat = Helper.TMat(networkDropdown.SelectedIndex);
            }


            //Parameter für die anordnung. einfach irgendwelche genommen. nochmal drüber nachdenken/nachlesen
            GraphControl = new GraphSharpControl();
            GraphControl.layout.LayoutMode = LayoutMode.Simple;
            GraphControl.layout.LayoutAlgorithmType = "CompoundFDP";
            GraphSharp.Algorithms.Layout.Compound.FDP.CompoundFDPLayoutParameters layoutParam = new GraphSharp.Algorithms.Layout.Compound.FDP.CompoundFDPLayoutParameters();
            layoutParam.ElasticConstant *= 1.5;
            GraphControl.layout.LayoutParameters = layoutParam;
            GraphControl.layout.OverlapRemovalConstraint = AlgorithmConstraints.Must;
            GraphControl.layout.OverlapRemovalAlgorithmType = "FSA";
            GraphSharp.Algorithms.OverlapRemoval.OverlapRemovalParameters overlapParam = new GraphSharp.Algorithms.OverlapRemoval.OverlapRemovalParameters();
            overlapParam.HorizontalGap = 25;
            overlapParam.VerticalGap = 25;
            GraphControl.layout.OverlapRemovalParameters = overlapParam;
            GraphControl.layout.HighlightAlgorithmType = "Simple";
            GraphControl.layout.Graph = graph;
            elementHost1.Child = GraphControl;


            runButton.Enabled = false;
            iterateButton.Enabled = false;
            initIteratorButton.Enabled = true;
            layoutButton.Enabled = true;
            betaRunButton.Enabled = true;



        }



        private void runButton_Click(object sender, EventArgs e)
        {

            if (iterationTimer.Enabled == false)
            {

                iterationTimer.Start();
                runButton.Text = "Stop Simulation";
            }
            else
            {
                iterationTimer.Stop();
                runButton.Text = "Run Simulation";
            }
        }

        private void iterationTimer_Tick(object sender, EventArgs e)
        {
            iterate();
        }

        void iterate()
        {
            iterator.iterate();
            double[] xs = iterator.xt[iterator.xt.Count - 1];
            for (int i = 0; i < iterator.nodeCount; i++)
            {
                vertices[i].Value = (xs[i] / (2.0 * Math.PI));
            }
            for (int i = 0; i < cluster.Length; i++)
            {

                double mid = 0;
                for (int j = 0; j < cluster[i].Length; j++)
                {
                    int nodenum = cluster[i][j];
                    mid += xs[nodenum];
                }
                mid /= (double)cluster[i].Length;
                for (int j = 0; j < cluster[i].Length; j++)
                {
                    int nodenum = cluster[i][j];
                    gseries[nodenum].Points.AddXY(iterator.xt.Count, xs[nodenum] - mid);
                }
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            iterationTimer.Interval = (int)numericUpDown1.Value;
        }

        private void iterateButton_Click(object sender, EventArgs e)
        {
            iterate();
        }

        private void initIteratorButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < cluster.Length; i++)
            {
                gseries[i].Points.Clear();
            }
            iterator = new NumIterator(adjmatrix, beta, sigma, delta, pertubation);
            iterator.noise = noise;
            double[] xs = iterator.xt[iterator.xt.Count - 1];
            for (int i = 0; i < iterator.nodeCount; i++)
            {
                vertices[i].Value = (xs[i] / (2.0 * Math.PI));
            }
            runButton.Enabled = true;
            iterateButton.Enabled = true;
            rmsChart.ChartAreas[0].AxisY.ScaleView.ZoomReset();

        }

        private void betaUpDown_ValueChanged(object sender, EventArgs e)
        {
            beta = (double)betaUpDown.Value * Math.PI;
            runButton.Enabled = false;
            iterateButton.Enabled = false;

        }

        private void sigmaUpDown_ValueChanged(object sender, EventArgs e)
        {
            sigma = (double)sigmaUpDown.Value * Math.PI;
            runButton.Enabled = false;
            iterateButton.Enabled = false;
        }

        private void deltaUpDown_ValueChanged(object sender, EventArgs e)
        {
            delta = (double)deltaUpDown.Value;
            runButton.Enabled = false;
            iterateButton.Enabled = false;
        }

        private void noiseUpDown_ValueChanged(object sender, EventArgs e)
        {
            noise = (double)noiseUpDown.Value;
            runButton.Enabled = false;
            iterateButton.Enabled = false;
        }

        private void tabPage1_Enter(object sender, EventArgs e)
        {
            betaUpDown.Enabled = true;
            beta = (double)betaUpDown.Value * Math.PI;
        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            betaUpDown.Enabled = false;
        }

        private void betaRunButton_Click(object sender, EventArgs e)
        {
            if (variatorThread != null && variatorThread.IsAlive)
            {
                variatorThread.Abort();
                betaRunButton.Text = "Run Simulation";
            }
            else
            {
                betaRunButton.Text = "Stop Simulation";
                betaRmsChart.Series.Clear();
                betaLjapChart.Series.Clear();
                betarmsseries = new Series[cluster.Length];
                betaljapseries = new Series[cluster.Length];

                betaRmsChart.ChartAreas[0].AxisX.Minimum = (double)betaMinUpDown.Value;
                betaRmsChart.ChartAreas[0].AxisX.Maximum = (double)betaMaxUpDown.Value;
                betaLjapChart.ChartAreas[0].AxisX.Minimum = (double)betaMinUpDown.Value;
                betaLjapChart.ChartAreas[0].AxisX.Maximum = (double)betaMaxUpDown.Value;

                for (int i = 0; i < cluster.Length; i++)
                {
                    System.Windows.Media.Color coltmp = Vertex.cluster_colors[i % Vertex.cluster_colors.Length];

                    betarmsseries[i] = new Series("Cluster " + i.ToString());
                    betarmsseries[i].ChartArea = "ChartArea1";
                    betaRmsChart.Series.Add(betarmsseries[i]);
                    betarmsseries[i].ChartType = SeriesChartType.FastPoint;
                    betarmsseries[i].Color = Color.FromArgb(255, coltmp.R, coltmp.G, coltmp.B);
                    betarmsseries[i].BorderWidth = 2;

                    betaljapseries[i] = new Series("Cluster " + i.ToString());
                    betaljapseries[i].ChartArea = "ChartArea1";
                    betaLjapChart.Series.Add(betaljapseries[i]);
                    betaljapseries[i].ChartType = SeriesChartType.FastPoint;
                    betaljapseries[i].Color = Color.FromArgb(255, coltmp.R, coltmp.G, coltmp.B);
                    betaljapseries[i].BorderWidth = 2;
                    if (i % 2 != 0)
                    {
                        betaljapseries[i].BorderDashStyle = ChartDashStyle.Dash;
                    }

                }

                stepsDone = 0;
                Action<NumVariator.result> callback_action = callback;
                variator = new NumVariator(adjmatrix, TMat, (double)betaMinUpDown.Value * Math.PI, (double)betaMaxUpDown.Value * Math.PI, (int)stepsUpDown.Value, sigma, delta, noise, pertubation, (int)preUpDown.Value, (int)recUpDown.Value, cluster, callback_action);
                variatorThread = new Thread(variator.DoWork);
                variatorThread.Start();

            }
        }

        private void callback(NumVariator.result result)
        {
            //wird auf dem anderen thread aufgerufen. damit winforms sachen geändert werden können muss das anscheinend über invoke gehen
            callbackDelegate callbackD = new callbackDelegate(callback_invoke);
            this.Invoke(callbackD, result);
        }

        private void callback_invoke(NumVariator.result result)
        //ergebnis anzeigen
        {
            stepsDone++;
            for (int i = 0; i < result.rms.Length; i++)
            {

                betarmsseries[i].Points.AddXY(result.beta / Math.PI, result.rms[i]);
            }
            for (int i = 0; i < result.ljapunow.Length; i++)
            {
                if (result.ljapunow[i] > -50 && result.ljapunow[i] < 50) betaljapseries[i].Points.AddXY(result.beta / Math.PI, result.ljapunow[i]);
            }

            if (stepsDone > (int)stepsUpDown.Value)
            {
                for (int i = 0; i < result.ljapunow.Length; i++)
                {
                    betaljapseries[i].Sort(PointSortOrder.Ascending, "X");
                    betarmsseries[i].Sort(PointSortOrder.Ascending, "X");
                    betarmsseries[i].ChartType = SeriesChartType.Spline;
                    betarmsseries[i]["LineTension"] = "0.65";
                    betaljapseries[i].ChartType = SeriesChartType.Spline;
                    betaljapseries[i]["LineTension"] = "0.65";
                }
                //wir sind fertig
                betaRunButton.Text = "Run Simulation";
            }

            Application.DoEvents();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //beim form schließen thread anhalten, sonst macht invoke einen fehler.
            if (variatorThread != null && variatorThread.IsAlive)
            {
                variatorThread.Abort();

            }
        }

        private void pertUpDown_ValueChanged(object sender, EventArgs e)
        {
            pertubation = (double)pertUpDown.Value * Math.PI;
            runButton.Enabled = false;
            iterateButton.Enabled = false;
        }

        private void matrixBox_TextChanged(object sender, EventArgs e)
        {
            int pos = matrixBox.SelectionStart; ;
            string text = matrixBox.Text;
            matrixBox.Clear();
            matrixBox.Text = text;
            matrixBox.SelectionStart = pos;

        }



        private void networkDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (networkDropdown.SelectedIndex > 2)
            {
                matrixBox.Enabled = true;
            }
            else
            {
                matrixBox.Enabled = false;
                matrixBox.Text = Helper.adjmatrix[networkDropdown.SelectedIndex];
            }

        }

    }
}
