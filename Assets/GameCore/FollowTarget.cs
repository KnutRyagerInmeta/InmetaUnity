using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;
    public float maxOffsetX = 1;
    public float maxOffsetY = 2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            GetCoordinate(transform.position.x, target.position.x, maxOffsetX),
            GetCoordinate(transform.position.y, target.position.y, maxOffsetY),
            transform.position.z);
    }

    private float GetCoordinate(float selfPosition, float targetPosition, float maxOffset) => Mathf.Clamp(selfPosition, targetPosition - maxOffset, targetPosition + maxOffset);
}
