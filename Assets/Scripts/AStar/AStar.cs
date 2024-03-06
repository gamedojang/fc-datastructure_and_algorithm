using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStar
{
    public class AStar
    {
        private static SortingQueue<Node> closedQueue, openQueue;

        /// <summary>
        /// A* 알고리즘을 이용하여 시작 노드에서 목표 노드까지의 경로를 찾는다.
        /// </summary>
        /// <param name="startNode"></param>
        /// <param name="endNode"></param>
        /// <returns></returns>
        public static Stack<Node> FindPath(Node startNode, Node endNode)
        {
            // 시작 노드를 오픈 큐에 추가
            openQueue = new SortingQueue<Node>();
            openQueue.Enqueue(startNode);

            // 시작 노드의 경우 gScore는 0으로 설정
            startNode.gScore = 0f;
            // 시작 노드의 hScore는 목표 노드까지의 거리로 설정
            startNode.hScore = GetPostionScore(startNode, endNode);

            // 닫힌 큐 초기화
            closedQueue = new SortingQueue<Node>();

            Node node = null;

            // 가야할 목적지가 남아있는 동안 반복
            while (openQueue.Count != 0)
            {
                node = openQueue.Dequeue();

                // 목표 노드를 찾았을 경우 함수 종료
                if (node == endNode)
                {
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
            // (Vector3 - Vector3).magnitude를 이용하여 두 노드 사이의 거리를 계산
            Vector3 resultValue = currentNode.position - endNode.position;
            return resultValue.magnitude;
        }
    }
}
