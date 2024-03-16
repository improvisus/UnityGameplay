using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.AI.Sensors
{
    public class TriggerSensorMechanics
    {
        private static readonly Collider[] buffer = new Collider[32];
        
        private IAtomicValue<bool> enabledTriggerMechanics;
        private Transform center;
        private IAtomicValue<float> radius;
        private IAtomicValue<LayerMask> layerMask;
        
        private IAtomicSetter<IAtomicObject> foundTarget;

        public TriggerSensorMechanics(IAtomicValue<bool> enabledTriggerMechanics,
                                Transform center,
                                IAtomicValue<float> radius,
                                IAtomicValue<LayerMask> layerMask,
                                IAtomicSetter<IAtomicObject> foundTarget)
        {
            this.enabledTriggerMechanics = enabledTriggerMechanics;
            this.center = center;
            this.radius = radius;
            this.layerMask = layerMask;
            this.foundTarget = foundTarget;
        }

        public void FixedUpdate(float fixedDeltaTime)
        {
            if(!enabledTriggerMechanics.Value)
                return;
            
            int count = Physics.OverlapSphereNonAlloc(center.position, radius.Value, buffer, layerMask.Value);
            for (var n = 0; n < count; n++)
            {
                var collider = buffer[n];

                if (!collider.TryGetComponent(out IAtomicObject obj))
                    continue;

                if (obj == null || obj.Equals(null))
                {
                    foundTarget.Value = null;
                    return;
                }
                foundTarget.Value = obj;
                
                return;
            }

            foundTarget.Value = null;
        }
    }
}