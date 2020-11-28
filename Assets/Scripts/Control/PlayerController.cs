using RPG.Combat;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control 
{
    public class PlayerController : MonoBehaviour 
    {
        private Fighter _fighter;
        private Mover _mover;

        void Start() 
        {
            _fighter = GetComponent<Fighter>();
            _mover = GetComponent<Mover>();
        }
        void Update()
        {
            if(InteractWithCombat()) return;
            if(InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            var hits = Physics.RaycastAll(GetMouseRay());
            foreach(var hit in hits)
            {
                var target = hit.transform.GetComponent<CombatTarget>();
                if(!_fighter.CanAttack(target)) continue;

                if (Input.GetMouseButtonDown(0))
                    _fighter.Attack(target);

                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            if (Physics.Raycast(GetMouseRay(), out var hit))
            {
                if (Input.GetMouseButton(0))
                    _mover.StartMoveAction(hit.point);

                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }    
}

