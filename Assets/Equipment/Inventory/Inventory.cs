using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    [SerializeField] Text text;
    [SerializeField] InventoryContent content;
    [SerializeField] bool isOpen = false;
    [SerializeField] public string Name = "Nameless chest";

    public bool Contains(InventoryItem item)
    {
        return content.Contains(item);
    }

    // Use this for initialization
    void Start()
    {
        if (!isOpen)
        {
            Close();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpen)
        {
            UpdateText();
        }
    }

    public void Open()
    {
        isOpen = true;
        UpdateText();
    }

    public void ToggleOpen()
    {
        if (isOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }

    public void Close()
    {
        isOpen = false;
        text.text = Name.Contains("Player") ? "" : "(E): Open";
    }

    public bool IsOpen()
    {
        return isOpen;
    }

    public InventoryItem Take(Inventory actor, int index)
    {
        var item = content.Take(index);
        UpdateText();
        return item;
    }

    public bool Add(Inventory actor, InventoryItem item)
    {
        var success = content.Add(item);
        UpdateText();
        return success;
    }

    public InventoryItem Peek(int index)
    {
        return content.Peek(index);
    }

    public int RemainingSpace()
    {
        return content.RemainingSpace();
    }

    public void UpdateText()
    {
        text.text = ((!Name.Contains("Player")) ? (Name + "\n") : "") + content.GetDisplayString();
    }
}
