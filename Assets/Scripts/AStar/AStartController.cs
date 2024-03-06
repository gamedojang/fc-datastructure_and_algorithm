using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace AStar
{
    public class AStartController : MonoBehaviour
    {
        // 바닥
        [SerializeField] private GameObject _plane;
        // 플레이어
        [SerializeField] private GameObject _player;

        private float _moveSpeed = 5f;

        // 셀의 크기
        private int cellSize = 1;
        // 바닥 셀의 높이
        private int numOfRows = 10;
        // 바닥 셀의 너비
        private int numOfColumns = 10;

        // 경로 저장
        private Stack<Node> pathList;

        // 장애물
        private GameObject[] obstacles;

        // 시작 지점 위치
        private Vector3 origin = Vector3.zero;

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
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "plane")
                    {
                        Vector3 targetPosition = hit.point;
                        targetPosition = new Vector3(Mathf.Floor(targetPosition.x), targetPosition.y, Mathf.Floor(targetPosition.z)); // Round down the position values

                        // Move the player towards the target position
                        if (_player != null)
                        {
                            // 기존 Nodes의 정보들 제거
                            ClearNodes();

                            (int playerRow, int playerCol) = GetNodeIndex(_player.transform.position);
                            Node startNode = nodes[playerRow, playerCol];

                            (int targetRow, int targetCol) = GetNodeIndex(targetPosition);
                            Node endNode = nodes[targetRow, targetCol];

                            // 경로 탐색
                            pathList = AStar.FindPath(startNode, endNode);
                        }
                    }
                }
            }

            if (pathList != null && pathList.Count > 0)
            {
                Node nextNode = pathList.Peek();
                Vector3 targetPosition = nextNode.position;

                // Move the player towards the target position
                if (_player != null)
                {
                    _player.transform.position = Vector3.MoveTowards(_player.transform.position, targetPosition, _moveSpeed * Time.deltaTime);

                    if (Vector3.Distance(_player.transform.position, targetPosition) < 0.001f)
                    {
                        var popNode = pathList.Pop();
                    }
                }
            }
        }

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
                    Node node = new Node(new Vector3(j, 0, i), index);
                    nodes[i, j] = node;
                    index++;
                }
            }

            // 장애물 위치 설정
            if (obstacles != null && obstacles.Length > 0)
            {
                foreach (GameObject obstacle in obstacles)
                {
                    var obstablePositionIndexes = GetNodeIndex(obstacle.transform.position);
                    nodes[obstablePositionIndexes.row, obstablePositionIndexes.column].isObstacle = true;
                }
            }
        }

        // 주어진 Node의 이웃 Node들을 반환하는 함수
        // 이동 가능한 Node인지 확인하여 반환
        public ArrayList GetAvailableNodes(Node node)
        {
            (int rowAdder,int colAdder)[] indexAdder = { (1, 0), (-1, 0), (0, 1), (0, -1) };

            ArrayList resultList = new ArrayList();
            Vector3 nodePosition = node.position;
            var nodePositionIndex = GetNodeIndex(nodePosition);
 
            int nodeRowIndex;
            int nodeColumnIndex;

            foreach (var adder in indexAdder) 
            {
                nodeRowIndex = nodePositionIndex.row + adder.rowAdder;
                nodeColumnIndex = nodePositionIndex.column + adder.colAdder;

                if (IsAvailableNode(nodeRowIndex, nodeColumnIndex))
                {
                    resultList.Add(nodes[nodeRowIndex, nodeColumnIndex]);
                }
            }

            return resultList;
        }

        /// <summary>
        /// 주어진 row, column이 유효한지 확인하는 함수
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        private bool IsAvailableNode(int rowIndex, int columnIndex)
        {
            // 해당 row, column이 유효하지 않으면 false
            if (!IsAvailableIndex(rowIndex, columnIndex)) return false;

            // 해당 row, column이 장애물이 아닌지 확인
            Node node = nodes[rowIndex, columnIndex];

            if (!node.isObstacle) return true;
            return false;
        }

        /// <summary>
        /// 주어진 위치가 유효한지 확인하는 함수
        /// 즉, x 좌표가 0~9, z 좌표가 0~9 사이에 있는지 확인 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 주어진 row, column index가 유효한지 확인하는 함수
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        private bool IsAvailableIndex(int rowIndex, int columnIndex)
        {
            if (rowIndex > -1 && columnIndex > -1 &&
                rowIndex < numOfRows && columnIndex < numOfColumns)
            {
                return true;
            }
            return false;
        }

        private (int row, int column) GetNodeIndex(Vector3 position)
        {
            int columnIndex = (int)Mathf.Round(position.x) / cellSize;
            int rowIndex = (int)Math.Round(position.z) / cellSize;

            return (rowIndex, columnIndex);
        }
    }
}
