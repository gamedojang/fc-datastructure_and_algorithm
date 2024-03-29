using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraController : MonoBehaviour
{
    private void Start()
    {
        WeightedNode<int> node1 = new WeightedNode<int>(1);
        WeightedNode<int> node2 = new WeightedNode<int>(2);
        WeightedNode<int> node3 = new WeightedNode<int>(3);
        WeightedNode<int> node4 = new WeightedNode<int>(4);
        WeightedNode<int> node5 = new WeightedNode<int>(5);
        WeightedNode<int> node6 = new WeightedNode<int>(6);

        Dijkstra<int> dijkstra = new Dijkstra<int>();
        dijkstra.AddNode(node1);
        dijkstra.AddNode(node2);
        dijkstra.AddNode(node3);
        dijkstra.AddNode(node4);
        dijkstra.AddNode(node5);
        dijkstra.AddNode(node6);

        dijkstra.AddEdge(node1, node2, 1);
        dijkstra.AddEdge(node1, node3, 1);

        dijkstra.AddEdge(node2, node4, 2);

        dijkstra.AddEdge(node3, node4, 1);
        dijkstra.AddEdge(node3, node6, 2);

        dijkstra.AddEdge(node4, node5, 1);
        dijkstra.AddEdge(node4, node6, 2);

        dijkstra.AddEdge(node5, node6, 2);

        dijkstra.StartDijkstra(node1, node5);
    }
}
