using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HashtableController : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text ageText;
    [SerializeField] private TMP_Text genderText;
    [SerializeField] private TMP_Text jobText;

    private Hashtable userInfo = new Hashtable();
    
    private void Start()
    {
        userInfo.Add("name", "홍길동");
        userInfo.Add("age", "23");
        userInfo.Add("gender", "남자");
        userInfo.Add("job", "프로그래머");

        nameText.text = userInfo["name"].ToString();
        ageText.text = userInfo["age"].ToString();
        genderText.text = userInfo["gender"].ToString();
        jobText.text = userInfo["job"].ToString();
    }
}
