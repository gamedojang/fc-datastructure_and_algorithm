using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackController : MonoBehaviour
{
    [SerializeField] private GameObject stackPanelPrefab;
    [SerializeField] private Transform panelParent;

    private Stack<StackPanelController> stackPanelControllerStack = new Stack<StackPanelController> ();
    private int index = 0;

    private void Start()
    {
        CreateNewStackPanel();
    }

    private void CreateNewStackPanel()
    {
        GameObject stackPanelObject = Instantiate(stackPanelPrefab, panelParent);
        StackPanelController stackPanelController = stackPanelObject.GetComponent<StackPanelController>();

        stackPanelController.Index = index++;

        stackPanelController.stackPanelPreviousDelegate = () =>
        {
            var lastPanel = stackPanelControllerStack.Pop();
            Destroy(lastPanel.gameObject);
            index--;
        };

        stackPanelController.stackPanelNextDelegate = () =>
        {
            CreateNewStackPanel();
        };

        stackPanelControllerStack.Push(stackPanelController);
    }
}
