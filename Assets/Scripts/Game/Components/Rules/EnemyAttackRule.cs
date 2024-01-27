using UnityEngine;

namespace Game.Components.Rules
{
    public class EnemyAttackRule : MonoBehaviour
    {
        [SerializeField]
        private float countdown;
        
        private EnemyMoveRule moveAgent;

        private GameObject target;
        private HitPointsComponent targetHitPoints;
        private WeaponComponent weaponComponent;
        private float currentTime;

        private void Awake()
        {
            moveAgent = GetComponent<EnemyMoveRule>();
            weaponComponent = GetComponent<WeaponComponent>();
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
                var direction = (Vector2) target.transform.position - weaponComponent.Position;
                weaponComponent.Fire(direction.normalized);
                
                currentTime += countdown;
            }
        }
    }
}