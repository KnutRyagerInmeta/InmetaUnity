using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakObject : MonoBehaviour
{

    [SerializeField] WeakPoint[] weakPoints;

    private void Start()
    {
        if (weakPoints == null || weakPoints.Length == 0)
        {
            weakPoints = GetComponentsInChildren<WeakPoint>();
        }
    }

    public void TriggerWeakpoint()
    {
        //Debug.Log("Trigger times: " + weakPoints[0].GetTriggerTime() + "," + weakPoints[1].GetTriggerTime());
        int triggerTime = weakPoints[0].GetTriggerTime();
        for (var i = 1; i < weakPoints.Length; i++)
        {
            if (weakPoints[i].GetTriggerTime() != triggerTime)
            {
                return;
            }
        }
        Destroy(gameObject);
    }
}
