using AIModule;
using Atomic.Objects;
using UnityEngine;

namespace Game.Engine.AI.Sensors
{
    [CreateAssetMenu(
        fileName = "NearTargetSensor",
        menuName = "Engine/AI/New NearTargetSensor"
    )]
    public class NearTargetSensor : AIMechanics, IAIGizmos
    {
        private static readonly Collider[] buffer = new Collider[32];

        [SerializeField, BlackboardKey]
        private ushort target;
        
        [SerializeField, BlackboardKey]
        private ushort center;

        [SerializeField]
        private float radius;

        [SerializeField]
        private LayerMask layerMask;

        public override void OnUpdate(IBlackboard blackboard, float deltaTime)
        {
            if (!blackboard.TryGetObject(this.center, out Transform center))
                return;

            int count = Physics.OverlapSphereNonAlloc(center.position, radius, buffer, this.layerMask);
            
            IAtomicObject target = null;
            var minDistance = float.MaxValue;
            for (var i = 0; i < count; i++)
            {
                var collider = buffer[i];
                if (collider.TryGetComponent(out IAtomicObject obj) && obj.TryGet(ObjectAPI.Transform, out Transform targetTransform))
                {
                    var direction = targetTransform.position - center.position;
                    var distance = direction.sqrMagnitude;
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        target = obj;
                    }
                }
            }
            
            if (target != null)
            {
                blackboard.SetObject(this.target, target);
                return;
            }
            
            blackboard.DeleteObject(this.target);
        }

        
        public void OnGizmos(IBlackboard blackboard)
        {
            if (!blackboard.TryGetObject(this.center, out Transform center))
                return;
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(center.position, radius);
        }
    }
}