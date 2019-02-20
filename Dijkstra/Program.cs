using System;
using System.Collections.Generic;
using System.Linq;

namespace Dijkstra
{
    class Program
    {
        static void Main(string[] args) {
            var g = new Graph<string>();

            //g.AddVertex("A");
            //g.AddVertex("B");
            //g.AddVertex("C");
            //g.AddVertex("D");
            //g.AddVertex("E");
            //g.AddVertex("F");

            //g.AddEdge("A", "B", 4);
            //g.AddEdge("A", "C", 2);
            //g.AddEdge("B", "E", 3);
            //g.AddEdge("C", "D", 2);
            //g.AddEdge("C", "F", 4);
            //g.AddEdge("D", "F", 1);
            //g.AddEdge("D", "E", 3);


            //g.Dijkstra("A","F");
            g.AddVertex("Lernia");
            g.AddVertex("Arboga");
            g.AddVertex("Örebro");
            g.AddVertex("Norrköping");
            g.AddVertex("Jönköping");
            g.AddVertex("Göteborg");

            g.AddEdge("Lernia", "Arboga", 145);
            g.AddEdge("Arboga", "Örebro", 45);
            g.AddEdge("Norrköping", "Örebro", 117);
            g.AddEdge("Göteborg", "Örebro", 281);
            g.AddEdge("Norrköping", "Jönköping", 167);
            g.AddEdge("Göteborg", "Jönköping", 144);

            g.Dijkstra("Lernia", "Göteborg");
            foreach (var item in g.Vertexes.Values) {
                Console.WriteLine(item.Name);
            }
            Console.WriteLine("====");
            foreach (var item in g.Vertexes.Values) {
                foreach (var edge in item.Edges) {
                    Console.WriteLine(item.Name + "=" + edge.Node + "-" + edge.Distance);
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
