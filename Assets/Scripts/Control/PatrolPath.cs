using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        private const float waypointGizmoRadius = .3f;

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }

        public int getNextIndex(int i)
        {
            if(i == transform.childCount - 1) return 0;
            return i + 1;
        }

        private void OnDrawGizmos() 
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmoRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(getNextIndex(i)));
            }
        }
    }
}
