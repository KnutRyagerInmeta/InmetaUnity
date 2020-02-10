using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] GameObject[] targets;
    [SerializeField] SwitchMode switchMode = SwitchMode.Move;
    [SerializeField] bool clicked;
    [SerializeField] Vector3 move;
    private float lastClickTime = -100;
    private float cooldown = 0.1f;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void Click()
    {
        var time = Time.time;
        if (lastClickTime + cooldown > time)
        {
            return;
        }
        lastClickTime = time;
        foreach (var target in targets)
        {
            Act(target);
        }
        clicked = !clicked;
    }

    public void Act(GameObject target)
    {
        switch (switchMode)
        {
            case SwitchMode.Move:
                var movers = target.GetComponentsInChildren<Mover>();
                foreach (var mover in movers)
                {
                    if (mover != null)
                    {
                        mover.Move(clicked ? -move : move);
                    }
                }
                break;
            case SwitchMode.Activate:
                target.SetActive(true);
                break;
            case SwitchMode.Deactivate:
                target.SetActive(true);
                break;
            case SwitchMode.ToggleActive:
                target.SetActive(!target.activeInHierarchy);
                break;
            case SwitchMode.Explode:
                throw new NotImplementedException();
                //break;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Click();
    }

    enum SwitchMode
    {
        Move, Activate, Deactivate, ToggleActive, Explode
    }

}
