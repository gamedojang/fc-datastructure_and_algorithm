
public class BinaryTreeNode<T>
{
    public T Value { get; set; }
    public BinaryTreeNode<T> LeftNode { get; set; }
    public BinaryTreeNode<T> RightNode { get; set; }
    public int Height { get; set; }

    public BinaryTreeNode(T value)
    {
        Value = value;
        Height = 1;
    }
}
