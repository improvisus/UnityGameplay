using Atomic.Elements;
using Atomic.Objects;
using UnityEngine;

namespace Game.Actions
{
    public class RayCastAction : IAtomicAction
    {
        private IAtomicAction<IAtomicObject> castAction;
        private Transform startPoint;
        private IAtomicValue<LayerMask> layerMask;
        
        public void Compose(IAtomicAction<IAtomicObject> castAction, Transform startPoint, IAtomicValue<LayerMask> layerMask)
        {
            this.castAction = castAction;
            this.startPoint = startPoint;
            this.layerMask = layerMask;
        }
        
        public void Invoke()
        {
            if (Physics.Raycast(startPoint.position, startPoint.TransformDirection(Vector3.forward),
                    out var hit, Mathf.Infinity, layerMask.Value))
            {
                if (hit.collider.gameObject.TryGetComponent<IAtomicObject>(out var target))
                    castAction?.Invoke(target);
            }
        }
    }
}