using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LinkedList
{
    public class DetailPanelController : MonoBehaviour
    {
        [SerializeField] private TMP_InputField nameInputField;
        [SerializeField] private TMP_InputField ageInputField;
        [SerializeField] private Toggle femaleToggle;
        [SerializeField] private Toggle maleToggle;
        [SerializeField] private TMP_InputField jobInputField;
    
        public delegate void DetailPanelDelegate(Person person);
        public DetailPanelDelegate deletePersonDelegate;

        private Person _person;
        private SceneController _sceneController;

        public void SetData(Person person, SceneController sceneController)
        {
            _person = person;
            _sceneController = sceneController;
        
            nameInputField.text = _person.Name;
            ageInputField.text = _person.Age.ToString();
            if (_person.Gender == Person.GenderType.Female)
                femaleToggle.isOn = true;
            else
                maleToggle.isOn = true;
            jobInputField.text = _person.Job;
        }
    
        public void Delete()
        {
            _sceneController?.OpenConfirmPopup("데이터를 삭제하겠습니까?", isConfirm =>
            {
                if (isConfirm)
                {
                    deletePersonDelegate?.Invoke(_person);
                    Destroy(gameObject);
                }
            });
        }

        public void Cancel()
        {
            Destroy(gameObject);
        }
    }
}
