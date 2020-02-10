using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitsPoints : MonoBehaviour
{

    private static int[] hitInfoId = new[] { Shader.PropertyToID("_HitPos"), Shader.PropertyToID("_HitPos1"), Shader.PropertyToID("_HitPos2"), Shader.PropertyToID("_HitPos3"), Shader.PropertyToID("_HitPos4") };
    private static int[] hitTimeId = new[] { Shader.PropertyToID("_HitAlpha"), Shader.PropertyToID("_HitAlpha1"), Shader.PropertyToID("_HitAlpha2"), Shader.PropertyToID("_HitAlpha3"), Shader.PropertyToID("_HitAlpha4")};
    private int lastHit = 0;
    private float[] floatArray = new[] { 1f, 1f, 1f, 1f, 1f};
    private Material myMat;

    public float TimeFactor = 0.8f;
    

    private void Start()
    {
        myMat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        if(myMat.GetFloat(hitTimeId[0]) > 0)
        {
            floatArray[0] = myMat.GetFloat(hitTimeId[0]) - Time.deltaTime * TimeFactor;
			if(floatArray[0] < 0)
			{
				floatArray[0] = 0;
			}
            myMat.SetFloat(hitTimeId[0], floatArray[0]); 
        }

        if (myMat.GetFloat(hitTimeId[1]) > 0)
        {
            floatArray[1] = myMat.GetFloat(hitTimeId[1]) - Time.deltaTime * TimeFactor;
			if(floatArray[1] < 0)
			{
				floatArray[1] = 0;
			}
            myMat.SetFloat(hitTimeId[1], floatArray[1]);
        }

        if (myMat.GetFloat(hitTimeId[2]) > 0)
        {
            floatArray[2] = myMat.GetFloat(hitTimeId[2]) - Time.deltaTime * TimeFactor;
			if(floatArray[2] < 0)
			{
				floatArray[2] = 0;
			}
            myMat.SetFloat(hitTimeId[2], floatArray[2]);
        }

        if (myMat.GetFloat(hitTimeId[3]) > 0)
        {
            floatArray[3] = myMat.GetFloat(hitTimeId[3]) - Time.deltaTime * TimeFactor;
			if(floatArray[3] < 0)
			{
				floatArray[3] = 0;
			}
            myMat.SetFloat(hitTimeId[3], floatArray[3]);
        }

        if (myMat.GetFloat(hitTimeId[4]) > 0)
        {
            floatArray[4] = myMat.GetFloat(hitTimeId[4]) - Time.deltaTime * TimeFactor;
			if(floatArray[4] < 0)
			{
				floatArray[4] = 0;
			}
            myMat.SetFloat(hitTimeId[4], floatArray[4]);
        }

    }

    public void OnHit(Vector3 point)
    {
        myMat.SetVector(hitInfoId[lastHit], point);
        myMat.SetFloat(hitTimeId[lastHit], 1f);
        lastHit++;
        if (lastHit >= hitInfoId.Length)
        {
            lastHit = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        OnHit(collision.contacts[0].normal);
    }
}