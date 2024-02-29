using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalculatorController : MonoBehaviour
{
    [SerializeField] private TMP_Text lcdText;

    private List<string> val = new List<string>();
    private Stack<string> opr = new Stack<string>();
    private string inputValue;

    private int GetOprPriority(string opr)
    {
        switch (opr)
        {
            case "+":
            case "-":
                return 0;
            case "*":
            case "/":
                return 1;
            default:
                return -1;
        }
    }

    private float Calculate(List<string> list)
    {
        Stack<float> resultStack = new Stack<float>();
        
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == "+")
            {
                float secondValue = resultStack.Pop();
                float firstValue = resultStack.Pop();
                
                resultStack.Push(firstValue + secondValue);
            }
            else if (list[i] == "-")
            {
                float secondValue = resultStack.Pop();
                float firstValue = resultStack.Pop();
                
                resultStack.Push(firstValue - secondValue);
            }
            else if (list[i] == "*")
            {
                float secondValue = resultStack.Pop();
                float firstValue = resultStack.Pop();
                
                resultStack.Push(firstValue * secondValue);
            }
            else if (list[i] == "/")
            {
                float secondValue = resultStack.Pop();
                float firstValue = resultStack.Pop();
                
                resultStack.Push(firstValue / secondValue);
            }
            else if (list[i] == "%")
            {
                float secondValue = resultStack.Pop();
                float firstValue = resultStack.Pop();
                
                resultStack.Push(firstValue % secondValue);
            }
            else
            {
                resultStack.Push(float.Parse(list[i]));
            }
        }

        return resultStack.Pop();
    }

    public void OnClickButton(string buttonValue)
    {
        switch (buttonValue)
        {
            case "0":
            case "1":
            case "2":
            case "3":
            case "4":
            case "5":
            case "6":
            case "7":
            case "8":
            case "9":
            case "10":
                inputValue += buttonValue;
                lcdText.text += buttonValue;
                break;
            case "+":
            case "-":
            case "*":
            case "/":
            case "%":
                val.Add(inputValue);
                inputValue = "";
                lcdText.text += buttonValue;
                
                // 연산자 처리
                if (opr.Count > 0)
                {
                    var lastOpr = opr.Peek();
                    
                    // 새로운 연산자가 우선 순위가 높으면
                    if (GetOprPriority(lastOpr) <= GetOprPriority(buttonValue))
                    {
                        opr.Push(buttonValue);
                    }
                    else
                    {
                        while (opr.Count > 0)
                        {
                            val.Add(opr.Pop());
                        }
                        
                        opr.Push(buttonValue);
                    }  
                }
                else
                {
                    opr.Push(buttonValue);
                }
                break;
            case "=":
                val.Add(inputValue);
                inputValue = "";
                
                while (opr.Count > 0)
                {
                    val.Add(opr.Pop());
                }
                
                float result = Calculate(val);
                
                var resultStr = result.ToString();
                lcdText.text = resultStr;

                val.Clear();
                inputValue = resultStr;
                
                Debug.Log(result);
                break;
            case "ac":
                break;
            case "ce":
                break;
        }
    }
}
