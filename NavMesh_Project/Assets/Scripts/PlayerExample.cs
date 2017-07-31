using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerExample : MonoBehaviour 
{
	ParticleSystem[] confetti;
	NavMeshAgent agent;
    Animator anim;

    void Start()
    {
		agent = GetComponent<NavMeshAgent> ();
        anim = GetComponent<Animator>();

		confetti = FindObjectsOfType<ParticleSystem> ();
    }

	public void BeginRunning(Vector3 position)
	{
		anim.SetFloat ("Speed", 1f);
		agent.SetDestination (position);
	}

    public void Win()
    {
		DisableScene ();
        anim.SetTrigger("Win");
    }

    public void Lose()
    {
		DisableScene ();
        anim.SetTrigger("Lose");
    }

	void DisableScene()
	{
		GetComponentInChildren<Camera> ().enabled = true;
		agent.isStopped = true;
	}

	public void PulseConfetti()
	{
		for (int i = 0; i < confetti.Length; i++)
			confetti [i].Play (true);
	}
}
