using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuickGraph;
using GraphSharp.Controls;

namespace WinFormsGraphSharp
{
    //beides nötig da eigener vertex für den graph. https://sachabarbs.wordpress.com/2010/08/31/pretty-cool-graphs-in-wpf/.
    public class NodeGraph :BidirectionalGraph<Vertex, Edge<Vertex>>
    {
        public NodeGraph() { }
    
        public NodeGraph(bool allowParallelEdges)
            : base(allowParallelEdges) { }

        public NodeGraph(bool allowParallelEdges, int vertexCapacity)
            : base(allowParallelEdges, vertexCapacity) { }
    }
    public class NodeGraphLayout : GraphLayout<Vertex, Edge<Vertex>, NodeGraph> { }
  
}