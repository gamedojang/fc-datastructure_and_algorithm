using System.Collections.Generic;
using UnityEngine;

namespace LinkedList
{
    public class LinkedListController : MonoBehaviour
    {
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject addPanelPrefab;
        [SerializeField] private Transform panelParent;
        [SerializeField] private GameObject detailPanelPrefab;

        private List<GameObject> cellObjectList = new List<GameObject>();

        //private LinkedList<Person> personLinkedList = new LinkedList<Person>(new Person[]
        //{
        //    new Person("홍길동", 23, Person.GenderType.Male, "프로그래머"),
        //    new Person("김민기", 23, Person.GenderType.Male, "아티스트"),
        //    new Person("최민희", 23, Person.GenderType.Female, "아티스트"),
        //    new Person("박수영", 23, Person.GenderType.Female, "프로그래머"),
        //    new Person("김민수", 23, Person.GenderType.Male, "기획자")
        //});

        private GYLinkedList<Person> personLinkedList = new GYLinkedList<Person>(new Person[]
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
        
        private void ReloadData()
        {
            foreach (GameObject cellObject in cellObjectList)
            {
                Destroy(cellObject);
            }

            int index = 0;
            foreach (Person person in personLinkedList)
            {
                GameObject cell = Instantiate(cellPrefab, parent);
                cellObjectList.Add(cell);

                SubtitleCellController subtitleCellController = cell.GetComponent<SubtitleCellController>();

                subtitleCellController.Title.text = person.Name;
                subtitleCellController.SubTitle.text = person.Job;

                subtitleCellController.Index = index++;

                subtitleCellController.openDetailPanelDelegate = i =>
                {
                    GameObject detailPanelObject = Instantiate(detailPanelPrefab, panelParent);

                    DetailPanelController detailPanelController = detailPanelObject.GetComponent<DetailPanelController>();

                    detailPanelController.SetData(person);
                    detailPanelController.detailPanelDelegate = person =>
                    {
                        personLinkedList.Remove(person);
                        ReloadData();
                    };
                };
            }
        }

        public void OpenAddPanel()
        {
            GameObject addPanelObject = Instantiate(addPanelPrefab, panelParent);

            addPanelObject.GetComponent<AddPanelController>().addPanelControllerDelegate = person =>
            {
                // 1. person를 personLinkedList에 추가
                // 2. personLinkedList 값을 Cell로 만들어서 화면에 표시
                personLinkedList.AddLast(person);
                ReloadData();
            };
        }
    }
}
