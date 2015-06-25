﻿using System;
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

        public GraphSharpControl GraphControl { get; set; }

        public MainForm()
        {
            InitializeComponent();


        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //nur zum testen ob das change event funktioniert...
            Random rnd = new Random();
            for (int i = 0; i < 8; i++)
            {
                GraphControl.layout.Graph.Vertices.ElementAt(i).Value = rnd.NextDouble()*2-1;
                GraphControl.layout.ContinueLayout();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //TODO: einfärben. code aufräumen
            //einlesen der matrix, darstellung, orbitsuche, einfärben der cluster

            var g = new NodeGraph();

            string[] strarr = textBox1.Text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            int dim = strarr.Length;

            Vertex[] vertices = new Vertex[dim];
            for (int k = 0; k < dim; k++)
            {
                vertices[k] = new Vertex(k.ToString(), 0,0);

                g.AddVertex(vertices[k]);
            }

            int[][] adjmatrix = new int[dim][];

            int i = 0;
            string dreadnautcmd = "n=" + dim + " g ";

            foreach (string line in strarr)
            {
                string[] strsplitarr = line.Split(' ');
                if (strsplitarr.Length != dim)
                {
                    MessageBox.Show("nicht quadratisch");
                    return;
                }

                int[] intsplitarr = Array.ConvertAll(strsplitarr, int.Parse);
                for (int j = 0; j < dim; j++)
                {
                    if (intsplitarr[j] != 0)
                    {//verbunden
                        dreadnautcmd += " " + j.ToString();

                        g.AddEdge(new Edge<Vertex>(vertices[i], vertices[j]));

                    }
                }
                adjmatrix[i] = intsplitarr;

                dreadnautcmd += ";";
                i++;
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

            int[][] cluster=new int[matches.Count][];
            textBox2.Text="";
            for (int j=0;j<matches.Count;j++)
            {
                textBox2.Text += "cluster " + j+" mit Knoten: ";
                string[] arr ;
              cluster[j]= Array.ConvertAll(  matches[j].Groups[1].Value.Split(new string[] {" "},StringSplitOptions.RemoveEmptyEntries), int.Parse);
              foreach (int k in cluster[j])
              {
                  textBox2.Text += k + " ";
                  vertices[k].Cluster = j;
              }
                textBox2.Text+="\r\n";
            }

            GraphControl = new GraphSharpControl();

            //Parameter für die anordnung. einfach irgendwelche genommen. nochmal drüber nachdenken/nachlesen
            GraphControl.layout.LayoutMode = LayoutMode.Simple;
            GraphControl.layout.LayoutAlgorithmType = "CompoundFDP";
            GraphControl.layout.OverlapRemovalConstraint = AlgorithmConstraints.Must;
            GraphControl.layout.OverlapRemovalAlgorithmType = "FSA";
            GraphControl.layout.HighlightAlgorithmType = "Simple";
            GraphControl.layout.Graph = g;

            elementHost1.Child = GraphControl;
        }



    }
}