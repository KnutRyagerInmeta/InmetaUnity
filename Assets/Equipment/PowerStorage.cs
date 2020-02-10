using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStorage : MonoBehaviour {
    
    [SerializeField] public SimpleHealthBar energyBar;
    [SerializeField] float energy;
    [SerializeField] float Energy { get { return energy; } set { energy = value; if (energyBar != null) { energyBar.UpdateBar(energy, maxEnergy); } } }
    [SerializeField] float maxEnergy;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public bool Drain(float amount)
    {
        var canAfford = amount <= energy;
        if (canAfford)
        {
            Energy -= amount;
        }
        return canAfford;
    }

    public void Charge(float amount)
    {
        Energy = Mathf.Min(energy + amount, maxEnergy);
    }
}
