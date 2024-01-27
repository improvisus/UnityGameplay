using UnityEngine;

namespace Game.Components.Rules
{
    public class BulletCollisionRule : MonoBehaviour
    {
        private TeamComponent teamComponent;
        private DamageComponent damageComponent;
        
        private void Awake()
        {
            teamComponent = GetComponent<TeamComponent>();
            damageComponent = GetComponent<DamageComponent>();
        }
        
        public void Collision(GameObject other)
        {
            if (!other.TryGetComponent(out TeamComponent team))
                return;
            
            if(teamComponent.Type == team.Type)
                return;

            if (other.TryGetComponent(out HitPointsComponent hitPoints))
                hitPoints.TakeDamage(damageComponent.Damage);
        }
    }
}