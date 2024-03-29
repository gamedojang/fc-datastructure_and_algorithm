using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTreeController : MonoBehaviour
{
    int[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    private void Start()
    {
        BinaryTreeNode<int> root = CreateBinaryTree(0, data.Length - 1);

        Search(root, 7);

        Traverse(root);
    }

    public BinaryTreeNode<int> CreateBinaryTree(int startIndex, int endIndex)
    {
        if (startIndex > endIndex) return null;

        int mid = (startIndex + endIndex) / 2;

        BinaryTreeNode<int> node = new BinaryTreeNode<int>(data[mid]);
        node.LeftNode = CreateBinaryTree(startIndex, mid - 1);
        node.RightNode = CreateBinaryTree(mid + 1, endIndex);
        return node;
    }

    void Search(BinaryTreeNode<int> node, int value)
    {
        if (node == null) return;
        if (node.Value == value)
        {
            Debug.Log("Value found!!");
            return;
        }
        if (value < node.Value)
        {
            Search(node.LeftNode, value);
        }
        else
        {
            Search(node.RightNode, value);
        }
    }

    void Traverse(BinaryTreeNode<int> node)
    {
        if (node == null) return;
        Traverse(node.LeftNode);
        Debug.Log(node.Value);
        Traverse(node.RightNode);
    }
}
