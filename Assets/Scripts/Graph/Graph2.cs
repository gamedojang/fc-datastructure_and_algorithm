using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PriorityQueue;
using System;

namespace Graph
{
    public class Edge<T>
    {
        public GraphNode2<T> targetNode;
        public int distance;

        public Edge(GraphNode2<T> targetNode, int distance)
        {
            this.targetNode = targetNode;
            this.distance = distance;
        }
    }

    public class GraphNode2<T>
    {
        public T data;
        public List<Edge<T>> neighbors;

        public GraphNode2(T data)
        {
            this.data = data;
            neighbors = new List<Edge<T>>();
        }

        public bool IsNeighborNode(GraphNode2<T> node)
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

        public void AddNeighborNode(GraphNode2<T> node, int distance)
        {
            if (!IsNeighborNode(node))
            {
                neighbors.Add(new Edge<T>(node, distance));
            }
        }

        public void RemoveNeighborNode(GraphNode2<T> node)
        {
            foreach (var neighbor in neighbors)
            {
                if (neighbor.targetNode == node)
                {
                    neighbors.Remove(neighbor);
                    return;
                }
            }
        }
    }

    public class Graph2<T>
    {
        public List<GraphNode2<T>> nodes;

        public Graph2()
        {
            nodes = new List<GraphNode2<T>>();
        }

        public void AddNode(GraphNode2<T> node)
        {
            nodes.Add(node);
        }

        public void AddEdge(GraphNode2<T> node1, GraphNode2<T> node2, int distance)
        {
            node1.AddNeighborNode(node2, distance);
            node2.AddNeighborNode(node1, distance);
        }

        public void RemoveNode(GraphNode2<T> node)
        {
            nodes.Remove(node);
            foreach (var n in nodes)
            {
                n.RemoveNeighborNode(node);
            }
        }

        public void RemoveEdge(GraphNode2<T> node1, GraphNode2<T> node2)
        {
            node1.RemoveNeighborNode(node2);
            node2.RemoveNeighborNode(node1);
        }

        public void StartDFS(GraphNode2<T> startNode)
        {
            HashSet<GraphNode2<T>> visited = new HashSet<GraphNode2<T>>();
            DFS(startNode, visited);
        }

        private void DFS(GraphNode2<T> startNode, HashSet<GraphNode2<T>> visited)
        {
            if (visited.Contains(startNode))
            {
                return;
            }

            visited.Add(startNode);
            Debug.Log(startNode.data);

            foreach (var neighbor in startNode.neighbors)
            {
                DFS(neighbor.targetNode, visited);
            }
        }

        public void StartBFS(GraphNode2<T> startNode)
        {
            Queue<GraphNode2<T>> queue = new Queue<GraphNode2<T>>();
            queue.Enqueue(startNode);
            HashSet<GraphNode2<T>> visited = new HashSet<GraphNode2<T>>();
            visited.Add(startNode);

            BFS(queue, visited);
        }

        public void BFS(Queue<GraphNode2<T>> queue, HashSet<GraphNode2<T>> visited)
        {
            GraphNode2<T> node = queue.Dequeue();
            Debug.Log(node.data);

            foreach (var neighbor in node.neighbors)
            {
                if (!visited.Contains(neighbor.targetNode))
                {
                    queue.Enqueue(neighbor.targetNode);
                    visited.Add(neighbor.targetNode);
                }
            }

            if (queue.Count > 0)
            {
                BFS(queue, visited);
            }
        }

        public void Dijkstra(GraphNode2<T> startNode)
        {
            Dictionary<GraphNode2<T>, int> distance = new Dictionary<GraphNode2<T>, int>();
            Dictionary<GraphNode2<T>, GraphNode2<T>> previous = new Dictionary<GraphNode2<T>, GraphNode2<T>>();
            List<GraphNode2<T>> unvisited = new List<GraphNode2<T>>();

            foreach (var node in nodes)
            {
                if (node == startNode)
                {
                    distance[node] = 0;
                }
                else
                {
                    distance[node] = int.MaxValue;
                }
                unvisited.Add(node);
            }

            while (unvisited.Count > 0)
            {
                GraphNode2<T> node = null;

                foreach (var possibleNode in unvisited)
                {
                    if (node == null || distance[possibleNode] < distance[node])
                    {
                        node = possibleNode;
                    }
                }

                unvisited.Remove(node);

                foreach (var neighbor in node.neighbors)
                {
                    if (unvisited.Contains(neighbor.targetNode) == false)
                    {
                        continue;
                    }

                    int alt = distance[node] + neighbor.distance;
                    if (alt < distance[neighbor.targetNode])
                    {
                        distance[neighbor.targetNode] = alt;
                        previous[neighbor.targetNode] = node;
                    }
                }
            }

            Debug.Log(distance);
        }

        public class NodeDistanceInfo
        {
            private GraphNode2<T> node;
            private int distance;

            public NodeDistanceInfo(GraphNode2<T> node, int distance)
            {
                this.node = node;
                this.distance = distance;
            }
        }
    }
}
