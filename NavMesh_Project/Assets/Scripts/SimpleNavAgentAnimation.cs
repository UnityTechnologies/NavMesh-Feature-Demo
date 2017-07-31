using UnityEngine;
using UnityEngine.AI;

public class SimpleNavAgentAnimation : MonoBehaviour 
{
	NavMeshAgent agent;		//A reference to the player's navmesh agent component
	NavMeshHit navHitInfo;	//Where on a navmesh the player is looking
	Animator anim;			//A reference to the player's animator component


	void Start ()
	{
		//Get references to the local navmesh agent and animator
		agent = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
	}

	void Update ()
	{
		//Record the desired speed of the navmesh agent
		float speed = agent.desiredVelocity.magnitude;

		//Tell the animator how fast the navmesh agent is going
		anim.SetFloat("Speed", speed);
	}
}