
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.MemoryProfiler;

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

public class GYLinkedList<T>: IEnumerable, IEnumerator where T : class, IComparable<T>
{
    public GYLinkedListNode<T> Head { get; set; }
    public GYLinkedListNode<T> Last { get; set; }
    
    public int Count { get; set; }
    
    public GYLinkedList(T[] data)
    {
        var node = new GYLinkedListNode<T>(data[0]);
        Head = new GYLinkedListNode<T>(null);
        Head.NextNode = node;
        node.PreviousNode = Head;
        
        Current = Head;

        for (int i = 1; i < data.Length; i++)
        {
            node.NextNode = new GYLinkedListNode<T>(data[i]);
            node.NextNode.PreviousNode = node;
            node = node.NextNode;
        }

        Last = new GYLinkedListNode<T>(null);
        Last.PreviousNode = node;
        node.NextNode = Last;
    }

    public void AddFirst(T value)
    {
        var node = new GYLinkedListNode<T>(value);
        node.NextNode = Head.NextNode;
        node.NextNode.PreviousNode = node;
        Head.NextNode = node;
        node.PreviousNode = Head;
    }

    public void AddLast(T value)
    {
        var node = new GYLinkedListNode<T>(value);
        node.PreviousNode = Last.PreviousNode;
        node.PreviousNode.NextNode = node;
        Last.PreviousNode = node;
        node.NextNode = Last;
    }

    public void Remove(T value)
    {
        var node = Head.NextNode;
        while (node != Last)
        {
            if (node.Value.CompareTo(value) == 0)
            {
                node.PreviousNode.NextNode = node.NextNode;
                node.NextNode.PreviousNode = node.PreviousNode;
                return;
            }

            node = node.NextNode;
        }
    }
    
    public IEnumerator GetEnumerator()
    {
        return this;
    }
    
    public bool MoveNext()
    {
        if (Current.NextNode.Value != null)
        {
            Current = Current.NextNode;
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

    public GYLinkedListNode<T> Current { get; set; }

    object IEnumerator.Current
    {
        get { return Current.Value; }
    }
}

