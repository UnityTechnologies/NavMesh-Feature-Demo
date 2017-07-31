using UnityEngine;
using System.Collections;
using UnityEditor;

public class mazeTools : MonoBehaviour
{

}

[CustomEditor(typeof(mazeTools))]
public class mazeToolsWindow : Editor
{
	public override void OnInspectorGUI()
	{
		mazeTools tool = (mazeTools)target;

		GUILayout.BeginVertical();
			if (GUILayout.Button("(re) generate"))
		{
			tool.gameObject.GetComponent<maze_generation>().clean();
			tool.gameObject.GetComponent<maze_generation>().generate();
		}
		if (GUILayout.Button("erase maze"))
			tool.gameObject.GetComponent<maze_generation>().clean();
		GUILayout.TextArea("Warning: Using these buttons delete all the children present in the maze object", GUILayout.Height(35));
		GUILayout.EndVertical();
	}
}
