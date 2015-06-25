using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph;
using GraphSharp.Controls;

namespace ProjectMSF
{
    //beides nötig da eigener vertex für den graph. https://sachabarbs.wordpress.com/2010/08/31/pretty-cool-graphs-in-wpf/.
    public class NodeGraph : BidirectionalGraph<Vertex, Edge<Vertex>>
    {

        public NodeGraph() { }
    
        public NodeGraph(bool allowParallelEdges)
            : base(allowParallelEdges) { }

        public NodeGraph(bool allowParallelEdges, int vertexCapacity)
            : base(allowParallelEdges, vertexCapacity) { }

        public override bool AddEdge(Edge<Vertex> edge) 
        {
            base.AddEdge(edge);
            Edge<Vertex> edge2 = new Edge<Vertex>(edge.Target, edge.Source);
            return base.AddEdge(edge2);
        }
    }
    public class NodeGraphLayout : GraphLayout<Vertex, Edge<Vertex>, NodeGraph> { }
  
}