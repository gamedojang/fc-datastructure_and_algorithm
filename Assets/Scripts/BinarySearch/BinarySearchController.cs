using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BinarySearchController : MonoBehaviour
{
    int[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    private void Start()
    {
        Search(9);
    }

    public int Search(int target)
    {
        int left = 0;
        int right = data.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (data[mid] == target)
            {
                return mid;
            }
            else if (data[mid] < target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        return -1;
    }

    private int RecursiveBinarySearch(int target, int left, int right)
    {
        if (left > right)
        {
            return -1;
        }

        int mid = left + (right - left) / 2;

        if (data[mid] == target)
        {
            return mid;
        }
        else if (data[mid] < target)
        {
            return RecursiveBinarySearch(target, mid + 1, right);
        }
        else
        {
            return RecursiveBinarySearch(target, left, right - 1);
        }
    }
}
