using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LandscapeBuilder : MonoBehaviour 
{
	public GameObject ground;
	public GameObject tree;
	public int numTrees = 12;
	public int numGround = 4;
	public float groundWidth = 10f;
	public float groundLength = 10f;

	NavMeshSurface surface;


	void Start () 
	{
		surface = GetComponent<NavMeshSurface> ();
	
		GenerateGeo ();
	}

	void Update()
	{
		if(Input.GetButtonDown("Fire1"))
			surface.Bake ();
	}

	void GenerateGeo()
	{
		int treePerGround = numTrees / numGround;
		float wRange = groundWidth / 2f;
		float lRange = groundLength / 2f;

		for (int g = 0; g < numGround; g++)
		{
			GameObject nGround = Instantiate (ground, transform.position + new Vector3 (0f, 0f, g * groundLength), Quaternion.identity) as GameObject;
			nGround.transform.parent = transform;

			for (int t = 0; t < treePerGround; t++)
			{
				Vector3 pos = nGround.transform.position;
				pos.x += Random.Range (-wRange, wRange);
				pos.z += Random.Range (-lRange, lRange);

				GameObject nTree = Instantiate (tree, pos, Quaternion.identity) as GameObject;
				nTree.transform.parent = nGround.transform;
			}
		}
	}
}
