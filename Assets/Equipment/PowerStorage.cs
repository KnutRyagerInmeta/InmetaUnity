using UnityEngine;

public class PowerStorage : MonoBehaviour {
    
    [SerializeField] public SimpleHealthBar energyBar;
    [SerializeField] float energy;
    public float Energy { get { return energy; } set { energy = value; if (energyBar != null) { energyBar.UpdateBar(energy, maxEnergy); } } }
    [SerializeField] float maxEnergy;

    void Start () {
		
	}
	
	void Update () {

    }

    public bool Drain(float amount)
    {
        var canAfford = amount <= Energy;
        if (canAfford) Energy -= amount;
        return canAfford;
    }

    public void Charge(float amount)
    {
        Energy = Mathf.Min(energy + amount, maxEnergy);
    }
}
