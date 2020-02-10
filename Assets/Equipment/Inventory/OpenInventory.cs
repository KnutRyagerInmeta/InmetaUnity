using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenInventory : InteractibleObject
{

    protected override void OnEButton()
    {
        chest.ToggleOpen();
    }

    protected override void OnNumberKey(int index)
    {
        Transfer(chest, playerInventory, index);
    }

    protected override void OnShiftNumberKey(int index)
    {
        Transfer(playerInventory, chest, index);
    }
}
