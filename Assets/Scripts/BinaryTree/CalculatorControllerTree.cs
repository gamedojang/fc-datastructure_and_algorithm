using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalculatorControllerTree : MonoBehaviour
{
    [SerializeField] private TMP_Text lcdText;

    private Stack<BinaryTreeNode> expression = new Stack<BinaryTreeNode>();

    private Stack<BinaryTreeNode> operatorList = new Stack<BinaryTreeNode>();
    
    private string inputValue = "";

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

    private float CalculateTree(BinaryTreeNode treeNode)
    {
        if (treeNode.LeftNode == null && treeNode.RightNode == null)
        {
            return float.Parse(treeNode.Value);
        }

        switch (treeNode.Value)
        {
            case "+":
                return CalculateTree(treeNode.LeftNode) + CalculateTree(treeNode.RightNode);
            case "-":
                return CalculateTree(treeNode.LeftNode) - CalculateTree(treeNode.RightNode);
            case "*":
                return CalculateTree(treeNode.LeftNode) * CalculateTree(treeNode.RightNode);
            case "/":
                return CalculateTree(treeNode.LeftNode) / CalculateTree(treeNode.RightNode);
            case "%":
                return CalculateTree(treeNode.LeftNode) % CalculateTree(treeNode.RightNode);
        }

        return 0;
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
            case ".":
                if (inputValue.IndexOf('.') > 0 && buttonValue == ".") break; 
                inputValue += buttonValue;
                lcdText.text += buttonValue;
                break;
            case "+":
            case "-":
            case "*":
            case "/":
            case "%":
                if (inputValue == "") break;
                
                BinaryTreeNode node = new BinaryTreeNode(inputValue);
                expression.Push(node);
                inputValue = "";
                lcdText.text += buttonValue;
                
                // 연산자 처리
                if (operatorList.Count > 0)
                {
                    var lastOprator = operatorList.Peek();
                    
                    // 새로운 연산자가 우선 순위가 높으면
                    if (GetOprPriority(lastOprator.Value) <= GetOprPriority(buttonValue))
                    {
                        operatorList.Push(new BinaryTreeNode(buttonValue));
                    }
                    else
                    {
                        while (operatorList.Count > 0)
                        {
                            var lastOperatorNode = operatorList.Pop();

                            lastOperatorNode.RightNode = expression.Pop();
                            lastOperatorNode.LeftNode = expression.Pop();

                            expression.Push(lastOperatorNode);
                        }
                        operatorList.Push(new BinaryTreeNode(buttonValue));
                    }  
                }
                else
                {
                    operatorList.Push(new BinaryTreeNode(buttonValue));
                }
                break;
            case "=":
                if (inputValue == "") break;
                
                expression.Push(new BinaryTreeNode(inputValue));
                inputValue = "";
                
                while (operatorList.Count > 0)
                {
                    var lastOperatorNode = operatorList.Pop();

                    lastOperatorNode.RightNode = expression.Pop();
                    lastOperatorNode.LeftNode = expression.Pop();

                    expression.Push(lastOperatorNode);
                }

                float result = CalculateTree(expression.Pop());
                                
                var resultStr = result.ToString();
                lcdText.text = resultStr;

                expression.Clear();
                inputValue = resultStr;
                break;
            case "ac":
                expression.Clear();
                operatorList = new Stack<BinaryTreeNode>();
                inputValue = "";
                lcdText.text = "";
                break;
        }
    }
}
