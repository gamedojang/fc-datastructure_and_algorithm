using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge<T>
{
    public WeightedNode<T> targetNode;
    public int distance;

    public Edge(WeightedNode<T> node, int distance)
    {
        this.targetNode = node;
        this.distance = distance;
    }
}

public class WeightedNode<T>
{
    public T data;
    public List<Edge<T>> neighbors;

    public WeightedNode(T data)
    {
        this.data = data;
        neighbors = new List<Edge<T>>();
    }

    bool IsNeighborNode(WeightedNode<T> node)
    {
        foreach (var neighbor in neighbors)
        {
            if (neighbor.targetNode == node)
            {
                return true;
            }
        }
        return false;
    }

    public void AddNeighborNode(WeightedNode<T> node, int distance)
    {
        if (!IsNeighborNode(node))
        {
            neighbors.Add(new Edge<T>(node, distance));
        }
    }

}
