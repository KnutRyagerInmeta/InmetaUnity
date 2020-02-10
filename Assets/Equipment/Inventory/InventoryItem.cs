using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour {

    [SerializeField] public string Name;
    [SerializeField] public string titleColor = "00FFFFFF";
    [SerializeField] public string color = "00FFFFFF";

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public string GetDisplayString()
    {
        return "<color=#" + titleColor +  ">" + Name + "</color>: <color=#" + color + ">" + GetSpecificDisplayString() + "</color>";
    }

    public virtual string GetSpecificDisplayString()
    {
        return "VALUE";
    }
}
