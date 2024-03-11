
using System.Collections.Generic;
using UnityEngine;

namespace BinaryTree
{
    public class AVLTree<T>
    {
        public BinaryTreeNode<T> Root { get; private set; }

        public int Height(BinaryTreeNode<T> node)
        {
            if (node == null)
            {
                return 0;
            }
            return node.Height;
        }

        // 오른쪽으로 회전
        public BinaryTreeNode<T> RotateRight(BinaryTreeNode<T> y)
        {
            BinaryTreeNode<T> x = y.LeftNode;
            BinaryTreeNode<T> T2 = x.RightNode;

            // 회전
            x.RightNode = y;
            y.LeftNode = T2;

            // 높이 업데이트
            y.Height = 1 + Mathf.Max(Height(y.LeftNode), Height(y.RightNode));
            x.Height = 1 + Mathf.Max(Height(x.LeftNode), Height(x.RightNode));
            
            // 새 루트 반환
            return x;
        }

        // 왼쪽으로 회전
        public BinaryTreeNode<T> RotateLeft(BinaryTreeNode<T> x)
        {
            BinaryTreeNode<T> y = x.RightNode;
            BinaryTreeNode<T> T2 = y.LeftNode;

            // 회전
            y.LeftNode = x;
            x.RightNode = T2;

            // 높이 업데이트
            x.Height = 1 + Mathf.Max(Height(x.LeftNode), Height(x.RightNode));
            y.Height = 1 + Mathf.Max(Height(y.LeftNode), Height(y.RightNode));

            // 새 루트 반환
            return y;
        }

        // 균형 인수 계산
        public int GetBalance(BinaryTreeNode<T> node)
        {
            if (node == null)
            {
                return 0;
            }
            return Height(node.LeftNode) - Height(node.RightNode);
        }

        // 삽입
        public BinaryTreeNode<T> Insert(BinaryTreeNode<T> node, T value)
        {
            // 일반 BST 삽입
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
            node.Height = 1 + Mathf.Max(Height(node.LeftNode), Height(node.RightNode));

            // 균형 인수 계산
            int balance = GetBalance(node);

            // 불균형 발생 시 4가지 경우로 나누어 회전
            // Left Left Case
            if (balance > 1 && Comparer<T>.Default.Compare(value, node.LeftNode.Value) < 0)
            {
                return RotateRight(node);
            }
            // Right Right Case
            if (balance < -1 && Comparer<T>.Default.Compare(value, node.RightNode.Value) > 0)
            {
                return RotateLeft(node);
            }
            // Left Right Case
            if (balance > 1 && Comparer<T>.Default.Compare(value, node.LeftNode.Value) > 0)
            {
                node.LeftNode = RotateLeft(node.LeftNode);
                return RotateRight(node);
            }
            // Right Left Case
            if (balance < -1 && Comparer<T>.Default.Compare(value, node.RightNode.Value) < 0)
            {
                node.RightNode = RotateRight(node.RightNode);
                return RotateLeft(node);
            }

            return node;
        }

        public void Insert(T value)
        {
            Root = Insert(Root, value);
        }
    }
}