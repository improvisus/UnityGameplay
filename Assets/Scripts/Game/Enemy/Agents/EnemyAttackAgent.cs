using System;
using Game.Components;
using UnityEngine;

namespace Game.Enemy.Agents
{
    public class EnemyAttackAgent : MonoBehaviour
    {
        public event Action<GameObject, GameObject> OnFire;
        
        [SerializeField]
        private float countdown;
        
        private EnemyMoveAgent moveAgent;

        private GameObject target;
        private HitPointsComponent targetHitPoints;
        private float currentTime;

        private void Awake()
        {
            moveAgent = GetComponent<EnemyMoveAgent>();
        }
        
        public void SetTarget(GameObject target)
        {
            this.target = target;
            targetHitPoints = target.GetComponent<HitPointsComponent>();
        }

        public void Reset()
        {
            currentTime = countdown;
        }

        private void FixedUpdate()
        {
            Attack();
        }

        private void Attack()
        {
            if (!moveAgent.IsReached)
                return;
            
            if (!target)
                return;
            
            if (!targetHitPoints.IsHitPointsExists)
                return;

            currentTime -= Time.fixedDeltaTime;
            if (currentTime <= 0)
            {
                OnFire?.Invoke(gameObject, target);
                currentTime += countdown;
            }
        }
    }
}