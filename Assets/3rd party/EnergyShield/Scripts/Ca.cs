using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ca : MonoBehaviour {

    public GameObject pref;

    void Update () {
        if (Input.GetMouseButtonDown(0))
        {

            var position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
            position = Camera.main.ScreenToWorldPoint(position);
            var go = Instantiate(pref, transform.position, Quaternion.identity) as GameObject;
            go.transform.LookAt(position);
            go.GetComponent<Rigidbody>().AddForce(go.transform.forward * 1000);
        }
    }
}
