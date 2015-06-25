using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using GraphSharp;
using GraphSharp.Controls;
using System.Windows.Media;

namespace ProjectMSF
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll",
            EntryPoint = "AllocConsole",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();
        public Form1()
        {
            InitializeComponent();
#if DEBUG
            AllocConsole();
#endif
            Console.WriteLine("Programm started.");

            var g = new NodeGraph();
            int vertexCount = 11;
            var vertices = new Vertex[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                float brightness = (float)(((double)i) / (double)vertexCount);
                vertices[i] = new Vertex(i.ToString(), Color.FromRgb(255, 255, 0),brightness );

                g.AddVertex(vertices[i]);

            }


            

            GraphSharpControl graphControl = (GraphSharpControl)elementHost1.Child;

            //Parameter für die anordnung. einfach irgendwelche genommen. nochmal drüber nachdenken/nachlesen
           
            graphControl.layout.LayoutMode = LayoutMode.Simple;
            graphControl.layout.LayoutAlgorithmType = "CompoundFDP";
            //graphControl.layout.OverlapRemovalConstraint = AlgorithmConstraints.Must;
            //graphControl.layout.OverlapRemovalAlgorithmType = "FSA";
            //graphControl.layout.HighlightAlgorithmType = "Simple";
        

            graphControl.layout.Graph = g;

           
        }
    }
}
