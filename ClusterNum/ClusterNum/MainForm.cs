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
using System.IO;
using Accord.Math;
using System.Globalization;


namespace ClusterNum
{
    public partial class MainForm : Form
    {

        bool graph_loaded = false;
        Vertex[] vertices;
        double[,] adjmatrix;
        double[,] TMat;
        int[][] cluster;

        GraphSharpControl GraphControl;
        NodeGraph graph;

        NumIterator iterator;
        NumVariator variator;
        Thread variatorThread;
        delegate void callbackDelegate(NumVariator.result result);
        delegate void sigmaVariationCallbackDelegate(int isigma, int sigmasteps);
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //beim form schließen thread anhalten, sonst macht invoke einen fehler.
            if (variatorThread != null && variatorThread.IsAlive)
            {
                variatorThread.Abort();

            }
        }

        private void betaUpDown_ValueChanged(object sender, EventArgs e)
        {
            beta = (double)betaUpDown.Value * Math.PI;
            iterator_reset();

        }

        private void sigmaUpDown_ValueChanged(object sender, EventArgs e)
        {
            sigma = (double)sigmaUpDown.Value * Math.PI;
            iterator_reset();
        }

        private void deltaUpDown_ValueChanged(object sender, EventArgs e)
        {
            delta = (double)deltaUpDown.Value;
            iterator_reset();
        }

        private void noiseUpDown_ValueChanged(object sender, EventArgs e)
        {
            noise = (double)noiseUpDown.Value;
            iterator_reset();
        }

        private void pertUpDown_ValueChanged(object sender, EventArgs e)
        {
            pertubation = (double)pertUpDown.Value * Math.PI;
            runButton.Enabled = false;
            iterateButton.Enabled = false;
        }

        private void networkDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            iterator_reset();
            graph_loaded = false;
            runButton.Enabled = false;
            iterateButton.Enabled = false;
            layoutButton.Enabled = false;
            betaRunButton.Enabled = false;

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

        private void matrixBox_TextChanged(object sender, EventArgs e)
        {
            graph_loaded = false;
            runButton.Enabled = false;
            iterateButton.Enabled = false;
            layoutButton.Enabled = false;
            betaRunButton.Enabled = false;

            int pos = matrixBox.SelectionStart; ;
            string text = matrixBox.Text;
            matrixBox.Clear();
            matrixBox.Text = text;
            matrixBox.SelectionStart = pos;
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

            graph_loaded = true;
            iterator_init();
            runButton.Enabled = true;
            iterateButton.Enabled = true;
            layoutButton.Enabled = true;
            betaRunButton.Enabled = true;
        }

        private void iterator_init()
        {
            if (graph_loaded)
            {
                foreach (Series serie in gseries)
                {
                    serie.Points.Clear();
                }

                iterator = new NumIterator(adjmatrix, beta, sigma, delta, pertubation);
                iterator.noise = noise;
                double[] xs = iterator.xt[iterator.xt.Count - 1];

                for (int i = 0; i < xs.Length; i++)
                {
                    vertices[i].Value = 2.0 * Math.PI;
                }
                rmsChart.ChartAreas[0].AxisY.ScaleView.ZoomReset();
            }
        }

