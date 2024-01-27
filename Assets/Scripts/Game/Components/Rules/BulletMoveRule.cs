using UnityEngine;

namespace Game.Components.Rules
{
    public class BulletMoveRule : MonoBehaviour
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