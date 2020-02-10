using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InteractibleObject : MonoBehaviour
{


    [SerializeField] public Inventory chest;
    [SerializeField] public Inventory playerInventory;
    public GameObject player;
    [SerializeField] public float radius = 2f;

    // Use this for initialization
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<Inventory>();
    }


    // Update is called once per frame
    public virtual void Update()
    {

        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance < radius)
        {
            if (Input.GetKeyDown("e"))
            {
                //print("found chest");
                OnEButton();
            }
            if (chest != null && chest.IsOpen())
            {
                for (int i = 0; i < 10; i++)
                {
                    DoNumberKey(i);
                }
            }
        }
    }

    protected abstract void OnEButton();
    protected abstract void OnNumberKey(int index);
    protected abstract void OnShiftNumberKey(int index);

    void DoNumberKey(int index)
    {
        if (Input.GetKeyDown(index.ToString()))
        {
            // Use 1-index with 0 as last index
            index--;
            if (index < 0)
            {
                index += 10;
            }
            bool isShiftKeyDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            if (isShiftKeyDown)
            {
                // Add
                //Debug.Log("Add: " + index);
                OnShiftNumberKey(index);
            }
            else
            {
                // Take
                //Debug.Log("Take: " + index);
                OnNumberKey(index);
            }
        }
    }

    public bool Transfer(Inventory from, Inventory to, int takeIndex)
    {
        var isSpace = to.RemainingSpace() > 0;
        if (!isSpace)
        {
            return false;
        }
        var itemForTranfer = from.Take(to, takeIndex);
        if (itemForTranfer != null)
        {
            to.Add(from, itemForTranfer);
            return true;
        }
        return false;
    }

}
