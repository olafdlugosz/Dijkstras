using System;
using System.Collections.Generic;
using System.Linq;

namespace Dijkstra
{
    class Program
    {
        static void Main(string[] args) {
            var g = new Graph<string>();

            g.AddVertex("A");
            g.AddVertex("B");
            g.AddVertex("C");
            g.AddVertex("D");
            g.AddVertex("E");
            g.AddVertex("F");

            g.AddEdge("A", "B", 4);
            g.AddEdge("A", "C", 2);
            g.AddEdge("B", "E", 3);
            g.AddEdge("C", "D", 2);
            g.AddEdge("C", "F", 4);
            g.AddEdge("D", "F", 1);
            g.AddEdge("D", "E", 3);


            g.Dijkstra("A","F");


            foreach (var item in g.Vertexes.Values) {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine("====");
            foreach (var item in g.Vertexes.Values) {
                foreach (var edge in item.Edges) {
                    Console.WriteLine(item.Name + "=" + edge.Key + "-" + edge.Value);
                }
            }
            Console.WriteLine("===");
            foreach (var item in g.Distances) {
                Console.WriteLine(item.Node + ":" + item.Distance);
            }
            Console.ReadLine();
        }
    }
}
