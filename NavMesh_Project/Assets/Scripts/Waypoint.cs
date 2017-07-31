using UnityEngine.AI;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint nextTarget;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            other.GetComponent<WaypointFollower>().UpdateTarget(nextTarget);
    }
}
