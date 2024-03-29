using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dijkstra<T>
{
    public List<WeightedNode<T>> nodes;

    public Dijkstra() 
    { 
        nodes = new List<WeightedNode<T>>();
    }

    public void AddNode(WeightedNode<T> node)
    {
        nodes.Add(node);
    }

    public void AddEdge(WeightedNode<T> firstNode, WeightedNode<T> secondNode, int distance) 
    {
        firstNode.AddNeighborNode(secondNode, distance);
        secondNode.AddNeighborNode(firstNode, distance);
    }

    public void StartDijkstra(WeightedNode<T> startNode, WeightedNode<T> endNode)
    {
        Dictionary<WeightedNode<T>, int> distance = new Dictionary<WeightedNode<T>, int>();
        Dictionary<WeightedNode<T>, WeightedNode<T>> previousNode = new Dictionary<WeightedNode<T>, WeightedNode<T>>();
        List<WeightedNode<T>> unvisited = new List<WeightedNode<T>>();

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
            WeightedNode<T> node = null;

            // unvisited 값을 탐색해서 distance에 nvisitedNode의 가중치가
            // node의 거리보다 작은 값을 찾습니다.
            // 처음에는 startNode가 distance가 0이기 때문에 startNode가 node가 됩니다.
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
                // node의 neighbors를 탐색해서 해당 노드가 unvisied 리스트에 없다면 continue 합니다.
                if (unvisited.Contains(neighbor.targetNode) == false)
                {
                    continue;
                }

                // neighbor node의 값이 unvisited에 있다면
                // node의 distance 값에 neighbor의 distance 값을 더해서 변수에 저장합니다.
                // alt 값이 기존 distance 배열에 저장된 값 보다 작으면 distance 배열의 값을 업데이트 합니다.
                // 아울러 해당 값을 업데이트 한 이전 노드의 정보를 저장하면 최단 경로를 얻을 수 있습니다.
                int alt = distance[node] + neighbor.distance;
                if (alt < distance[neighbor.targetNode])
                {
                    distance[neighbor.targetNode] = alt;
                    previousNode[neighbor.targetNode] = node;
                }
            }
        }

        // 결과 출력
        List<WeightedNode<T>> pathList = new List<WeightedNode<T>>();

        WeightedNode<T> currentKey = endNode;

        while (previousNode.ContainsKey(currentKey))
        {
            currentKey = previousNode[currentKey];
            pathList.Add(currentKey);
        }

        pathList.Reverse();

        foreach (var path in pathList)
        {
            Debug.Log(path.data.ToString());
        }
    }
}
