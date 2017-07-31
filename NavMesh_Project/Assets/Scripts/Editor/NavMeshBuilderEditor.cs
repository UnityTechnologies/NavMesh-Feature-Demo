using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class NavMeshBuilderEditor : Editor 
{

	[MenuItem("Edit/Build NavMesh %1")]
	public static void BuildNavMesh()
	{
        Demo2Controller.Bake();
	}
}
