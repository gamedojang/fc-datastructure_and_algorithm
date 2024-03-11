using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BinaryTree
{
    public class AVLTreeController : MonoBehaviour
    {
        void Start()
        {
            AVLTree<int> tree = new AVLTree<int>();

            tree.Insert(1);
            tree.Insert(2);
            tree.Insert(3);
            tree.Insert(4);
            tree.Insert(5);
            tree.Insert(6);
            tree.Insert(7);
            tree.Insert(8);
            tree.Insert(9);
            tree.Insert(9);

            Debug.Log("tree : " + tree.Root);
        }
    }
}
