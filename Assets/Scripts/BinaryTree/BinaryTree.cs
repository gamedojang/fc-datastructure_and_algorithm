
using JetBrains.Annotations;
using UnityEngine;

public class TreeNode<T> 
{
    public T Value { get; set; }
    public TreeNode<T> LeftNode { get; set; }
    public TreeNode<T> RightNode { get; set; }

    public TreeNode(T value)
    {
        Value = value;
    }

    public void AddSubTree(TreeNode<T> node)
    {
        
    }
}
