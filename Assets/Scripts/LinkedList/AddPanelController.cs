using System;
using System.Collections;
using System.Collections.Generic;
using LinkedList;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddPanelController : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_InputField ageInputField;
    [SerializeField] private Toggle femaleToggle;
    [SerializeField] private Toggle maleToggle;
    [SerializeField] private TMP_InputField jobInputField;
    [SerializeField] private Button okButton;

    public delegate void AddPanelDelegate(Person person);
    public AddPanelDelegate addPanelDelegate;

    private void Update()
    {
        if (nameInputField.text != "" && ageInputField.text != "")
        {
            okButton.interactable = true;
        }
        else
        {
            okButton.interactable = false;
        }
    }

    public void Ok()
    {
        var person = new Person(nameInputField.text, int.Parse(ageInputField.text),
            femaleToggle.isOn ? Person.GenderType.Female : Person.GenderType.Male, jobInputField.text);

        addPanelDelegate.Invoke(person);
        Destroy(gameObject);
    }

    public void Cancel()
    {
        Destroy(gameObject);
    }
}
