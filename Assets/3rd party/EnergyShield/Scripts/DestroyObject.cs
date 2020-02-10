using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour {

    public float TimeDestroy = 3f;

	void Update () {
        Destroy(gameObject, TimeDestroy);
	}
}