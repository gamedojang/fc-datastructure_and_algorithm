using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StackPanelController : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    public delegate void StackPanelNextDelegate();
    public delegate void StackPanelPreviousDelegate();

    public StackPanelNextDelegate stackPanelNextDelegate;
    public StackPanelPreviousDelegate stackPanelPreviousDelegate;

    public int Index
    {
        get
        {
            return Index;
        }

        set
        {
            text.text = value.ToString();

            if (value > 0 )
            {
                previousButton.interactable = true;
            }
        }
    }

    public void Next()
    {
        stackPanelNextDelegate?.Invoke();
    }

    public void Previous() 
    { 
        stackPanelPreviousDelegate?.Invoke();
    }
}
