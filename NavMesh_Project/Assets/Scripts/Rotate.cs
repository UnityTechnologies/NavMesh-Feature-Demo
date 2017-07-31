using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float xSpeed = 10f;
    public float ySpeed = 10f;
    public float zSpeed = 10f;


    void Update ()
    {
		if (Input.GetButton ("Fire1"))
		{
			float t = Time.deltaTime;
			transform.Rotate (xSpeed * t, ySpeed * t, zSpeed * t);	
		}
	}
}
