using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4Detonator : Gun {

    [SerializeField] List<C4> c4s;


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
        for(int i = 0; i < c4s.Count; i++)
        {
            c4s[i].Explode();
        }
        c4s.Clear();
    }

    protected override void OnFire(Bullet c4)
    {
        c4s.Add((C4)c4);
    }
}
