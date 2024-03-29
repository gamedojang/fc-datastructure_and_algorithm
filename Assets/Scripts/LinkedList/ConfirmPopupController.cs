using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConfirmPopupController : MonoBehaviour
{
    [SerializeField] private TMP_Text messageText;

    public delegate void ConfirmPopupDelegate(bool isConfirm);
    public ConfirmPopupDelegate confirmPopupDelegate;

    public void SetMessage(string message)
    {
        messageText.text = message;
    }
    
    public void OK()
    {
        confirmPopupDelegate?.Invoke(true);
        Destroy(gameObject);
    }

    public void Cancel()
    {
        confirmPopupDelegate?.Invoke(false);
        Destroy(gameObject);
    }
}
