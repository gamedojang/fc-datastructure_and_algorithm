using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PriorityQueue<T> where T : IComparable
{
    List<T> heap;

    public PriorityQueue()
    {
        heap = new List<T>();
    }

    public int Count
    {
        get
        {
            return heap.Count;
        }
    }

    public bool Contains(T item)
    {
        return heap.Contains(item);
    }

    int Parent(int pos)
    {
        return (pos - 1) / 2;
    }

    int LeftChild(int pos)
    {
        return (2 * pos) + 1;
    }

    int RightChild(int pos)
    {
        return (2 * pos) + 2;
    }

    void Swap(int prevIndex, int nextIndex)
    {
        T temp;
        temp = heap[prevIndex];
        heap[prevIndex] = heap[nextIndex];
        heap[nextIndex] = temp;
    }

    public void Enqueue(T item)
    {
        heap.Add(item);

        int current = heap.Count - 1;

        while (heap[current].CompareTo(heap[Parent(current)]) < 0)
        {
            Swap(current, Parent(current));
            current = Parent(current);
        }
    }

    public T Dequeue()
    {
        if (heap.Count > 0)
        {
            T result = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            if (heap.Count > 0) Heapify(0);

            return result;
        }
        return default(T);
    }

    bool IsLeaf(int pos)
    {
        if (pos >= heap.Count / 2)
        {
            return true;
        }
        return false;
    }

    void Heapify(int pos)
    {
        if (!IsLeaf(pos))
        {
            int leftChildIndex = LeftChild(pos);
            int rightChildIndex = RightChild(pos);

            T parentValue = heap[pos];

            if (rightChildIndex <= heap.Count - 1)
            {
                T leftChildValue = heap[leftChildIndex];
                T rightChildValue = heap[rightChildIndex];

                if (parentValue.CompareTo(leftChildValue) > 0 ||
                    parentValue.CompareTo(rightChildValue) > 0)
                {
                    if (leftChildValue.CompareTo(rightChildValue) < 0)
                    {
                        Swap(pos, leftChildIndex);
                        Heapify(leftChildIndex);
                    }
                    else
                    {
                        Swap(pos, rightChildIndex);
                        Heapify(rightChildIndex);
                    }
                }
            }
            else
            {
                T leftChildValue = heap[leftChildIndex];

                if (parentValue.CompareTo(leftChildValue) > 0)
                {
                    Swap(pos, leftChildIndex);
                    Heapify(leftChildIndex);
                }
            }
        }
    }
}
