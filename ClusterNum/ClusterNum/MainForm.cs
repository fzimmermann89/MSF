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


namespace ClusterNum
{
    public partial class MainForm : Form
    {
        private NodeGraph graph;
        private Vertex[] vertices;
        private int[][] adjmatrix;
        private int[][] cluster;

        public GraphSharpControl GraphControl { get; set; }

        public NumIterator iterator;

        double beta = 0.72 * Math.PI;
        double sigma = 0.67 * Math.PI;
        double delta = 0.525;
        double epsilon = 0.01;

        Series[] gseries;

        public MainForm()
        {
            InitializeComponent();



        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void layoutButton_Click(object sender, EventArgs e)
        {
            GraphControl.layout.ContinueLayout();
        }

        private void initGraphButton_Click(object sender, EventArgs e)
        {
            //TODO: einfärben. code aufräumen
            //einlesen der matrix, darstellung, orbitsuche, einfärben der cluster

            graph = new NodeGraph();


            string[] strarr = matrixBox.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            int dim = strarr.Length;
            adjmatrix = new int[dim][];

            int i = 0;
            foreach (string line in strarr)
            {
                string[] strsplitarr = line.Split(' ');
                if (strsplitarr.Length != dim)
                {
                    MessageBox.Show("nicht quadratisch");
                    return;
                }

                int[] intsplitarr = Array.ConvertAll(strsplitarr, int.Parse);
                adjmatrix[i++] = intsplitarr;

            }

            vertices = new Vertex[dim];
            for (int k = 0; k < dim; k++)
            {
                vertices[k] = new Vertex(k.ToString(), 1.0, 0);

                graph.AddVertex(vertices[k]);
            }


            string dreadnautcmd = "n=" + dim + " g ";
            for (int irow = 0; irow < dim; irow++)
            {
                for (int icol = 0; icol < dim; icol++)
                {
                    if (adjmatrix[irow][icol] != 0)
                    {
                        graph.AddEdge(new Edge<Vertex>(vertices[irow], vertices[icol]));
                        dreadnautcmd += icol + " ";
                    }



                }
                dreadnautcmd += ";";

            }


            dreadnautcmd += "x o q";

            //dreadnaut starten
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = Environment.CurrentDirectory + @"\..\..\..\dreadnaut.exe";
            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            process.StandardInput.WriteLine(dreadnautcmd);
            process.WaitForExit();
            string ergebnis = process.StandardOutput.ReadToEnd();

            //letzte zeile der ausgabe enthält orbits.
            string[] s = ergebnis.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            MatchCollection matches = Regex.Matches(s[s.Length - 2], @"([0-9 ]+)(\(([^)]*)\))?;");

            cluster = new int[matches.Count][];
            clusterBox.Text = "";
            for (int j = 0; j < matches.Count; j++)
            {
                clusterBox.Text += "cluster " + j + " mit Knoten: ";
                cluster[j] = Array.ConvertAll(matches[j].Groups[1].Value.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
                foreach (int k in cluster[j])
                {
                    clusterBox.Text += k + " ";
                    vertices[k].Cluster = j;
                }
                clusterBox.Text += "\r\n";
            }

            rmsChart.Series.Clear();
            gseries = new Series[cluster.Length];
            for (i = 0; i < cluster.Length; i++)
            {
                gseries[i] = new Series("Cluster " + i.ToString());
                gseries[i].ChartArea = "ChartArea1";
                rmsChart.Series.Add(gseries[i]);

                gseries[i].ChartType = SeriesChartType.FastLine;

                System.Windows.Media.Color coltmp = Vertex.cluster_colors[i % Vertex.cluster_colors.Length];

                gseries[i].Color = Color.FromArgb(255, coltmp.R, coltmp.G, coltmp.B);
            }

            //Parameter für die anordnung. einfach irgendwelche genommen. nochmal drüber nachdenken/nachlesen
            GraphControl = new GraphSharpControl();
            GraphControl.layout.LayoutMode = LayoutMode.Simple;
            GraphControl.layout.LayoutAlgorithmType = "CompoundFDP";
            GraphControl.layout.OverlapRemovalConstraint = AlgorithmConstraints.Must;
            GraphControl.layout.OverlapRemovalAlgorithmType = "FSA";
            GraphControl.layout.HighlightAlgorithmType = "Simple";
            GraphControl.layout.Graph = graph;
            elementHost1.Child = GraphControl;


            runButton.Enabled = false;
            iterateButton.Enabled = false;
            initIteratorButton.Enabled = true;
            layoutButton.Enabled = true;
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
            for (int i = 0; i < iterator.vertexCount; i++)
            {
                vertices[i].Value = (xs[i] / (2.0 * Math.PI));
            }
            for (int i = 0; i < cluster.Length; i++)
            {
                double rms = 0;
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
                    rms += (mid - xs[nodenum]) * (mid - xs[nodenum]);
                }
                rms = Math.Sqrt(rms);
                gseries[i].Points.AddXY(iterator.xt.Count, rms);
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
            iterator = new NumIterator(adjmatrix, beta, sigma, delta);
            iterator.pertubation = epsilon;
            double[] xs = iterator.xt[iterator.xt.Count - 1];
            for (int i = 0; i < iterator.vertexCount; i++)
            {
                vertices[i].Value = (xs[i] / (2.0 * Math.PI));
            }
            runButton.Enabled = true;
            iterateButton.Enabled = true;
        }

        private void betaUpDown_ValueChanged(object sender, EventArgs e)
        {
            beta = (double)betaUpDown.Value;
        }

        private void sigmaUpDown_ValueChanged(object sender, EventArgs e)
        {
            sigma = (double)sigmaUpDown.Value;
        }

        private void deltaUpDown_ValueChanged(object sender, EventArgs e)
        {
            delta = (double)deltaUpDown.Value;
        }

        private void epsilonUpDown_ValueChanged(object sender, EventArgs e)
        {
            epsilon = (double)epsilonUpDown.Value;
        }



    }
}
