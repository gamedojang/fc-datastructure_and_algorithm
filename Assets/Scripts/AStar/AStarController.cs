using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AStarController : MonoBehaviour
{
    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject player;

    float moveSpeed = 5f;

    // 셀의 크기
    int cellSize = 1;

    // 셀의 높이
    int numOfRows = 10;

    // 셀의 너비
    int numOfColumns = 10;

    // 경로 저장
    Stack<AStarNode> pathList;

    // 장애물
    GameObject[] obstacles;

    // 시작 지점 위치
    Vector3 origin = Vector3.zero;

    // AStarNode 배열
    AStarNode[,] nodes;

    // AStarController 싱글턴
    static AStarController instance = null;
    public static AStarController Instance
    {
        get
        {
            if (!instance)
            {
                instance = FindAnyObjectByType(typeof(AStarController)) as AStarController;
                if (!instance)
                {
                    Debug.Log("AStarController가 씬에 존재하지 않습니다.");
                }
            }
            return instance;
        }
    }

    private void Start()
    {
        obstacles = GameObject.FindGameObjectsWithTag("obstacle");

        // nodes 배열 초기화
        InitNodes();
    }

    private void Update()
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
                    targetPosition = new Vector3(Mathf.Floor(targetPosition.x), 
                        targetPosition.y, 
                        Mathf.Floor(targetPosition.z));

                    if (player != null)
                    {
                        // 기존 Nodes의 정보를 제거
                        ClearNodes();

                        (int playerRow, int playerCol) = GetNodeIndex(player.transform.position);
                        AStarNode startNode = nodes[playerRow, playerCol];

                        (int targetRow, int targetCol) = GetNodeIndex(targetPosition);
                        AStarNode endNode = nodes[targetRow, targetCol];

                        // AStar 알고리즘 사용
                        pathList = AStar.FindPath(startNode, endNode);
                    }
                }
            }
        }

        // Player 이동 처리
        if (pathList != null && pathList.Count > 0)
        {
            AStarNode nextNode = pathList.Peek();
            Vector3 targetPosition = nextNode.position;

            if (player != null)
            {
                player.transform.position = Vector3.MoveTowards(player.transform.position,
                    targetPosition, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(player.transform.position, targetPosition) < 0.001f)
                {
                    var popNode = pathList.Pop();
                }
            }
        }
    }

    void ClearNodes()
    {
        foreach (AStarNode node in nodes)
        {
            node.Clear();
        }
    }

    void InitNodes()
    {
        nodes = new AStarNode[numOfRows, numOfColumns];

        // Node의 위치 계산
        int index = 0;

        for (int i = 0; i < numOfRows; i++)
        {
            for (int j = 0; j < numOfColumns; j++)
            {
                AStarNode node = new AStarNode(new Vector3(j, 0, i), index);
                nodes[i, j] = node;
                index++;
            }
        }

        // 장애물 위치 설정
        if (obstacles != null && obstacles.Length > 0)
        {
            foreach (GameObject obstacle in obstacles)
            {
                var obstaclePositionIndexes = GetNodeIndex(obstacle.transform.position);
                nodes[obstaclePositionIndexes.row, obstaclePositionIndexes.column].isObstacle = true;
            }
        }
    }

    (int row, int column) GetNodeIndex(Vector3 position)
    {
        int columnIndex = (int)Mathf.Round(position.x) / cellSize;
        int rowIndex = (int)Mathf.Round(position.z) / cellSize;

        return (rowIndex, columnIndex);
    }

    // 주어진 Node의 이웃 Node들을 반환하는 메서드
    // 이동 가능한 Node인지 확인하여 반환
    public ArrayList GetAvailableNodes(AStarNode node)
    {
        (int rowAdder, int colAdder)[] indexAdder = { (1, 0), (-1, 0), (0, 1), (0, -1) };

        ArrayList resultList = new ArrayList();
        Vector3 nodePosition = node.position; ;
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

    bool IsAvailableNode(int rowIndex, int columnIndex)
    {
        // 해당 row, column이 유효하지 않으면 false
        if (!IsAvailableIndex(rowIndex, columnIndex)) return false;

        // 해당 row, column이 장애물이 아닌지 확인
        AStarNode node = nodes[rowIndex, columnIndex];
        if (!node.isObstacle)
        {
            return true;
        }
        return false;
    }

    bool IsAvailableIndex(int rowIndex, int columnIndex)
    {
        if (rowIndex > -1 && columnIndex > -1 && rowIndex < numOfRows && columnIndex < numOfColumns)
        {
            return true;
        }
        return false;
    }
}
