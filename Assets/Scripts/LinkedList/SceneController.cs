using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SceneController : MonoBehaviour
{
    public abstract void OpenConfirmPopup(string message, ConfirmPopupController.ConfirmPopupDelegate confirmPopupDelegate);
}
