using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class InventoryContent : MonoBehaviour
{

    [SerializeField] int Capacity = 10;
    [SerializeField] InventoryItem[] Items;

    // Use this for initialization
    void Start()
    {
        if (Items == null || Items.Length == 0)
        {
            Items = new InventoryItem[Capacity];
        }
        else
        {
            Capacity = Items.Length;
        }

    }

    public bool Contains(InventoryItem item)
    {
        return Items.Any(x => x != null && x.Equals(item));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetDisplayString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < Capacity; i++)
        {
            InventoryItem item = Items[i];
            if (item != null)
            {
                sb.Append("(" + (i+1) + "): " +item.GetDisplayString()).Append("\n");
            }
        }
        return sb.ToString();
    }

    public int RemainingSpace()
    {
        for (int i = 0; i < Capacity; i++)
        {
            InventoryItem item = Items[i];
            if (item == null)
            {
                return Capacity - i;
            }
        }
        return 0;
    }

    public InventoryItem Take(int index)
    {
        if (index < 0 || index >= Capacity)
        {

        }
        var item = Items[index];
        if (item == null)
        {
            return null;
        }
        for (int i = index; i < Capacity; i++)
        {
            InventoryItem switchValue = null;
            if (i + 1 < Capacity)
            {
                switchValue = Items[i + 1];
            }
            Items[i] = switchValue;
        }
        return item;
    }

    internal InventoryItem Peek(int index)
    {
        return Items[index];
    }

    public bool Add(InventoryItem item)
    {
        for (int i = 0; i < Capacity; i++)
        {
            if (Items[i] == null)
            {
                Items[i] = item;
                return true;
            }
        }
        return false;
    }
}
