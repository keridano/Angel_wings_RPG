using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control 
{
    public class PlayerController : MonoBehaviour 
    {
        private void Update()
        {
            InteractWithCombat();
            InteractWithMovement();
        }

        private void InteractWithCombat()
        {
            var hits = Physics.RaycastAll(GetMouseRay());
            foreach(var hit in hits)
            {
                var target = hit.transform.GetComponent<CombatTarget>();
                if(target == null) continue;

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
            }
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            if (Physics.Raycast(GetMouseRay(), out var hit))
            {
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }    
}

