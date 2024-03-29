using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStar
{
    static PriorityQueue<AStarNode> closedQueue, openQueue;

    static Stack<AStarNode> GetReverseResult(AStarNode node)
    {
        Stack<AStarNode> resultStack = new Stack<AStarNode>();
        while (node != null)
        {
            resultStack.Push(node);
            node = node.parent;
        }
        return resultStack;
    }

    static float GetPositionScore(AStarNode currentNode, AStarNode endNode) 
    { 
        Vector3 resultValue = currentNode.position - endNode.position;
        return resultValue.magnitude;
    }

    public static Stack<AStarNode> FindPath(AStarNode startNode, AStarNode endNode) 
    {
        openQueue = new PriorityQueue<AStarNode>();
        openQueue.Enqueue(startNode);

        startNode.gScore = 0;
        startNode.hScore = GetPositionScore(startNode, endNode);

        closedQueue = new PriorityQueue<AStarNode>();

        AStarNode node = null;

        while (openQueue.Count != 0)
        {
            node = openQueue.Dequeue();

            Debug.Log($"Dequeue : [{node.position.x}[{node.position.z}");

            if (node == endNode)
            {
                return GetReverseResult(node);
            }

            // 현재 node의 이웃 노드들을 탐색
            ArrayList availableNodes = AStarController.Instance.GetAvailableNodes(node);

            foreach (AStarNode availableNode in availableNodes)
            {
                if (!closedQueue.Contains(availableNode))
                {
                    if (openQueue.Contains(availableNode))
                    {
                        float score = GetPositionScore(node, availableNode);

                        float newGScore = node.gScore + score;

                        if (availableNode.gScore > newGScore)
                        {
                            availableNode.gScore = newGScore;
                            availableNode.parent = node;
                        }
                    }
                    else
                    {
                        float score = GetPositionScore(node, availableNode);

                        float newGScore = node.gScore + score;
                        float newHScore = GetPositionScore(availableNode, endNode);

                        availableNode.gScore = newGScore;
                        availableNode.hScore = newHScore;
                        availableNode.parent = node;

                        openQueue.Enqueue(availableNode);

                        Debug.Log($"Enqueue : [{availableNode.position.x}][{availableNode.position.z}] : " +
                            $"G[{availableNode.gScore}], H[{availableNode.hScore}]");
                    }
                }
            }
            closedQueue.Enqueue(node);
        }

        if (node == endNode)
        {
            return GetReverseResult(node);
        }
        return null;
    }
}
