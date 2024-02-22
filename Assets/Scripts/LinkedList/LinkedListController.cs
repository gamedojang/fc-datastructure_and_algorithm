using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace LinkedList
{
    public class LinkedListController : SceneController
    {
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private Transform scrollViewParent;
        [SerializeField] private Transform panelParent;
        [SerializeField] private GameObject addPanel;
        [SerializeField] private GameObject detailPanel;
        [SerializeField] private GameObject confirmPanel;

        private List<GameObject> cellObjectList = new List<GameObject>();
        
        private LinkedList<Person> _personLinkedList = new LinkedList<Person>(new Person[]
        {
            new Person("홍길동", 23, Person.GenderType.Male, "프로그래머"),
            new Person("김민기", 23, Person.GenderType.Male, "아티스트"),
            new Person("최민희", 23, Person.GenderType.Female, "아티스트"),
            new Person("박수영", 23, Person.GenderType.Female, "프로그래머"),
            new Person("김민수", 23, Person.GenderType.Male, "기획자")
        });

        private GYLinkedList<Person>_personLinkedList2 = new GYLinkedList<Person>(new Person[]
        {
            new Person("홍길동", 23, Person.GenderType.Male, "프로그래머"),
            new Person("김민기", 23, Person.GenderType.Male, "아티스트"),
            new Person("최민희", 23, Person.GenderType.Female, "아티스트"),
            new Person("박수영", 23, Person.GenderType.Female, "프로그래머"),
            new Person("김민수", 23, Person.GenderType.Male, "기획자")    
        });

        private void Start()
        {
            ReloadData();
        }

        public void Add(Person person)
        {
            _personLinkedList2.AddFirst(person);
        }

        public void Remove(Person person)
        {
            _personLinkedList2.Remove(person);
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

            int index = 0;
            foreach (var person in _personLinkedList2)
            {
                GameObject cell = Instantiate(cellPrefab, scrollViewParent);
                cellObjectList.Add(cell);

                SubtitleCellController subtitleCellController = cell.GetComponent<SubtitleCellController>();

                subtitleCellController.Title.text = person.Name;
                
                subtitleCellController.SubTitle.text = person.Job;
                subtitleCellController.Index = index++;

                subtitleCellController.openDetailPanelDelegate = i =>
                {
                    var detailPanelObject = Instantiate(detailPanel, panelParent);
                    var detailPanelController = detailPanelObject.GetComponent<DetailPanelController>();
                    detailPanelController.SetData(person, this);
                    detailPanelController.deletePersonDelegate = target =>
                    {
                        Remove(target);
                        ReloadData();
                    };
                };
            }
        }

        public override void OpenConfirmPopup(string message, ConfirmPopupController.ConfirmPopupDelegate confirmPopupDelegate)
        {
            var confimPanelObject = Instantiate(confirmPanel, panelParent);
            var confirmPanelController = confimPanelObject.GetComponent<ConfirmPopupController>();
            confirmPanelController.SetMessage(message);
            confirmPanelController.confirmPopupDelegate = confirmPopupDelegate;
        }
    }
}
