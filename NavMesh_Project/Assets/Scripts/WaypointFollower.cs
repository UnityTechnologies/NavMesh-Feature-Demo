using UnityEngine.AI;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    NavMeshAgent agent;
    public Waypoint target;

	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();	
	}

	void Update ()
    {
		if (Input.GetButtonDown ("Fire1"))
			agent.enabled = false;
		else if (Input.GetButtonUp ("Fire1"))
		{	
			agent.enabled = true;
			agent.SetDestination (target.transform.position);	
		}
	}

    public void UpdateTarget(Waypoint newTarget)
    {
        target = newTarget;
		if(agent.enabled)
			agent.SetDestination(target.transform.position);
    }
}
