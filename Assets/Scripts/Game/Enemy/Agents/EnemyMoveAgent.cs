using Game.Components;
using UnityEngine;

namespace Game.Enemy.Agents
{
    public class EnemyMoveAgent : MonoBehaviour
    {
        [SerializeField]
        private float reachingDistance = 0.25f;

        private MoveComponent moveComponent;

        private Vector2 destination;

        private bool isReached;

        public bool IsReached => isReached;
        
        private void Awake()
        {
            moveComponent = GetComponent<MoveComponent>();
        }
        
        public void SetDestination(Vector2 endPoint)
        {
            destination = endPoint;
            isReached = false;
        }

        private void FixedUpdate()
        {
            Move();
        }
        
        private void Move()
        {
            if (isReached)
                return;

            var vector = destination - (Vector2)transform.position;
            if (vector.magnitude <= reachingDistance)
            {
                isReached = true;
                return;
            }

            var direction = vector.normalized * Time.fixedDeltaTime;
            moveComponent.Move(direction);
        }

    }
}