using LinkedList;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailPanelController : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_InputField ageInputField;
    [SerializeField] private TMP_InputField jobInputField;
    [SerializeField] private Toggle femaleToggle;
    [SerializeField] private Toggle maleToggle;

    public delegate void DetailPanelDelegate(Person person);
    public DetailPanelDelegate detailPanelDelegate;

    private Person person;

    public void SetData(Person person)
    {
        this.person = person;

        nameInputField.text = person.Name;
        ageInputField.text = person.Age.ToString();
        jobInputField.text = person.Job;

        if (person.Gender == Person.GenderType.Female)
        {
            femaleToggle.isOn = true;
        }
        else
        {
            maleToggle.isOn = true;
        }
    }

    public void Delete()
    {
        detailPanelDelegate?.Invoke(person);
        Destroy(gameObject);
    }

    public void Cancel()
    {
        Destroy(gameObject);
    }
}
