using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTreeController : MonoBehaviour
{
    private int[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    public BinaryTreeNode<int> CreateBinaryTree(int startIndex, int endIndex)
    {
        if (startIndex > endIndex) return null;
        int mid = (startIndex + endIndex) / 2;
        BinaryTreeNode<int> node = new BinaryTreeNode<int>(data[mid]);
        node.LeftNode = CreateBinaryTree(startIndex, mid - 1);
        node.RightNode = CreateBinaryTree(mid + 1, endIndex);
        return node;
    }

    public void Traverse(BinaryTreeNode<int> node)
    {
        if (node == null) return;
        Traverse(node.LeftNode);
        Debug.Log(node.Value);
        Traverse(node.RightNode);
    }

    private void Start()
    {
        BinaryTreeNode<int> root = CreateBinaryTree(0, data.Length - 1);
        Traverse(root);

        search(root, 7);
    }

    private void search(BinaryTreeNode<int> node, int value)
    {
        if (node == null) return;
        if (node.Value == value)
        {
            Debug.Log("Value found: " + value);
            return;
        }
        if (value < node.Value)
        {
            search(node.LeftNode, value);
        }
        else
        {
            search(node.RightNode, value);
        }
    }
}
