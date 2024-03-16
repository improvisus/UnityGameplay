using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.AI.Sensors;
using Game.Common;
using UnityEngine;

namespace Game.Components
{
    [Serializable]
    public class TriggerComponent
    {
        [SerializeField]
        private AtomicVariable<bool> enabledTriggerMechanics;
        [SerializeField]
        private Transform center;
        [SerializeField]
        private AtomicVariable<float> radius;
        [SerializeField]
        private AtomicVariable<LayerMask> layerMask;

        [Get(ObjectAPI.FoundTarget)]
        private AtomicVariable<IAtomicObject> foundTarget = new();
        private TriggerSensorMechanics triggerSensorMechanics;
        
        public void Compose()
        {
            triggerSensorMechanics = new TriggerSensorMechanics(enabledTriggerMechanics, center, radius, layerMask, foundTarget);
        }
        
        public void FixedUpdate(float fixedDeltaTime)
        {
            triggerSensorMechanics.FixedUpdate(fixedDeltaTime);
        }
        
        public void OnDrawGizmos()
        {
            if(Application.isPlaying)
                triggerSensorMechanics.OnGizmos();
        }
    }
}