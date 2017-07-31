using UnityEngine.AI;
using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform target;

	NavMeshPath path;
    Animator anim;
    NavMeshAgent agent;
    bool isChasing;


    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
		path = new NavMeshPath();
    }

	public void BeginChasing()
	{
		StartCoroutine (Chase());
	}

	IEnumerator Chase()
	{
		isChasing = true;
		anim.SetTrigger("Chase");

		while (isChasing && agent.enabled) 
		{
			agent.CalculatePath (target.position, path);
			if (path.status != NavMeshPathStatus.PathPartial)
				agent.SetPath (path);
			else
				isChasing = false;

			yield return null;
		}

		anim.SetTrigger ("Stop");
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerExample>().Lose();
            anim.SetTrigger("Stop");
			agent.enabled = false;
        }
    }
}
