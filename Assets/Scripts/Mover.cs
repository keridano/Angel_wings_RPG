using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour 
{
    Ray lastRay;

    void Update () 
    {
        if (Input.GetMouseButtonDown (0)) 
        {
            MoveToCursor ();
        }
    }

    private void MoveToCursor () 
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(ray.origin, ray.direction * 100);
        if(Physics.Raycast (ray, out var hitInfo))
        {
            GetComponent<NavMeshAgent> ().destination = hitInfo.point;
        }
    }
}