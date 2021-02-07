using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;

namespace RPG.Control 
{
    public class PlayerController : MonoBehaviour 
    {
        private Health _health;
        private Fighter _fighter;
        private Mover _mover;

        void Start() 
        {
            _health = GetComponent<Health>();
            _fighter = GetComponent<Fighter>();
            _mover = GetComponent<Mover>();
        }
        void Update()
        {
            if(_health.IsDead()) return;
            if(InteractWithCombat()) return;
            if(InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            var hits = Physics.RaycastAll(GetMouseRay());
            foreach(var hit in hits)
            {
                var target = hit.transform.GetComponent<CombatTarget>();
                if(target == null) continue;
                if(!_fighter.CanAttack(target.gameObject)) continue;

                if (Input.GetMouseButton(0))
                    _fighter.Attack(target.gameObject);

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

