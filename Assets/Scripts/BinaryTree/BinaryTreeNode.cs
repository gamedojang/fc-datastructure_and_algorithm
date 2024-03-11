
using System;
using JetBrains.Annotations;
using UnityEngine;

namespace BinaryTree
{
    public class BinaryTreeNode<T>
    {
        public T Value { get; set; }
        public BinaryTreeNode<T> LeftNode { get; set; }
        public BinaryTreeNode<T> RightNode { get; set; }
        public int Height { get; set; }     // AVL

        public BinaryTreeNode(T value)
        {
            Value = value;
            Height = 1;     // AVL
        }
    }

}