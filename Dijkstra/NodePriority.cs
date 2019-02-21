using System;
using System.Collections.Generic;
using System.Text;

namespace Dijkstra
{
    public class NodePriority
    {
        public NodePriority(string node, int priority) {
            Node = node;
            Priority = priority;
        }

        public string Node { get; set; }
        public int Priority { get; set; }
    }
}
