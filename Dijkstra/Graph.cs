using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dijkstra
{
    public class Graph<T>
    {
        public Dictionary<string, Node> Vertexes { get; set; }
        public List<NodeDistance> Distances { get; set; }
        public List<NodePriority> PriorityQueue { get; set; }
        public List<PathStep> PathSteps { get; set; }
        public Graph() {
            Vertexes = new Dictionary<string, Node>();
            Distances = new List<NodeDistance>();
            PriorityQueue = new List<NodePriority>();
            PathSteps = new List<PathStep>();
        }
        public class Node
        {
            public string Name { get; set; }
            public List<NodeDistance> Edges { get; set; }

            public Node(string name) {
                Name = name;
                Edges = new List<NodeDistance>();
            }
        }

        public void AddVertex(string Name) {
            var node = new Node(Name);
            if (!Vertexes.ContainsKey(Name) && Vertexes != null) {
                Vertexes.Add(Name, node);
            };
        }
        public void AddEdge(string V1, string V2, int weight) {
            foreach (var node in Vertexes.Values) {
                if (node.Name == V1) node.Edges.Add(new NodeDistance(V2, weight));
                if (node.Name == V2) node.Edges.Add(new NodeDistance(V1, weight));
            }
        }
        #region Dijkstra
        public void Dijkstra(string start, string end) {
            //Initial State
            foreach (var node in Vertexes.Values) {
                if (node.Name == start) {
                    Distances.Add(new NodeDistance(node.Name, 0));
                    PriorityQueue.Add(new NodePriority(node.Name, 0));
                } else {
                    Distances.Add(new NodeDistance(node.Name, int.MaxValue));
                    PriorityQueue.Add(new NodePriority(node.Name, int.MaxValue));
                }
                PathSteps.Add(new PathStep(node.Name, null));
            }
            PriorityQueue.Sort((x, y) => x.Priority.CompareTo(y.Priority));
            //As long as there's smth to visit
            while (PriorityQueue.Count > 0) {
                var lowest = PriorityQueue[0].Node;
                PriorityQueue.RemoveAt(0);
                if (lowest == end) {
                    //We're DONE;
                    Console.WriteLine("Distances");
                    foreach (var item in Distances) {
                        Console.WriteLine(item.Node + ":" + item.Distance);
                    }
                    Console.WriteLine("Previous:");
                    foreach (var pathstep in PathSteps) {
                        Console.WriteLine(pathstep.Location + ":" + pathstep.PreviousStep);
                    }
                    var path = PrintShortestPath(end, start, "");
                    //path.Reverse();
                    Console.WriteLine($"Shortest path from {start} to {end} is: {end}<--{path}");
                    break;
                };
                if (lowest != null || Distances
                    .Where(x => x.Node == lowest)
                    .Select(x => x.Distance)
                    .FirstOrDefault() != int.MaxValue) {
                    foreach (var neighbor in Vertexes[lowest].Edges) {
                        //Find neighboring nodes
                        var index = Vertexes[lowest].Edges.IndexOf(neighbor);
                        var nextNode = Vertexes[lowest].Edges[index];
                        //calculate new distance to neighboring node
                        var candidate = Distances.Where(x => x.Node == lowest).FirstOrDefault();
                        var addDistance = candidate.Distance + nextNode.Distance;
                        var nextNeighbor = nextNode.Node;
                        if (addDistance < Distances.Where(x => x.Node == nextNeighbor).Select(x => x.Distance).FirstOrDefault()) {
                            var oldDistance = Distances.Where(x => x.Node == nextNeighbor).FirstOrDefault();
                            //updating new smallest distance to neighbor
                            var newDistance = new NodeDistance(oldDistance.Node, addDistance);
                            Distances.Remove(oldDistance);
                            Distances.Add(newDistance);
                            //update Previous - how we got to neighbor
                            var previous = PathSteps.Where(x => x.Location == oldDistance.Node).FirstOrDefault();
                            var newPreviousPair = new PathStep(previous.Location, lowest);
                            PathSteps.Remove(previous);
                            PathSteps.Add(newPreviousPair);
                            //update the priority queue
                            var oldPriority = PriorityQueue.Where(x => x.Node == nextNeighbor).FirstOrDefault();
                            var newPriority = new NodePriority(nextNeighbor, addDistance);
                            PriorityQueue.Remove(oldPriority);
                            PriorityQueue.Add(newPriority);

                            PriorityQueue.Sort((x, y) => x.Priority.CompareTo(y.Priority));
                        }
                    }
                }
            }
        }
        public string PrintShortestPath(string end, string start, string path) {
            if (end == start) {
                return path.Remove(path.Length - 3);               
            };
            var endNode = PathSteps.Where(x => x.Location == end).FirstOrDefault();
            path += endNode.PreviousStep + "<--";
            return PrintShortestPath(endNode.PreviousStep, start, path);

        }
        #endregion

    }
}
