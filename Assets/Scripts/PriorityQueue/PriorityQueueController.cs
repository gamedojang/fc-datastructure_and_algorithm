using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using LinkedList;

namespace PriorityQueue
{
    public class PriorityQueueController : SceneController
    {
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private Transform scrollViewParent;
        [SerializeField] private Transform panelParent;
        [SerializeField] private GameObject addPanel;
        [SerializeField] private GameObject detailPanel;
        [SerializeField] private GameObject confirmPanel;

        private List<GameObject> cellObjectList = new List<GameObject>();

        PriorityQueue<Person2> _personPriorityQueue = new PriorityQueue<Person2>();

        private void Start()
        {
            _personPriorityQueue.Enqueue(new Person2("홍길동", 23, Person2.GenderType.Male, "프로그래머"));
            _personPriorityQueue.Enqueue(new Person2("김민기", 24, Person2.GenderType.Male, "프로그래머"));
            _personPriorityQueue.Enqueue(new Person2("최민희", 33, Person2.GenderType.Female, "아티스트"));
            _personPriorityQueue.Enqueue(new Person2("박수영", 31, Person2.GenderType.Female, "아티스트"));
            _personPriorityQueue.Enqueue(new Person2("김민수", 27, Person2.GenderType.Male, "기획자"));
            _personPriorityQueue.Enqueue(new Person2("홍금보", 23, Person2.GenderType.Male, "프로그래머"));
            _personPriorityQueue.Enqueue(new Person2("타조", 23, Person2.GenderType.Male, "프로그래머"));

            ReloadData();
        }

        public void Add(Person person)
        {
            //_personPriorityQueue.Enqueue(person);
        }

        public void Remove(Person person)
        {
            //_personLinkedList2.Remove(person);
        }

        public void ShowAddPanel()
        {
            //var addPanelObject = Instantiate(addPanel, panelParent);
            //addPanelObject.GetComponent<AddPanelController>().addPanelDelegate = person =>
            //{
            //    Add(person);
            //    ReloadData();
            //};
        }

        public void ReloadData()
        {
            foreach (var cellObject in cellObjectList)
            {
                Destroy(cellObject);
            }

            int index = 0;

            while (_personPriorityQueue.Count > 0)
            {
                Person2 person = _personPriorityQueue.Dequeue();
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
