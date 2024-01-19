using Game.Components;
using UnityEngine;

namespace Game.Bullets.Agents
{
    public class BulletMoveAgent : MonoBehaviour
    {
        private DirectionComponent directionComponent;
        private MoveComponent moveComponent;

        private void Awake()
        {
            directionComponent = GetComponent<DirectionComponent>();
            moveComponent = GetComponent<MoveComponent>();
        }
        
        private void FixedUpdate()
        {
            moveComponent.Move(directionComponent.Direction * Time.fixedDeltaTime);
        }
    }
}