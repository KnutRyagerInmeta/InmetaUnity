using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    Level level;
    Transform player;
    float radius = 1;
    float radiusSqr;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        level = GetComponentInParent<Level>();
        radiusSqr = radius * radius;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if ((transform.position - player.position).sqrMagnitude <= radiusSqr)
        {
            Reach();
        }
	}

    public void Reach()
    {

    }
}
