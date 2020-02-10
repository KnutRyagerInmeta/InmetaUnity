using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowEquipment : Equipment {

    [SerializeField] public Transform target;
    [SerializeField] protected Vector3 offset;

    void Start () {
		
	}
	
	protected virtual void Update () {
        SetPositionToTarget();
    }

    public void SetPositionToTarget()
    {
        transform.position = target.position + offset;
    }
}
