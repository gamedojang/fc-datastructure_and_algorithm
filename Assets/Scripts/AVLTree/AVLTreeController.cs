using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AVLTreeController : MonoBehaviour
{
    private void Start()
    {
        AVLTree<int> tree = new AVLTree<int>();

        tree.Insert(1);
        tree.Insert(2);
        tree.Insert(3);
    }
}
