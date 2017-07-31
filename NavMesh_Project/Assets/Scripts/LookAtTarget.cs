using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour {

    public Transform target;

    private void LateUpdate()
    {
        transform.LookAt(target.position);
    }
}
