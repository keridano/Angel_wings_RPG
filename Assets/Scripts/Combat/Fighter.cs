using RPG.Movement;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        private Transform _target;
        private Mover _mover;
        private ActionScheduler _actionScheduler;

        void Start() 
        {
            _mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        void Update() 
        {
            if(_target == null) return;
            
            if(!IsInRange())
                _mover.MoveTo(_target.position);
            else
                _mover.Cancel();
        }

        public void Attack(CombatTarget combatTarget)
        {
            _actionScheduler.StartAction(this);
            _target = combatTarget.transform;
        }
        
        public void Cancel()
        {
            _target = null;
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, _target.position) < weaponRange;
        }
    }
}