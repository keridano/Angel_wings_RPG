using RPG.Movement;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;

        private float _timeSinceLastAttack = 0;
        private Health _target;
        private Mover _mover;
        private ActionScheduler _actionScheduler;
        private Animator _animator;

        public void Attack(CombatTarget combatTarget)
        {
            _actionScheduler.StartAction(this);
            _target = combatTarget.GetComponent<Health>();
        }
        
        public void Cancel()
        {
            StopAttack();
            _target = null;
        }


        public bool CanAttack(CombatTarget combatTarget)
        {
            if(combatTarget == null) return false;

            var targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        void Start() 
        {
            _mover = GetComponent<Mover>();
            _actionScheduler = GetComponent<ActionScheduler>();
            _animator = GetComponent<Animator>();
        }

        void Update() 
        {
            _timeSinceLastAttack += Time.deltaTime;

            if(_target == null) return;
            if(_target.IsDead()) return;

            if (!IsInRange())
            {
                _mover.MoveTo(_target.transform.position);
            }
            else
            {
                _mover.Cancel();
                AttackBehaviour();
            }
        }

        //Animation Event
        private void Hit()
        {
            if(_target == null) return;
            _target.TakeDamage(weaponDamage);
        }

        private void AttackBehaviour()
        {
            transform.LookAt(_target.transform);
            if(_timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                _timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            _animator.ResetTrigger("stopAttack");
            //This will trigger the Hit() event
            _animator.SetTrigger("attack");
        }

        private void StopAttack()
        {
            _animator.ResetTrigger("attack");
            _animator.SetTrigger("stopAttack");
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < weaponRange;
        }
    }
}