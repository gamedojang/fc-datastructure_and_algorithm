using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    [SerializeField] private GameObject stackPanelPrefab;
    [SerializeField] private Transform canvasTransform;
    private Stack<StackPanelController> panelStack = new Stack<StackPanelController>();

    private int index = 0;
    
    private void Start()
    {
        CreateNewStackPanel();
    }
    
    void CreateNewStackPanel()
    {
        var stackPanelController = Instantiate(stackPanelPrefab, canvasTransform).GetComponent<StackPanelController>();
        stackPanelController.Index = index++;
        stackPanelController.stackPanelPreviousDelegate = () =>
        {
            var lastPanel = panelStack.Pop();
            Destroy(lastPanel.gameObject);
        };
        stackPanelController.stackPanelNextDelegate = () =>
        {
            CreateNewStackPanel();
        };
        panelStack.Push(stackPanelController);
    }
}
