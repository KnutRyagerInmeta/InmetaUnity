using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

	[SerializeField] float impactToDestroy = 1e10f;
	[SerializeField] float explosiveImpactToDestroy = 1e10f;

    public void ExplodeOn(float power)
    {
        //Debug.Log("IMPACT: " + power + "/" + explosiveImpactToDestroy);
        if(power >= explosiveImpactToDestroy)
        {
            Destroy(gameObject);
        }
    }

}
