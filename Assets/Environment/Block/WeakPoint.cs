using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoint : MonoBehaviour {

    [SerializeField] WeakObject weakObject;

    int triggerFrame = -1;

    private void Start()
    {
        if(weakObject == null)
        {
            weakObject = GetComponentInParent<WeakObject>();
        }
    }

    public int GetTriggerTime()
    {
        return triggerFrame;
    }

    public void Trigger()
    {
        triggerFrame = Time.frameCount;
        weakObject.TriggerWeakpoint();
    }
}
