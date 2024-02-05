using Atomic.Elements;
using Atomic.Objects;
using Game.Extensions;
using UnityEngine;

namespace Game.Actions
{
    public class ConeCastAction : IAtomicAction
    {
        private const int HitBufferSize = 32;
        private IAtomicAction<IAtomicObject> castAction;
        private Transform startPoint;
        private IAtomicValue<float> distance;
        private IAtomicValue<float> angle;
        private IAtomicValue<LayerMask> layerMask;
        
        public void Compose(IAtomicAction<IAtomicObject> castAction, Transform startPoint, IAtomicValue<float> distance,
                            IAtomicValue<float> angle, IAtomicValue<LayerMask> layerMask)
        {
            this.castAction = castAction;
            this.startPoint = startPoint;
            this.distance = distance;
            this.angle = angle;
            this.layerMask = layerMask;
        }
        
        public void Invoke()
        {
            var hits = SpecificsCastExtension.ConeCastAll(startPoint.position, startPoint.forward, 
                                distance.Value, angle.Value, layerMask.Value, HitBufferSize);
            
            if (hits.Length == 0)
                return;
            
            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent<IAtomicObject>(out var target))
                    castAction?.Invoke(target);
            }
        }
    }
}
