using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class ArrayListController : MonoBehaviour
{
    [SerializeField]
    private GameObject cellPrefab;

    [SerializeField] private Transform parent;
    
    private ArrayList _nameList = new ArrayList()
    {
        "홍길동", "김민기", "최민희", "박수영", "김민수"
    };
    
    private void Start()
    {
        foreach (string name in _nameList)
        {
            GameObject cell = Instantiate(cellPrefab, parent);
            cell.GetComponent<BasicCellController>().Text.text = name;
        }
    }
}
