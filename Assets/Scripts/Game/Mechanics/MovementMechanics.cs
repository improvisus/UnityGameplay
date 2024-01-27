using Atomic.Elements;
using UnityEngine;

namespace Game.Mechanics
{
    public class MovementMechanics
    {
        private readonly Transform transform;
        private readonly IAtomicValue<float> speed;
        private readonly IAtomicValue<Vector3> direction;

        public MovementMechanics(Transform transform, IAtomicValue<float> speed, IAtomicValue<Vector3> direction)
        {
            this.transform = transform;
            this.speed = speed;
            this.direction = direction;
        }

        public void Update(float deltaTime)
        {
            transform.position += direction.Value * (speed.Value * deltaTime);
        }
    }
}
