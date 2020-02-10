using UnityEngine;

public class Equipment : MonoBehaviour {

    public virtual void Equip()
    {
        gameObject.SetActive(true);
    }

    public virtual void DeEquip()
    {
        gameObject.SetActive(false);
    }
}
