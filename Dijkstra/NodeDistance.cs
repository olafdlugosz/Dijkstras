using System;
using System.Collections.Generic;
using System.Text;

namespace Dijkstra
{
    public struct NodeDistance
    {
        public string Node { get; set; }
        public int Distance { get; set; }
        public NodeDistance(string node, int distance) : this() {
            Node = node;
            Distance = distance;
        }
    }
}
