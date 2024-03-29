using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode<T>
{
    public T Data;
    public List<GraphNode<T>> neighbors;

    public GraphNode(T data)
    {
        Data = data;
        neighbors = new List<GraphNode<T>>();
    }
}    


public class Graph<T>
{
    public List<GraphNode<T>> nodes;

    public Graph()
    {
        nodes = new List<GraphNode<T>>();
    }

    public void AddNode(GraphNode<T> node)
    {
        nodes.Add(node);
    }

    public void AddEdge(GraphNode<T> firstNode, GraphNode<T> secondNode) 
    {
        if (!firstNode.neighbors.Contains(secondNode))
        {
            firstNode.neighbors.Add(secondNode);
        }

        if (!secondNode.neighbors.Contains(firstNode))
        {
            secondNode.neighbors.Add(firstNode);
        }
    }

    public void RemoveNode(GraphNode<T> node)
    {
        nodes.Remove(node);
        foreach (var n in nodes)
        {
            n.neighbors.Remove(node);
        }
    }

    public void RemoveEdge(GraphNode<T> firstNode, GraphNode<T> secondNode)
    {
        firstNode.neighbors.Remove(secondNode);
        secondNode.neighbors.Remove(firstNode);
    }

    public void StartDFS(GraphNode<T> startNode)
    {
        HashSet<GraphNode<T>> visited = new HashSet<GraphNode<T>>();
        DFS(startNode, visited);
    }

    void DFS(GraphNode<T> startNode, HashSet<GraphNode<T>> visited)
    {
        if (visited.Contains(startNode))
        {
            return;
        }

        visited.Add(startNode);

        Debug.Log(startNode.Data);

        foreach (var neighbor in startNode.neighbors)
        {
            DFS(neighbor, visited);
        }
    }

    public void StartBFS(GraphNode<T> startNode)
    {
        Queue<GraphNode<T>> queue = new Queue<GraphNode<T>>();
        queue.Enqueue(startNode);
        HashSet<GraphNode<T>> visited = new HashSet<GraphNode<T>>();
        visited.Add(startNode);

        BFS(queue, visited);
    }

    void BFS(Queue<GraphNode<T>> queue, HashSet<GraphNode<T>> visited)
    {
        GraphNode<T> node = queue.Dequeue();

        Debug.Log(node.Data);

        foreach(var neighbor in node.neighbors)
        {
            if (!visited.Contains(neighbor))
            {
                queue.Enqueue(neighbor);
                visited.Add(neighbor);
            }
        }

        if (queue.Count > 0)
        {
            BFS(queue, visited);
        }
    }

}
