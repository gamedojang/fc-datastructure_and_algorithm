using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStar
{
    public class AStar
    {
        private static SortingQueue<Node> closedQueue, openQueue;

        public static Stack<Node> FindPath(Node startNode, Node endNode)
        {
            Debug.Log("### FindPath");

            int findCount = 0;

            // 시작 노드를 오픈 큐에 추가
            openQueue = new SortingQueue<Node>();
            openQueue.Enqueue(startNode);

            startNode.gScore = 0f;
            startNode.hScore = GetPostionScore(startNode, endNode);

            // 닫힌 큐 초기화
            closedQueue = new SortingQueue<Node>();

            Node node = null;

            while (openQueue.Count != 0)
            {
                node = openQueue.Dequeue();

                // 목표 노드를 찾았을 경우
                if (node == endNode)
                {
                    Debug.Log("Find: " + findCount);
                    return GetReverseResult(node);
                }

                // 현재 노드의 이웃 노드들을 탐색
                ArrayList availableNodes = AStartController.Instance.GetAvailableNodes(node);

                foreach (Node availableNode in availableNodes)
                {
                    if (!closedQueue.Contains(availableNode))
                    {
                        if (openQueue.Contains(availableNode))
                        {
                            float score = GetPostionScore(node, availableNode);
                            float newGScore = node.gScore + score;

                            if (availableNode.gScore > newGScore)
                            {
                                availableNode.gScore = newGScore;
                                availableNode.parent = node;
                            }
                        }
                        else
                        {
                            float score = GetPostionScore(node, availableNode);

                            float newGScore = node.gScore + score;
                            float newHScore = GetPostionScore(availableNode, endNode);

                            availableNode.gScore = newGScore;
                            availableNode.hScore = newHScore;
                            availableNode.parent = node;

                            openQueue.Enqueue(availableNode);
                            findCount++;
                        }
                    }
                }
                closedQueue.Enqueue(node);
            }

            if (node == endNode)
            {
                Debug.Log("Find: " + findCount);
                return GetReverseResult(node);
            }

            return null;
        }

        private static Stack<Node> GetReverseResult(Node node)
        {
            Stack<Node> resultStack = new Stack<Node>();
            while (node != null)
            {
                resultStack.Push(node);
                node = node.parent;
            }

            return resultStack;
        }

        private static float GetPostionScore(Node currentNode, Node endNode)
        {
            Vector3 resultValue = currentNode.position - endNode.position;
            return resultValue.magnitude;
        }
    }

}
