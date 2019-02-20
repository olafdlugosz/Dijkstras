using System;
using System.Collections.Generic;
using System.Text;

namespace Dijkstra
{
    public struct NodePriority
    {
        public NodePriority(string node, int priority) : this() {
            Node = node;
            Priority = priority;
        }

        public string Node { get; set; }
        public int Priority { get; set; }
    }
}
