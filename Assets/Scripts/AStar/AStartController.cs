using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AStar
{
    public class AStartController : MonoBehaviour
    {
        [SerializeField] private GameObject _plane;
        [SerializeField] private GameObject _player;

        private Vector3 _targetPosition;
        private float _moveSpeed = 5f;

        // 셀의 크기
        private int cellSize = 1;
        private int numOfRows = 10;
        private int numOfColumns = 10;

        // 경로 저장
        private Stack<Node> pathList;

        // 장애물
        private GameObject[] obstacles;

        // 시작 지점 위치
        private Vector3 origin = new Vector3();

        // 목표 지점 위치
        private Transform startTransform, endTransform;

        // Node 배열
        private Node[,] nodes;

        // GameController 싱글톤 패턴
        private static AStartController instance = null;
        public static AStartController Instance
        {
            get
            {
                if (!instance)
                {
                    instance = FindObjectOfType(typeof(AStartController)) as AStartController;
                    if (!instance)
                    {
                        Debug.Log("AStartController가 씬에 존재하지 않습니다.");
                    }
                }
                return instance;
            }
        }

        private void Start()
        {
            // 장애물 정보 수집
            obstacles = GameObject.FindGameObjectsWithTag("obstacle");

            // Nodes 배열 초기화
            InitNodes();
        }

        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("### Input.GetMouseButtonDown(0)");
                
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "plane")
                    {
                        Vector3 position = hit.point;
                        position = new Vector3(Mathf.Floor(position.x), position.y, Mathf.Floor(position.z)); // Round down the position values
                        Debug.Log("Clicked position: " + position);

                        _targetPosition = position;

                        // Move the player towards the target position
                        if (_player != null)
                        {
                            // 기존 Nodes의 정보들 제거
                            ClearNodes();

                            int nodeIndex, nodeRowIndex, nodeColumnIndex;
                            nodeIndex = GetNodeIndex(_player.transform.position);
                            nodeRowIndex = GetRowIndex(nodeIndex);
                            nodeColumnIndex = GetColumnIndex(nodeIndex);

                            Node startNode = nodes[nodeRowIndex, nodeColumnIndex];

                            nodeIndex = GetNodeIndex(_targetPosition);
                            nodeRowIndex = GetRowIndex(nodeIndex);
                            nodeColumnIndex = GetColumnIndex(nodeIndex);

                            Node endNode = nodes[nodeRowIndex, nodeColumnIndex];

                            // 경로 탐색
                            pathList = AStar.FindPath(startNode, endNode);
                            Debug.Log("@@@@ : " + pathList);
                        }
                    }
                }
            }

            if (pathList != null && pathList.Count > 0)
            {
                Node nextNode = pathList.Peek();
                _targetPosition = nextNode.position;

                // Move the player towards the target position
                if (_player != null)
                {
                    _player.transform.position = Vector3.MoveTowards(_player.transform.position, _targetPosition, _moveSpeed * Time.deltaTime);

                    if (Vector3.Distance(_player.transform.position, _targetPosition) < 0.001f)
                    {
                        var popNode = pathList.Pop();
                        Debug.Log("### pathList.Pop() : " + popNode.position);
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClearNodes()
        {
            foreach (Node node in nodes)
            {
                node.Clear();
            }
        }

        private void InitNodes()
        {
            nodes = new Node[numOfRows, numOfColumns];

            // Node 위치 계산
            int index = 0;

            for (int i = 0; i < numOfRows; i++)
            {
                for (int j = 0; j < numOfColumns; j++)
                {
                    Vector3 nodePosition = GetNodePosition(index);
                    Node node = new Node(nodePosition, index);
                    nodes[i, j] = node;
                    index++;
                }
            }

            // 장애물 위치 설정
            if (obstacles != null && obstacles.Length > 0)
            {
                foreach (GameObject obstacle in obstacles)
                {
                    int nodeIndex = GetNodeIndex(obstacle.transform.position);
                    int columnIndex = GetColumnIndex(nodeIndex);
                    int rowIndex = GetRowIndex(nodeIndex);

                    nodes[rowIndex, columnIndex].isObstacle = true;
                }
            }
        }

        // 주어진 Node의 이웃 Node들을 반환하는 함수
        // 이동 가능한 Node인지 확인하여 반환
        public ArrayList GetAvailableNodes(Node node)
        {
            ArrayList resultList = new ArrayList();
            Vector3 nodePosition = node.position;
            int nodeIndex = GetNodeIndex(nodePosition);
            int rowIndex = GetRowIndex(nodeIndex);
            int columnIndex = GetColumnIndex(nodeIndex);
            int nodeRowIndex;
            int nodeColumnIndex;

            // 위
            nodeRowIndex = rowIndex + 1;
            nodeColumnIndex = columnIndex;
            if (IsAvailableNode(nodeRowIndex, nodeColumnIndex))
            {
                resultList.Add(nodes[nodeRowIndex, nodeColumnIndex]);
            }

            // 아래
            nodeRowIndex = rowIndex - 1;
            nodeColumnIndex = columnIndex;
            if (IsAvailableNode(nodeRowIndex, nodeColumnIndex))
            {
                resultList.Add(nodes[nodeRowIndex, nodeColumnIndex]);
            }

            // 오른쪽
            nodeRowIndex = rowIndex;
            nodeColumnIndex = columnIndex + 1;
            if (IsAvailableNode(nodeRowIndex, nodeColumnIndex))
            {
                resultList.Add(nodes[nodeRowIndex, nodeColumnIndex]);
            }

            // 왼쪽
            nodeRowIndex = rowIndex;
            nodeColumnIndex = columnIndex - 1;
            if (IsAvailableNode(nodeRowIndex, nodeColumnIndex))
            {
                resultList.Add(nodes[nodeRowIndex, nodeColumnIndex]);
            }

            return resultList;
        }

        // 주어진 Row, Column index가 유효한 Node인지 확인하는 함수
        private bool IsAvailableNode(int rowIndex, int columnIndex)
        {
            // 해당 row, column이 유효하지 않으면 false
            if (!IsAvailableIndex(rowIndex, columnIndex)) return false;

            // 해당 row, column이 장애물이 아닌지 확인
            Node node = nodes[rowIndex, columnIndex];

            if (!node.isObstacle) return true;
            return false;
        }

        private bool IsAvailablePosition(Vector3 position)
        {
            float availableWidht = numOfColumns * cellSize;
            float availableHeight = numOfRows * cellSize;
            if (position.x >= origin.x && position.x <= origin.x + availableWidht &&
                position.z >= origin.z && position.z <= origin.z + availableHeight)
            {
                return true;
            }
            return false;
        }

        // 주어진 row, column index가 유효한지 확인하는 함수
        private bool IsAvailableIndex(int rowIndex, int columnIndex)
        {
            if (rowIndex > -1 && columnIndex > -1 &&
                rowIndex < numOfRows && columnIndex < numOfColumns)
            {
                return true;
            }
            return false;
        }

        private Vector3 GetNodePosition(int index)
        {
            int rowIndex = GetRowIndex(index);
            int columnIndex = GetColumnIndex(index);

            float xPosition = columnIndex * cellSize;
            float zPosition = rowIndex * cellSize;

            return new Vector3(xPosition, 0f, zPosition);
        }

        private int GetNodeIndex(Vector3 position)
        {
            if (!IsAvailablePosition(position))
            {
                return -1;
            }

            int test = (int)Math.Round(position.x);

            int columnIndex = (int)Mathf.Round(position.x) / cellSize;
            int rowIndex = (int)Math.Round(position.z) / cellSize;

            return (rowIndex * numOfColumns + columnIndex);
        }

        private int GetRowIndex(int nodeIndex)
        {
            int rowIndex = nodeIndex / numOfColumns;
            return rowIndex;
        }

        private int GetColumnIndex(int nodeIndex)
        {
            int columnIndex = nodeIndex % numOfColumns;
            return columnIndex;
        }
    }
}
