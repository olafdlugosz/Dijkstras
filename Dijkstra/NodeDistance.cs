using System;
using System.Collections.Generic;
using System.Text;

namespace Dijkstra
{
    public class NodeDistance
    {
        public string Node { get; set; }
        public int Distance { get; set; }
        public NodeDistance(string node, int distance) {
            Node = node;
            Distance = distance;
        }
    }
}
