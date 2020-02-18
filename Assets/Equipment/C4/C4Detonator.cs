using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4Detonator : Gun {

    public List<C4> c4s;


	// Use this for initialization
	void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();
        if (Input.GetMouseButtonDown(1))
        {
            Detonate();
        }
	}

    public void Detonate()
    {
        c4s.ForEach(c4 => c4.Explode());
        c4s.Clear();
    }

    protected override void OnFire(Projectile c4)
    {
        c4s.Add((C4)c4);
    }
}
