
using System;
using JetBrains.Annotations;
using UnityEngine;

public class BinaryTreeNode
{
    public string Value { get; set; }
    public BinaryTreeNode LeftNode { get; set; }
    public BinaryTreeNode RightNode { get; set; }

    public BinaryTreeNode(string value)
    {
        Value = value;
    }
}
