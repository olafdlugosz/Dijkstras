using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dijkstra
{
    public class Graph<T>
    {
        public Dictionary<string, Node> Vertexes { get; set; }
        public List<KeyValuePair<string, int>> Distances { get; set; }
        public List<KeyValuePair<string, int>> PriorityQueue { get; set; }
        public List<KeyValuePair<string, string>> Previous { get; set; }
        public Graph() {
            Vertexes = new Dictionary<string, Node>();
            Distances = new List<KeyValuePair<string, int>>();
            PriorityQueue = new List<KeyValuePair<string, int>>();
            Previous = new List<KeyValuePair<string, string>>();
        }
        public class Node
        {
            public string Name { get; set; }
            public List<KeyValuePair<string, int>> Edges { get; set; }

            public Node(string name) {
                Name = name;
                Edges = new List<KeyValuePair<string, int>>();
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
                if (node.Name == V1) node.Edges.Add(new KeyValuePair<string, int>(V2, weight));
                if (node.Name == V2) node.Edges.Add(new KeyValuePair<string, int>(V1, weight));
            }
        }
        #region Dijkstra
        public void Dijkstra(string start, string end) {
            //Initial State
            foreach (var node in Vertexes.Values) {
                if (node.Name == start) {
                    Distances.Add(new KeyValuePair<string, int>(node.Name, 0));
                    PriorityQueue.Add(new KeyValuePair<string, int>(node.Name, 0));
                } else {
                    Distances.Add(new KeyValuePair<string, int>(node.Name, int.MaxValue));
                    PriorityQueue.Add(new KeyValuePair<string, int>(node.Name, int.MaxValue));
                }
                Previous.Add(new KeyValuePair<string, string>(node.Name, null));
            }
            PriorityQueue.Sort((x, y) => x.Value.CompareTo(y.Value));
            //As long as there's smth to visit
            while (PriorityQueue.Count > 0) {
                var lowest = PriorityQueue[0].Key;
                PriorityQueue.RemoveAt(0);
                if (lowest == end) {
                    //We're DONE;
                    Console.WriteLine("Distances");
                    foreach (var item in Distances) {
                        Console.WriteLine(item.Key + ":" + item.Value);
                    }
                    Console.WriteLine("Previous:");
                    foreach (var item in Previous) {
                        Console.WriteLine(item.Key + ":" + item.Value);
                    }
                    var path = PrintShortestPath(end, start, "");
                    path.Reverse();
                    Console.WriteLine($"Shortest path from {start} to {end} is: {end}-->{path}");
                    break;
                };
                if (lowest != null || Distances
                    .Where(x => x.Key == lowest)
                    .Select(x => x.Value)
                    .FirstOrDefault() != int.MaxValue) {
                    foreach (var neighbor in Vertexes[lowest].Edges) {
                        //Find neighboring nodes
                        var index = Vertexes[lowest].Edges.IndexOf(neighbor);
                        var nextNode = Vertexes[lowest].Edges[index];
                        //calculate new distance to neighboring node
                        var candidate = Distances.Where(x => x.Key == lowest).FirstOrDefault();
                        var addDistance = candidate.Value + nextNode.Value;
                        var nextNeighbor = nextNode.Key;
                        if (addDistance < Distances.Where(x => x.Key == nextNeighbor).Select(x => x.Value).FirstOrDefault()) {
                            var update = Distances.Where(x => x.Key == nextNeighbor).FirstOrDefault();
                            //updating new smallest distance to neighbor
                            var newPair = new KeyValuePair<string, int>(update.Key, addDistance);
                            Distances.Remove(update);
                            Distances.Add(newPair);
                            //update Previous - how we got to neighbor
                            var previous = Previous.Where(x => x.Key == update.Key).FirstOrDefault();
                            var newPreviousPair = new KeyValuePair<string, string>(previous.Key, lowest);
                            Previous.Remove(previous);
                            Previous.Add(newPreviousPair);
                            //update the priority queue
                            var dequeue = PriorityQueue.Where(x => x.Key == nextNeighbor).FirstOrDefault();
                            var newPriority = new KeyValuePair<string, int>(nextNeighbor, addDistance);
                            PriorityQueue.Remove(dequeue);
                            PriorityQueue.Add(newPriority);

                            PriorityQueue.Sort((x, y) => x.Value.CompareTo(y.Value));
                        }
                    }
                }
            }
        }
        public string PrintShortestPath(string end, string start, string path) {
            if (end == start) {
                return path.Remove(path.Length - 3);
               
            };
            var endNode = Previous.Where(x => x.Key == end).FirstOrDefault();
            path += endNode.Value + "-->";
            return PrintShortestPath(endNode.Value, start, path);

        }
        #endregion

    }
}
