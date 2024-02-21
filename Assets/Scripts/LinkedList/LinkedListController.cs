using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace LinkedList
{
    public class LinkedListController : MonoBehaviour
    {
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private Transform scrollViewParent;
        [SerializeField] private Transform panelParent;
        [SerializeField] private GameObject addPanel;
        [SerializeField] private GameObject scrollView;

        private List<GameObject> cellObjectList = new List<GameObject>();
        
        private LinkedList<Person> _personLinkedList = new LinkedList<Person>(new Person[]
        {
            new Person("홍길동", 23, Person.GenderType.Male, "프로그래머"),
            new Person("김민기", 23, Person.GenderType.Male, "아티스트"),
            new Person("최민희", 23, Person.GenderType.Female, "아티스트"),
            new Person("박수영", 23, Person.GenderType.Female, "프로그래머"),
            new Person("김민수", 23, Person.GenderType.Male, "기획자"),
        });

        private void Start()
        {
            ReloadData();
        }

        public void Add(Person person)
        {
            _personLinkedList.AddFirst(person);
        }

        public void ShowAddPanel()
        {
            var addPanelObject = Instantiate(addPanel, panelParent);
            addPanelObject.GetComponent<AddPanelController>().addPanelDelegate = person =>
            {
                Add(person);
                ReloadData();
            };
        }

        public void ReloadData()
        {
            foreach (var cellObject in cellObjectList)
            {
                Destroy(cellObject);
            }
            
            foreach (var person in _personLinkedList)
            {
                GameObject cell = Instantiate(cellPrefab, scrollViewParent);
                cellObjectList.Add(cell);

                cell.GetComponent<SubtitleCellController>().Title.text = person.Name;
                cell.GetComponent<SubtitleCellController>().SubTitle.text = person.Job;
            }
        }
    }
}
