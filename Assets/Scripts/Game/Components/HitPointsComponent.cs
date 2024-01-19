using System;
using UnityEngine;

namespace Game.Components
{
    public class HitPointsComponent : MonoBehaviour
    {
        [SerializeField]
        private int hitPoints;
        
        public event Action<GameObject> OnHitPointEmpty;

        public bool IsHitPointsExists => hitPoints > 0;

        public void TakeDamage(int damage)
        {
            hitPoints -= damage;
            if (hitPoints <= 0)
                OnHitPointEmpty?.Invoke(gameObject);
        }
    }
}