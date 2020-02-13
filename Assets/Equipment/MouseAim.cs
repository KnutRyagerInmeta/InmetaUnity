using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAim : MonoBehaviour
{
    [SerializeField] Transform aimer; // Aiming entity (usually main character)
    [SerializeField] float aimSpeed = 720; // Degress per second
    [SerializeField] float minX = -30;
    [SerializeField] float maxX = 45;
    [SerializeField] bool useHorizonOnly = false;
    private GameObject spawnPt; // holds the spawn point object

	void Start()
    {

    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(1))
        //{ // only do anything when the button is pressed:

        var mosueWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 dir = Input.mousePosition - aimer.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //aimer.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        var aimPoint = Vector3.zero;
        if (!useHorizonOnly && Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(aimer.position, hit.point);
            // cache oneSpawn object in spawnPt, if not cached yet
            //if (!spawnPt) spawPt = GameObject.Find("oneSpawn");
            //var projectile = Instantiate(bullet, spawnPt.transform.position, Quaternion.identity);
            //// turn the projectile to hit.point
            //projectile.transform.LookAt(hit.point);
            //// accelerate it
            //projectile.rigidbody.velocity = projectile.transform.forward * 10;
            aimPoint = hit.point;
        }
        else
        {
            aimPoint = mosueWorldPos + ray.direction.normalized * 1000;
        }


        var targetRotation = Quaternion.LookRotation(aimPoint - aimer.position, Vector3.up);
        var rotationQuaternion = Quaternion.RotateTowards(aimer.rotation, targetRotation, aimSpeed * Time.deltaTime);
        var rotation = rotationQuaternion.eulerAngles;
        var rotationX = rotation.x;
        if (rotationX > 180) rotationX -= 360;
        rotation.x = Mathf.Clamp(rotationX, minX, maxX);
        aimer.rotation = Quaternion.Euler(rotation);
        //}
    }
}
