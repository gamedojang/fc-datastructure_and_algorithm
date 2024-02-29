using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CalculatorController : MonoBehaviour
{
    [SerializeField] private TMP_Text lcdText;

    public void OnClickButton(int buttonIndex)
    {
        switch (buttonIndex)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
            case 8:
            case 9:
                lcdText.text += buttonIndex.ToString();
                break;
            case 10:
                // AC

                break;
            case 11:
                // CE

                break;
            case 12:
                // %
                
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                break;
            case 16:
                break;
            case 17:
                break;
            case 18:
                break;
        }
    }
}
