using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeapTreeController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HeapTree<int> tree = new HeapTree<int>();
        tree.Insert(5);
        tree.Insert(15);
        tree.Insert(32);
        tree.Insert(4);
        tree.Insert(7);
        tree.Insert(9);
        tree.Insert(2);
        tree.Insert(1);
        tree.Insert(35);
        tree.Insert(6);
        tree.Insert(8);

        int result = tree.Remove();
        result = tree.Remove();
        result = tree.Remove();
        result = tree.Remove();
        result = tree.Remove();

        Debug.Log("tree : " + tree);

    }
}
