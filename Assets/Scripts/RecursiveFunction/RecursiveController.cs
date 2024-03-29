using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursiveController : MonoBehaviour
{
    private void Start()
    {
        var result = Factorial(10);
        Debug.Log(result);
    }

    private long Factorial(int n)
    {
        if (n == 0)
        {
            return 1;
        }

        return n * Factorial(n - 1);
    }

    private long FactorialWithWhile(int n)
    {
        long result = 1;
        while (n > 0)
        {
            result *= n;
            n--;
        }

        return result;
    }
}
