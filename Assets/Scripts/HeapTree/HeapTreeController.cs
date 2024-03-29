using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeapTreeController : MonoBehaviour
{
    private void Start()
    {
        HeapTree<int> heapTree = new HeapTree<int>();
        heapTree.Insert(5);
        heapTree.Insert(15);
        heapTree.Insert(32);
        heapTree.Insert(4);
        heapTree.Insert(7);
        heapTree.Insert(9);

        int? result = heapTree.Remove();
        result = heapTree.Remove();
        result = heapTree.Remove();
        result = heapTree.Remove();
        result = heapTree.Remove();
        result = heapTree.Remove();
        result = heapTree.Remove();
        result = heapTree.Remove();
        result = heapTree.Remove();
        result = heapTree.Remove();
    }
}
