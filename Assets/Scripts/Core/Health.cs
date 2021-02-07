using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour 
    {
        [SerializeField] float healthPoints = 100f;
        private bool _isDead;

        public bool IsDead()
        {
            return _isDead;
        }

        public void TakeDamage(float damage)
        {
            if(_isDead) return;

            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if(healthPoints == 0)
                Die();
        }

        private void Die()
        {
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            _isDead = true;
        }
    }
}

