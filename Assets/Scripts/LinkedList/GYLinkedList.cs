using System.Collections;

public class GYLinkedListNode<T>
{
    public T Value { get; set; }
    public GYLinkedListNode<T> previousNode { get; set; }
    public GYLinkedListNode<T> nextNode { get; set; }

    public GYLinkedListNode(T value)
    {
        Value = value;
    }
}

public class GYLinkedList<T> : IEnumerable, IEnumerator where T : class
{
    public GYLinkedListNode<T> Head { get; set; }
    public GYLinkedListNode<T> Tail { get; set; }

    public int Count { get; set; }

    public GYLinkedListNode<T> Current { get; set; }
    object IEnumerator.Current
    {
        get { return Current.Value; }
    }

    public GYLinkedList(T[] data)
    {
        var node = new GYLinkedListNode<T>(data[0]);
        Head = new GYLinkedListNode<T>(null);
        Head.nextNode = node;
        node.previousNode = Head;

        Current = Head;

        for (int i = 1; i < data.Length; i++)
        {
            node.nextNode = new GYLinkedListNode<T>(data[i]);
            node.nextNode.previousNode = node;
            node = node.nextNode;
        }

        Tail = new GYLinkedListNode<T>(null);
        Tail.previousNode = node;
        node.nextNode = Tail;
    }

    public void AddFirst(T value)
    {
        var node = new GYLinkedListNode<T>(value);
        node.nextNode = Head.nextNode;
        node.nextNode.previousNode = node;
        Head.nextNode = node;
        node.previousNode = Head;
    }

    public void AddLast(T value)
    {
        var node = new GYLinkedListNode<T>(value);
        node.previousNode = Tail.previousNode;
        node.previousNode.nextNode = node;
        Tail.previousNode = node;
        node.nextNode = Tail;
    }

    public void Remove(T value)
    {
        var node = Head.nextNode;
        while (node != Tail)
        {
            if (node.Value == value)
            {
                node.previousNode.nextNode = node.nextNode;
                node.nextNode.previousNode = node.previousNode;
                return;
            }
            node = node.nextNode;
        }
    }

    public bool MoveNext()
    {
        if (Current.nextNode.Value != null)
        {
            Current = Current.nextNode;
            return true;
        }
        else
        {
            Reset();
            return false;
        }
    }

    public void Reset()
    {
        Current = Head;
    }

    public IEnumerator GetEnumerator()
    {
        return this;
    }
}
