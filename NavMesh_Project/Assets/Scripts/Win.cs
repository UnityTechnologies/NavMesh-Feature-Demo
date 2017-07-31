using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour 
{
	public int priority = 0;

	Demo1Controller controller;

	void Start()
	{
		controller = FindObjectOfType<Demo1Controller> ();
	}

    private void OnTriggerEnter(Collider other)
    {
		if (other.CompareTag ("Player"))
			controller.WayPointReached ();

		GetComponent<Collider> ().enabled = false;
    }
}
