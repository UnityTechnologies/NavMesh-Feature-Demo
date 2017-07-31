using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentAreaBaker : MonoBehaviour 
{
	public NavMeshSurface surface;
	public float distanceThreshold = 5f;

	void Awake()
	{
		UpdateNavMesh ();
	}

	void Update()
	{
		if (Vector3.Distance (transform.position, surface.transform.position) >= distanceThreshold)
			UpdateNavMesh ();
	}

	void UpdateNavMesh()
	{
		surface.RemoveData ();
		surface.transform.position = transform.position;
		surface.Bake ();
	}
}
