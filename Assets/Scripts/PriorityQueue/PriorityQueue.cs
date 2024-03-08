
using System;
using System.Collections.Generic;
using LinkedList;
using Unity.VisualScripting;

namespace PriorityQueue
{
    public class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> _data = new List<T>();
     
        public void Enqueue(T item)
        {
            // 데이터를 추가하면 List의 가장 마지막에 데이터를 추가하고,
            // 추가된 데이터는 자신의 부모 노드와 값을 비교하면서 위치를 정렬한다.
            // 
            _data.Add(item);
            int index = _data.Count - 1;
            while (index > 0)
            {
                int parent = (index - 1) / 2;
                if (_data[index].CompareTo(_data[parent]) >= 0)
                    break;

                T temp = _data[index];
                _data[index] = _data[parent];
                _data[parent] = temp;
                index = parent;
            }
        }

        public T Dequeue()
        {
            // 데이터를 삭제하면 List의 가장 마지막 데이터를 삭제하고,
            // 삭제된 데이터는 가장 첫번째 데이터와 값을 비교하면서 위치를 정렬한다.
            int lastIndex = _data.Count - 1;
            T frontItem = _data[0];
            _data[0] = _data[lastIndex];
            _data.RemoveAt(lastIndex);
            lastIndex--;

            int parent = 0;
            while (true)
            {
                int left = parent * 2 + 1;
                if (left > lastIndex)
                    break;

                int right = left + 1;
                int min = left;
                if (right <= lastIndex && _data[right].CompareTo(_data[left]) < 0)
                    min = right;

                if (_data[parent].CompareTo(_data[min]) <= 0)
                    break;

                T temp = _data[parent];
                _data[parent] = _data[min];
                _data[min] = temp;
                parent = min;
            }

            return frontItem;
        }

        public T Peek()
        {
            if (_data.Count == 0)
                throw new InvalidOperationException("Queue is empty.");
            return _data[0];
        }

        public int Count
        {
            get { return _data.Count; }
        }
    }
}