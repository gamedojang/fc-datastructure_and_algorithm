using System;
using UnityEngine;

namespace AStar
{
    public class Node : IComparable
    {
        public float FScore             // F = G + H
        {
            get
            {
                return hScore + gScore;
            }
        }

        public int index;

        public float hScore;            // 현재 노드에서 목표 노드까지의 예상 거리 값
        public float gScore;            // 출발 노드에서 현재 노드까지의 실제 거리 값
        public Node parent;             // 부모 노드

        public Vector3 position;        // 위치 정보
        public bool isObstacle;         // 장애물이 있는지 여부

        public Node(Vector3 position, int index)
        {
            this.hScore = 0f;
            this.gScore = 0f;
            this.isObstacle = false;
            this.parent = null;
            this.position = position;
            this.index = index;
        }

        public void Clear()
        {
            this.hScore = 0f;
            this.gScore = 0f;
            this.parent = null;
        }

        public int CompareTo(object obj)
        {
            Node node = (Node)obj;

            if (this.FScore < node.FScore)
            {
                return -1;
            }
            else if (this.FScore > node.FScore)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public override string ToString()
        {
            return "[ " + this.index + "]";
        }
    }
}
