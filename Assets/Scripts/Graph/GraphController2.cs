using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graph
{
    public class GraphController2 : MonoBehaviour
    {
        private void Start()
        {
            GraphNode2<int> node1 = new GraphNode2<int>(1);
            GraphNode2<int> node2 = new GraphNode2<int>(2);
            GraphNode2<int> node3 = new GraphNode2<int>(3);
            GraphNode2<int> node4 = new GraphNode2<int>(4);
            GraphNode2<int> node5 = new GraphNode2<int>(5);
            GraphNode2<int> node6 = new GraphNode2<int>(6);
            GraphNode2<int> node7 = new GraphNode2<int>(7);

            Graph2<int> graph = new Graph2<int>();
            graph.AddNode(node1);
            graph.AddNode(node2);
            graph.AddNode(node3);
            graph.AddNode(node4);
            graph.AddNode(node5);
            graph.AddNode(node6);

            graph.AddEdge(node1, node2, 2);
            graph.AddEdge(node1, node3, 5);
            graph.AddEdge(node1, node4, 1);

            graph.AddEdge(node2, node1, 2);
            graph.AddEdge(node2, node3, 3);
            graph.AddEdge(node2, node4, 2);

            graph.AddEdge(node3, node1, 5);
            graph.AddEdge(node3, node2, 3);
            graph.AddEdge(node3, node4, 3);
            graph.AddEdge(node3, node5, 1);
            graph.AddEdge(node3, node6, 5);

            graph.AddEdge(node4, node1, 1);
            graph.AddEdge(node4, node2, 2);
            graph.AddEdge(node4, node3, 3);
            graph.AddEdge(node4, node5, 1);

            graph.AddEdge(node5, node3, 1);
            graph.AddEdge(node5, node4, 1);
            graph.AddEdge(node5, node6, 2);

            graph.AddEdge(node6, node3, 5);
            graph.AddEdge(node6, node5, 2);

            graph.Dijkstra(node1);

            //graph.StartDFS(node1);
            //graph.StartBFS(node1);
        }
    }
}
