
using System.Collections.Generic;
using UnityEngine;

public class AVLTree<T>
{
    public BinaryTreeNode<T> Root { get; set; }

    public int NodeHeight(BinaryTreeNode<T> node)
    {
        if (node == null)
        {
            return 0;
        }
        return node.Height;
    }

    public int GetBalanceFactor(BinaryTreeNode<T> node)
    {
        if (node == null)
        {
            return 0;
        }
        return NodeHeight(node.LeftNode) - NodeHeight(node.RightNode);
    }

    public void Insert(T value)
    {
        Root = Insert(Root, value);
    }

    public BinaryTreeNode<T> Insert(BinaryTreeNode<T> node, T value) 
    {
        if (node == null)
        {
            return new BinaryTreeNode<T>(value);
        }

        if (Comparer<T>.Default.Compare(value, node.Value) < 0) 
        { 
            node.LeftNode = Insert(node.LeftNode, value);
        }
        else if (Comparer<T>.Default.Compare(value, node.Value) > 0)
        {
            node.RightNode = Insert(node.RightNode, value);
        }
        else
        {
            return node;
        }

        // 높이 업데이트
        node.Height = 1 + Mathf.Max(NodeHeight(node.LeftNode), NodeHeight(node.RightNode));

        // 균형 인수 계산
        int balanceFactor = GetBalanceFactor(node);

        // 불균형 상태
        // LL Case
        if (balanceFactor > 1 && Comparer<T>.Default.Compare(value, node.LeftNode.Value) < 0)
        {
            // 오른쪽으로 회전
            return RotateRight(node);
        }

        // RR Case
        if (balanceFactor < -1 && Comparer<T>.Default.Compare(value, node.RightNode.Value) > 0)
        {
            // 왼쪽으로 회전
            return RotateLeft(node);
        }

        // LR Case
        if (balanceFactor > 1 && Comparer<T>.Default.Compare(value, node.LeftNode.Value) > 0)
        {
            // 왼쪽으로 회전
            // 오른쪽으로 회전
            node.LeftNode = RotateLeft(node.LeftNode);
            return RotateRight(node);
        }

        // RL Case
        if (balanceFactor < -1 && Comparer<T>.Default.Compare(value, node.RightNode.Value) < 0)
        {
            // 오른쪽으로 회전
            // 왼쪽으로 회전
            node.RightNode = RotateRight(node.RightNode);
            return RotateLeft(node);
        }

        return node;
    }

    // 오른쪽 회전
    BinaryTreeNode<T> RotateRight(BinaryTreeNode<T> node)
    {
        BinaryTreeNode<T> x = node.LeftNode;
        BinaryTreeNode<T> T2 = x.RightNode;

        // 회전
        x.RightNode = node;
        node.LeftNode = T2;

        // 높이 업데이트
        node.Height = 1 + Mathf.Max(NodeHeight(node.LeftNode), NodeHeight(node.RightNode));
        x.Height = 1 + Mathf.Max(NodeHeight(x.LeftNode), NodeHeight(x.RightNode));

        // 새로운 루트 노드 반환
        return x;
    }

    // 왼쪽 회전
    BinaryTreeNode<T> RotateLeft(BinaryTreeNode<T> node)
    {
        BinaryTreeNode<T> y = node.RightNode;
        BinaryTreeNode<T> T2 = y.LeftNode;

        // 회전
        y.LeftNode = node;
        node.RightNode = T2;

        // 높이 업데이트
        node.Height = 1 + Mathf.Max(NodeHeight(node.LeftNode), NodeHeight(node.RightNode));
        y.Height = 1 + Mathf.Max(NodeHeight(y.LeftNode), NodeHeight(y.RightNode));

        // 새로운 루트 노드 반환
        return y;
    }
}
