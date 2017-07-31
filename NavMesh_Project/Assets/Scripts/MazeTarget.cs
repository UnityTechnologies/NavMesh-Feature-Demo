using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeTarget : MonoBehaviour 
{
	bool hasCollided = false;

	void OnTriggerEnter(Collider other)
	{
		if (hasCollided)
			return;
		
		if (other.CompareTag ("Player")) 
		{
            Demo2Controller.Bake ();
			hasCollided = true;

		}
	}

	void OnTriggerExit(Collider other)
	{
		if (!hasCollided)
			return;
		
		if (other.CompareTag ("Player")) 
		{
			hasCollided = false;
		}
	}
}
