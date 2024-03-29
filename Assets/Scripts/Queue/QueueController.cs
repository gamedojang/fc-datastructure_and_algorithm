using System;
using System.Collections.Generic;
using UnityEngine;

public class QueueController : MonoBehaviour
{
    [SerializeField] private GameObject basicCellPrefab;
    [SerializeField] private Transform parent;

    private Queue<BasicCellController> dateTimeQueue = new Queue<BasicCellController>();
    float timeInterval = 0f;

    private void Update()
    {
        timeInterval += Time.deltaTime;

        if (timeInterval > 2f)
        {
            if (dateTimeQueue.Count > 0 ) 
            {
                BasicCellController cellController = dateTimeQueue.Dequeue();
                Destroy(cellController.gameObject);
            }
            timeInterval = 0f;
        }
    }

    public void Add()
    {
        GameObject cellObject = Instantiate(basicCellPrefab, parent);
        BasicCellController basicCellController = cellObject.GetComponent<BasicCellController>();

        DateTime now = DateTime.Now;
        basicCellController.Text.text = now.Hour.ToString() + ":" 
            + now.Minute.ToString() + ":" + now.Second.ToString() + ":" + now.Millisecond.ToString();

        dateTimeQueue.Enqueue(basicCellController);
    }
}
