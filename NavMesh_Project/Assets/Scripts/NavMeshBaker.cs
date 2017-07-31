using UnityEngine;
using UnityEngine.AI;

[DefaultExecutionOrder(-100)]
public class NavMeshBaker : MonoBehaviour 
{
	NavMeshSurface surface;


	void Start()
	{
		surface = GetComponent<NavMeshSurface> ();
		surface.Bake ();
	}

	void Update()
	{
		if (Input.GetButtonDown ("Fire1"))
			surface.Bake ();
		else if (Input.GetButtonDown ("Fire2"))
			surface.RemoveData ();
	}
}
