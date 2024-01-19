using System;
using UnityEngine;

namespace Game.Components
{
    public class CollisionComponent : MonoBehaviour
    {
        public event Action<GameObject, Collision2D> OnCollisionEntered;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(gameObject, collision);
        }
    }
}