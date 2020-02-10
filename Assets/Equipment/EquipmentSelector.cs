using UnityEngine;

public class EquipmentSelector : MonoBehaviour
{
    [SerializeField] Equipment[] equipments;
    private int currentIndex;

    void Start()
    {

    }

    void Update()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll > 0f) Switch(1);
        else if (scroll < 0f) Switch(-1);
        for(int i = 0; i < 9; i++) if (Input.GetKeyDown(KeyCode.Alpha0 + i) && equipments.Length > i) SwitchTo(i);
    }

    public Equipment CurrentEquipment() => equipments[currentIndex];

    public void Switch(int diff)
    {
        CurrentEquipment().DeEquip();
        currentIndex += diff;
        if (currentIndex < 0) currentIndex += equipments.Length;
        if (currentIndex >= equipments.Length) currentIndex -= equipments.Length;
        CurrentEquipment().Equip();
    }

    public void SwitchTo(int index) => Switch(index - currentIndex);
}
