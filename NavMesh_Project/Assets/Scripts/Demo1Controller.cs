using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo1Controller : MonoBehaviour 
{
	[SerializeField] PlayerExample player;
	[SerializeField] Enemy enemy;
	[SerializeField] float demoStartDelay = 1f;

	Win[] goalZones;
	int currentPriority = 0;
	int currentIndex = -1;

	void Start () 
	{
		goalZones = FindObjectsOfType<Win> ();

		Invoke ("BeginDemo", demoStartDelay);
	}
	
	void BeginDemo()
	{
		currentIndex = FindNextTarget (currentPriority);

		player.BeginRunning (goalZones[currentIndex].transform.position);
		enemy.BeginChasing ();
	}

	public void WayPointReached()
	{
		currentIndex = FindNextTarget (++currentPriority);

		if (currentIndex == -1)
			player.Win ();
		else
			player.BeginRunning (goalZones [currentIndex].transform.position);
	}

	int FindNextTarget(int priority)
	{
		for (int i = 0; i < goalZones.Length; i++)
			if (goalZones [i].priority == priority)
				return i;

		return -1;
	}
}
