
using System;
using System.Collections;
using System.Collections.Generic;

public class GYLinkedListNode<T> where T : IComparable<T>
{
    public T Value { get; set; }
    public GYLinkedListNode<T> PreviousNode { get; set; }
    public GYLinkedListNode<T> NextNode { get; set; }

    public GYLinkedListNode(T value)
    {
        Value = value;
    }
}

public class GYLinkedList<T> : IEnumerable where T : IComparable<T>, IEnumerable<T>
{
    public GYLinkedListNode<T> Head { get; set; }
    public GYLinkedListNode<T> Last { get; set; }
    public GYLinkedListNode<T> Current { get; set; }
    public int Count { get; set; }
    
    public GYLinkedList(T[] data)
    {
        var node = new GYLinkedListNode<T>(data[0]);
        Head = node;

        for (int i = 1; i < data.Length; i++)
        {
            node.NextNode = new GYLinkedListNode<T>(data[i]);
            node = node.NextNode;
        }

        Last = node;
    }

    public void AddFirst(T value)
    {
        var node = new GYLinkedListNode<T>(value);
        node.NextNode = Head;
        Head.PreviousNode = node;
    }

    public void AddLast(T value)
    {
        var node = new GYLinkedListNode<T>(value);
        node.PreviousNode = Last;
        Last.NextNode = node;
    }

    public void Remove(T value)
    {
        var node = Head;
        while (node != null)
        {
            if (node.Value.CompareTo(value) == 0)
            {
                node.PreviousNode.NextNode = node.NextNode;
                node.NextNode.PreviousNode = node.PreviousNode;
            }
        }
    }

    public IEnumerator GetEnumerator()
    {
        return new GYLinkedListNodeEnum()
    }
    
    public class GYLinkedListNodeEnum : IEnumerator
    {
        public GYLinkedListNode<T>[] _nodes;
        
        private int position = -1;
        
        public GYLinkedListNodeEnum(GYLinkedListNode<T>[] nodes)
        {
            _nodes = nodes;
        }
        
        public bool MoveNext()
        {
            position++;
            return (position < _nodes.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        public object Current
        {
            get
            {
                try
                {
                    return _nodes[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
