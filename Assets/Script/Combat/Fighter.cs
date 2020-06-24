using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Movement;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Combat
{

    public class Fighter : MonoBehaviour, IAction
    {

        [SerializeField] private float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttacks = 1f;
        [SerializeField] private float weaponDamage = 5f;

        private Transform target;
        private float timeSinceLastAttack = 0;

        private NavMeshAgent navMeshAgent;



        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            
            if(!GetIsInRange()) 
            {
                GetComponent<Mover>().MoveTo(target.position);

            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehavior();
            }
        }

        private void AttackBehavior()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // this will trigger Hit() event.
                GetComponent<Animator>().SetTrigger("attacking");
                timeSinceLastAttack = 0;
            }
        }

        void Hit()
        {
            Health healthComponent = target.GetComponent<Health>();
            healthComponent.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }


        public void Cancel()
        {
            target = null;
        }

        // animation event

    }
}
