
using System;
using JetBrains.Annotations;
using UnityEngine;

public class BinaryTreeNode<T>
{
    public T Value { get; set; }
    public BinaryTreeNode<T> LeftNode { get; set; }
    public BinaryTreeNode<T> RightNode { get; set; }

    public BinaryTreeNode(T value)
    {
        Value = value;
    }
}
