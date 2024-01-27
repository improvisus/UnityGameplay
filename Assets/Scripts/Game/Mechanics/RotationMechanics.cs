using Atomic.Elements;
using UnityEngine;

namespace Game.Mechanics
{
    public class RotationMechanics
    {
        private readonly Transform transform;
        private readonly IAtomicValue<Vector3> lookDirection;
        
        public RotationMechanics(Transform transform, IAtomicValue<Vector3> lookDirection)
        {
            this.transform = transform;
            this.lookDirection = lookDirection;
        }

        public void Update(float deltaTime)
        {
            var direction = lookDirection.Value;
            if (direction == Vector3.zero)
                return;
            
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }
}