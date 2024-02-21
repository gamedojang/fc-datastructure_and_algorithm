using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayListAndHashtableController : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Transform parent;
    
    private ArrayList _personList = new ArrayList()
    {
        new Hashtable()
        {
            ["name"] = "홍길동",
            ["age"] = "23",
            ["gender"] = "남자",
            ["job"] = "프로그래머"
        },
        new Hashtable()
        {
            ["name"] = "김민기",
            ["age"] = "23",
            ["gender"] = "남자",
            ["job"] = "아티스트"
        },
        new Hashtable()
        {
            ["name"] = "최민희",
            ["age"] = "23",
            ["gender"] = "여자",
            ["job"] = "아티스트"
        },
        new Hashtable()
        {
            ["name"] = "박수영",
            ["age"] = "23",
            ["gender"] = "여자",
            ["job"] = "프로그래머"
        },
        new Hashtable()
        {
            ["name"] = "김민수",
            ["age"] = "23",
            ["gender"] = "남자",
            ["job"] = "아티스트"
        }
    };
    
    private void Start()
    {
        foreach (Hashtable person in _personList)
        {
            GameObject cell = Instantiate(cellPrefab, parent);
            
            string val = person["name"].ToString();
            
            cell.GetComponent<SubtitleCellController>().Title.text = person["name"].ToString();
            cell.GetComponent<SubtitleCellController>().SubTitle.text = person["job"].ToString();
        }
    }
}
