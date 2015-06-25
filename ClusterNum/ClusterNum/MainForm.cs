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


namespace ClusterNum
{
    public partial class MainForm : Form
    {
        private NodeGraph graph;
        private Vertex[] vertices;
        private int[][] adjmatrix;
        private int dim;
        public GraphSharpControl GraphControl { get; set; }

        public NumIterator iterator;


        public MainForm()
        {
            InitializeComponent();


        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GraphControl.layout.ContinueLayout();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //TODO: einfärben. code aufräumen
            //einlesen der matrix, darstellung, orbitsuche, einfärben der cluster

            graph = new NodeGraph();


            string[] strarr = textBox1.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
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
                vertices[k] = new Vertex(k.ToString(), 0, 0);

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

            int[][] cluster = new int[matches.Count][];
            textBox2.Text = "";
            for (int j = 0; j < matches.Count; j++)
            {
                textBox2.Text += "cluster " + j + " mit Knoten: ";
                cluster[j] = Array.ConvertAll(matches[j].Groups[1].Value.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries), int.Parse);
                foreach (int k in cluster[j])
                {
                    textBox2.Text += k + " ";
                    vertices[k].Cluster = j;
                }
                textBox2.Text += "\r\n";
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
        }

        private void runButton_Click(object sender, EventArgs e)
        {

            if (iterationTimer.Enabled == false)
            {
                iterator = new NumIterator(adjmatrix, 0.72 * Math.PI, 0.67 * Math.PI, 0.525 * Math.PI);
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

            iterator.iterate();
            double[] xs = iterator.xt[iterator.xt.Count - 1];
            for (int i = 0; i < iterator.vertexCount; i++)
            {
                vertices[i].Value = (xs[i] / (2.0 * Math.PI));
            }
        }



    }
}
