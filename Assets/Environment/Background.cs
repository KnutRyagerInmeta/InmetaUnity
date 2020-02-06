using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Transform Camera;
    public float MoveFactorX = 0.95f;
    public float MoveFactorY = 0.95f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(
            Camera.position.x * MoveFactorX,
            Camera.position.y * MoveFactorY,
            transform.position.z
            );
    }
}
