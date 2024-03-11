using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Graph
{
    public class GraphNode<T>
    {
        public T data;
        public List<GraphNode<T>> neighbors;

        public GraphNode(T data)
        {
            this.data = data;
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

        public void AddEdge(GraphNode<T> node1, GraphNode<T> node2)
        {
            if (!node1.neighbors.Contains(node2))
            {
                node1.neighbors.Add(node2);
            }

            if (!node2.neighbors.Contains(node1))
            {
                node2.neighbors.Add(node1);
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

        public void RemoveEdge(GraphNode<T> node1, GraphNode<T> node2)
        {
            node1.neighbors.Remove(node2);
            node2.neighbors.Remove(node1);
        }

        public void StartDFS(GraphNode<T> startNode)
        {
            HashSet<GraphNode<T>> visited = new HashSet<GraphNode<T>>();
            DFS(startNode, visited);
        }

        private void DFS(GraphNode<T> startNode, HashSet<GraphNode<T>> visited)
        {
            if (visited.Contains(startNode))
            {
                return;
            }

            visited.Add(startNode);
            Debug.Log(startNode.data);

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

        public void BFS(Queue<GraphNode<T>> queue, HashSet<GraphNode<T>> visited)
        {
            GraphNode<T> node = queue.Dequeue();
            Debug.Log(node.data);

            foreach (var neighbor in node.neighbors)
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
}
