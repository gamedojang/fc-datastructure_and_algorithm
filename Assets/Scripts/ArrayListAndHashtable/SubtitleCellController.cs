using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SubtitleCellController : MonoBehaviour
{
    public TMP_Text Title;
    public TMP_Text SubTitle;

    public int Index { get; set; }
    public delegate void CellDelegate(int index);
    public CellDelegate openDetailPanelDelegate;

    public void SelectCell()
    {
        openDetailPanelDelegate?.Invoke(Index);
    }
}
