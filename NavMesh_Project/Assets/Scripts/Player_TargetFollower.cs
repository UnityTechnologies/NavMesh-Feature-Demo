using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player_TargetFollower : MonoBehaviour 
{
	[SerializeField] Transform target;
	[SerializeField] float runDelay = 1f;

	NavMeshAgent agent;
	Animator anim;

	void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
		agent.enabled = false;

		anim = GetComponent<Animator> ();
	}

	void Update()
	{
		float speed = agent.desiredVelocity.magnitude;

		//Tell the animator how fast the navmesh agent is going
		anim.SetFloat("Speed", speed);

		//If the player if moving...
		if (speed > 0f) 
		{
			//...calculate the angle the player should be facing...
			Quaternion targetRotation = Quaternion.LookRotation(agent.desiredVelocity);
			//...and rotate over time to face that direction
			transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 15 * Time.deltaTime);
		}
	}

	public void BeginRunning()
	{
		StartCoroutine (Run ());
	}

	IEnumerator Run()
	{
		agent.enabled = true;

		while (true) 
		{
			yield return new WaitForSeconds (runDelay);

			agent.SetDestination (target.position);
		}
	}
}
