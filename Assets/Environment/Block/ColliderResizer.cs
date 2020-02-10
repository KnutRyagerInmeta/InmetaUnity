using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ColliderResizer : MonoBehaviour {

    private BoxCollider collider;
    private Transform bodyTransform;

    private void Start()
    {
        collider = GetComponentInChildren<BoxCollider>();
        bodyTransform = GetComponentInChildren<Block>().gameObject.transform;
        this.UpdateTiling();
    }

    // Update is called once per frame
    void Update()
    {
    }

    [ContextMenu("UpdateCollider")]
    void UpdateTiling()
    {
        //collider.center = bodyTransform.position;
        collider.size = bodyTransform.localScale;
    }

}