        private void iterator_reset()
        {
            iterationTimer.Stop();
            runButton.Text = "Run Simulation";
            iterator_init();

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

        private void variateSigmaBetaButton_Click(object sender, EventArgs e)
        {
            Thread variateSigmaThread = new Thread(variateSigma);
            variateSigmaThread.Start();

        }

        Stopwatch sigmaStopwatch = new Stopwatch();

        private void variateSigma()
        {
            outerisigma = 0;
            CultureInfo cinf = new CultureInfo("en-gb");

            string filepath = "Result2d\\ljapunow_Cluster";

            variator = new NumVariator(adjmatrix, TMat, (double)betaMinUpDown.Value * Math.PI, (double)betaMaxUpDown.Value * Math.PI, (int)stepsUpDown.Value, sigma, delta, noise, pertubation, (int)preUpDown.Value, (int)recUpDown.Value, cluster, null);
            variator.sigmaVariationCallback = sigmaVariationCallback;
            double pi2 = Math.PI * 2;
            variator.pre = 100;
            variator.rec = 1000;
            int div = 199;
            sigmaStopwatch.Start();
            double[][][] ljapunow = variator.VariateSigmaBeta(-pi2, pi2, div, -pi2, pi2, div);
            for (int icluster = 0; icluster < cluster.Length; icluster++)  // Für jedes Cluster
            {
                string nfilepath = filepath + icluster.ToString() + ".txt";
                StreamWriter sw = new StreamWriter(nfilepath);
                for (int isigma = 0; isigma < ljapunow.Length; isigma++)     // Für jedes Sigma wird Zeile mit Ljapunow über Beta generiert
                {
                    string line = "";                                           // Zeilenstring mit Tabgetrennte geht beta durch
                    for (int ibeta = 0; ibeta < ljapunow[isigma].Length; ibeta++)
                    {
                        double ljap = ljapunow[isigma][ibeta][icluster];
                        if(double.IsNaN(ljap))        // Falls NaN
                        {
                            ljap = -1.0;
                            /*  Mittelwert Geschichte
                            ljap = 0;
                            double cnt=0;
                            for(int midsigma=-1;midsigma<=1;midsigma++)
                                for(int midbeta=-1;midbeta<=1;midbeta++)
                                {
                                    if(ibeta+midbeta<ljapunow[isigma].Length && ibeta+midbeta>0)
                                    {
                                        if (!Double.IsNaN(ljapunow[isigma][ibeta + midbeta][icluster]))
                                        {
                                            ljap += ljapunow[isigma][ibeta + midbeta][icluster];
                                            cnt++;
                                        }
                                    }
                                    if (isigma + midsigma < ljapunow.Length && isigma + midsigma > 0)
                                    {
                                        if (!Double.IsNaN(ljapunow[isigma+midsigma][ibeta][icluster]))
                                        {
                                            ljap += ljapunow[isigma+midsigma][ibeta][icluster];
                                            cnt++;
                                        }
                                    }
                                }
                            ljap /= cnt;*/
                        }
                        line += ljap.ToString(CultureInfo.InvariantCulture);
                        if (ibeta != ljapunow[isigma].Length - 1)  // am Ende der Zeile kein Tab einfügen
                            line += "\t";
                    }
                    
                    sw.WriteLine(line);
                }
                sw.Close();
            }
        }

        private void sigmaVariationCallback(int isigma, int sigmasteps)
        {
            //wird auf dem anderen thread aufgerufen. damit winforms sachen geändert werden können muss das anscheinend über invoke gehen
            sigmaVariationCallbackDelegate callbackD = new sigmaVariationCallbackDelegate(sigmaVariationCallback_invoke);
            this.Invoke(callbackD, isigma, sigmasteps);
        }


        int outerisigma;
        private void sigmaVariationCallback_invoke(int isigma, int sigmasteps)
        {
            double done = ((double)outerisigma / (double)sigmasteps);
            string percentage = (done * 100.0).ToString();
            sigmaProgressLable.Text = "Sigma: " + outerisigma.ToString() + " von " + sigmasteps.ToString() + " (" + percentage + " %)";
            double elapsedms = sigmaStopwatch.ElapsedMilliseconds;
            double msPerStep = elapsedms / (double)outerisigma;
            int stepsleft = sigmasteps - outerisigma;
            double todoSeconds = (msPerStep * (double)stepsleft) / 1000.0;
            double todoMinutes = todoSeconds / 60.0;
            todoMinutes = Math.Round(todoMinutes, 2);

            timeLeftLabel.Text = todoMinutes.ToString() + " min";
            outerisigma++;

        }

    }
}
