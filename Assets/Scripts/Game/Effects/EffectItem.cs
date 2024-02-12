using Atomic.Behaviours;
using Game.Common;
using UnityEngine;

namespace Game.Effects
{
    public class EffectItem : MonoBehaviour
    {
         [SerializeField]
         private CompositeEffect effectPrefab;
        
        private void OnTriggerEnter(Collider col)
        {
            var obj = col.GetComponentInParent<AtomicBehaviour>();

            if (obj != null && obj.TryGet(ObjectAPI.EffectManager, out EffectManager effectManager))
            {
                Transform objTransform = obj.transform;
                IEffect effect = Instantiate(effectPrefab, objTransform.position, objTransform.rotation, objTransform);
                effectManager.ApplyEffect(effect);
                Destroy(gameObject);
            }
        }
    }
}