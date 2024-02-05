using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Actions
{
    public class ShapeCastAction : IAtomicAction
    {
        private IAtomicAction<IAtomicObject> castAction;
        private Transform startPoint;
        private IAtomicValue<float> radius;
        private IAtomicValue<float> distance;
        private IAtomicValue<LayerMask> layerMask;
        
        public void Compose(IAtomicAction<IAtomicObject> castAction, Transform startPoint, IAtomicValue<float> radius,
                            IAtomicValue<float> distance, IAtomicValue<LayerMask> layerMask)
        {
            this.castAction = castAction;
            this.startPoint = startPoint;
            this.radius = radius;
            this.distance = distance;
            this.layerMask = layerMask;
        }
        
        public void Invoke()
        {
            if (Physics.SphereCast(startPoint.position, radius.Value, startPoint.forward,
                out var hit, distance.Value, layerMask.Value))
            {
                if (hit.collider.gameObject.TryGetComponent<IAtomicObject>(out var target))
                    castAction?.Invoke(target);
            }
        }
    }
}