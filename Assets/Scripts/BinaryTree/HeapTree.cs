using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeapTree<T> where T : IComparable
{
    private List<T> Heap;

    public HeapTree()
    {
        Heap = new List<T>();
    }

    private int Parent(int pos)
    {
        return pos / 2;
    }

    private int LeftChild(int pos)
    {
        return (2 * pos);
    }

    private int RightChild(int pos)
    {
        return (2 * pos) + 1;
    }

    private bool IsLeaf(int pos)
    {
        if (pos >= (Heap.Count / 2) && pos <= Heap.Count)
        {
            return true; 
        } 
        return false; 
    }

    private void Swap(int previousIndex, int nextIndex)
    {
        T temp;
        temp = Heap[previousIndex];
        Heap[previousIndex] = Heap[nextIndex];
        Heap[nextIndex] = temp;
    }   

    /// <summary>
    /// 새로운 값은 가장 마지막에 추가한 뒤,
    /// 추가한 값과 추가한 값의 부모와 값 크기를 비교해서 작은 값이 부모 노드가 될 때까지 위치를 바꾼다.
    /// </summary>
    /// <param name="element"></param>
    public void Insert(T element)
    {
        Heap.Add(element);

        int current = Heap.Count - 1;

        while (Heap[current].CompareTo(Heap[Parent(current)]) < 0)
        {
            Swap(current, Parent(current));
            current = Parent(current);
        }
    }

    /// <summary>
    /// MaxHeapify를 호출하여 트리를 재정렬한다.
    /// pos 위치가 Leaf 노드가 아니라면,
    /// pos 위치의 노드의 왼쪽과 오른쪽 자식 노드와 값을 비교해서 재정렬한다.
    /// </summary>
    /// <param name="pos"></param>
    private void MaxHeapify(int pos)
    {
        if (!IsLeaf(pos))
        {
            // pos 위치에 있는 노드가 왼쪽 자식 노드나 오른쪽 자식 노드보다 작다면
            // 왼쪽 자식 노드와 오른쪽 자식 노드 중 큰 값과 위치를 바꾼다.
            if (Heap[pos].CompareTo(Heap[LeftChild(pos)]) > 0 || Heap[pos].CompareTo(Heap[RightChild(pos)]) > 0)
            {
                // 왼쪽 자식 노드가 오른쪽 자식 노드보다 크다면
                // pos 위치에 있는 노드와 왼쪽 자식 노드와 위치를 바꾼다.
                if (Heap[LeftChild(pos)].CompareTo(Heap[RightChild(pos)]) < 0)
                {
                    Swap(pos, LeftChild(pos));
                    MaxHeapify(LeftChild(pos));
                }
                else
                {
                    Swap(pos, RightChild(pos));
                    MaxHeapify(RightChild(pos));
                }
            }
        }   
    }

    /// <summary>
    /// 트리 가장 위에 있는 값을 popped 변수에 임시로 저장한 뒤
    /// 트리의 가장 마지막에 있는 값을 가장 위로 올린다.
    /// 그리고 가장 마지막에 있는 값을 삭제한다.
    /// MaxHeapify를 호출하여 트리를 재정렬한다.
    /// popped 변수를 반환한다.
    /// </summary>
    /// <returns></returns>
    public T Remove()
    {
        T popped = Heap[0];
        Heap[0] = Heap[Heap.Count - 1];
        Heap.RemoveAt(Heap.Count - 1);
        MaxHeapify(0);
        return popped;
    }
}
