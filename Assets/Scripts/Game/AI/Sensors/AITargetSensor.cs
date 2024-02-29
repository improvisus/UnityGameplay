using AIModule;
using Atomic.Objects;
using UnityEngine;

namespace Game.AI.Sensors
{
    [CreateAssetMenu(
        fileName = "AITargetSensor",
        menuName = "Content/AI/Sensors/New AITargetSensor"
    )]
    public class AITargetSensor : AIMechanics, IAIGizmos
    {
        private static readonly Collider[] buffer = new Collider[32];
        
        [SerializeField, BlackboardKey]
        private ushort center;

        [SerializeField, BlackboardKey]
        private ushort radius;
        
        [SerializeField, BlackboardKey]
        private ushort target;
        
        [SerializeField]
        private LayerMask layerMask;
        
        public override void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(this.center, out Transform center) ||
                !blackboard.TryGetFloat(this.radius, out float radius))
            {
                return;
            }

            int count = Physics.OverlapSphereNonAlloc(center.position, radius, buffer, layerMask);
            for (var n = 0; n < count; n++)
            {
                var collider = buffer[n];

                if (!collider.TryGetComponent(out IAtomicObject obj))
                    continue;
                
                return;
            }

            blackboard.DeleteObject(target);
        }
        
        public void OnGizmos(IBlackboard blackboard)
        {
            if (!blackboard.TryGetObject(this.center, out Transform center) ||
                !blackboard.TryGetFloat(this.radius, out float radius))
            {
                return;
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(center.position, radius);
        }
    }
}