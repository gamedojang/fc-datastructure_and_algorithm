using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphController : MonoBehaviour
{
    private void Start()
    {
        GraphNode<int> node1 = new GraphNode<int>(1);
        GraphNode<int> node2 = new GraphNode<int>(2);
        GraphNode<int> node3 = new GraphNode<int>(3);
        GraphNode<int> node4 = new GraphNode<int>(4);
        GraphNode<int> node5 = new GraphNode<int>(5);
        GraphNode<int> node6 = new GraphNode<int>(6);
        GraphNode<int> node7 = new GraphNode<int>(7);

        Graph<int> graph = new Graph<int>();
        graph.AddNode(node1);
        graph.AddNode(node2);
        graph.AddNode(node3);
        graph.AddNode(node4);
        graph.AddNode(node5);
        graph.AddNode(node6);
        graph.AddNode(node7);

        graph.AddEdge(node1, node2);
        graph.AddEdge(node1, node3);
        graph.AddEdge(node2, node4);
        graph.AddEdge(node3, node4);
        graph.AddEdge(node4, node5);
        graph.AddEdge(node4, node6);
        graph.AddEdge(node5, node6);
        graph.AddEdge(node1, node7);

        //graph.StartDFS(node1);
        graph.StartBFS(node1);

    }
}
