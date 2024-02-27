using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueueController : MonoBehaviour
{
    [SerializeField] private Transform scrollViewTransform;
    [SerializeField] private GameObject basicCellPrefab;
    
    private Queue<BasicCellController> dateTimeQueue = new Queue<BasicCellController>();
    float timeInterval = 0f;
    
    private void Update()
    {
        timeInterval += Time.deltaTime;
        if (timeInterval > 2)
        {
            if (dateTimeQueue.Count > 0)
            {
                var dateTimeCell = dateTimeQueue.Dequeue();
                Destroy(dateTimeCell.gameObject);
            }
            timeInterval = 0f;
        }
    }

    public void AddDate()
    {
        var basicCellController = Instantiate(basicCellPrefab, scrollViewTransform).GetComponent<BasicCellController>();

        var now = DateTime.Now;
        basicCellController.Text.text = now.Hour + ":" + now.Minute + ":" + now.Second + ":" + now.Millisecond;
        
        dateTimeQueue.Enqueue(basicCellController);
    }
}
