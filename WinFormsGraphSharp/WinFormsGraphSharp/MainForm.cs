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

namespace WinFormsGraphSharp
{
    public partial class MainForm : Form
    {

        public GraphSharpControl GraphControl { get; set; }

        public MainForm()
        {
            InitializeComponent();

            var g = new NodeGraph();
          //  var ug = new UndirectedBidirectionalGraph<Vertex,UndirectedEdge<Vertex>>(g); //klappt nicht
            //einfach mal irgendwas darstellen.
            var vertices = new Vertex[30];
            for (int i = 0; i < 8; i++)
            {
                vertices[i] = new Vertex(i.ToString(), i * 100 % 255);

                g.AddVertex(vertices[i]);
            }
           
            g.AddEdge(new Edge<Vertex>(vertices[0], vertices[1]));
           g.AddEdge(new Edge<Vertex>(vertices[1], vertices[0]));
           g.AddEdge(new Edge<Vertex>(vertices[1], vertices[2]));
           g.AddEdge(new Edge<Vertex>(vertices[2], vertices[1]));
           g.AddEdge(new Edge<Vertex>(vertices[2], vertices[3]));
           g.AddEdge(new Edge<Vertex>(vertices[3], vertices[2]));
           g.AddEdge(new Edge<Vertex>(vertices[3], vertices[4]));
           g.AddEdge(new Edge<Vertex>(vertices[4], vertices[3]));
           g.AddEdge(new Edge<Vertex>(vertices[4], vertices[5]));
           g.AddEdge(new Edge<Vertex>(vertices[5], vertices[4]));
           g.AddEdge(new Edge<Vertex>(vertices[5], vertices[6]));
           g.AddEdge(new Edge<Vertex>(vertices[6], vertices[5]));
           g.AddEdge(new Edge<Vertex>(vertices[6], vertices[7]));
           g.AddEdge(new Edge<Vertex>(vertices[7], vertices[6]));
           g.AddEdge(new Edge<Vertex>(vertices[7], vertices[0]));

           g.AddEdge(new Edge<Vertex>(vertices[0], vertices[4]));
           g.AddEdge(new Edge<Vertex>(vertices[4], vertices[0]));

           g.AddEdge(new Edge<Vertex>(vertices[5], vertices[2]));
           g.AddEdge(new Edge<Vertex>(vertices[2], vertices[5]));

           g.AddEdge(new Edge<Vertex>(vertices[2], vertices[7]));
           g.AddEdge(new Edge<Vertex>(vertices[7], vertices[2]));

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




        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //nur zum testen ob das change event funktioniert...
            Random rnd = new Random();
            for (int i = 0; i < 8; i++)
            {
                GraphControl.layout.Graph.Vertices.ElementAt(i).Value = rnd.Next(0, 255);
                GraphControl.layout.ContinueLayout();
            }

        }
    }
}
