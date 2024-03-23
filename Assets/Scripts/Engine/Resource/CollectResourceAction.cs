using System;
using Atomic.Elements;
using Atomic.Objects;
using Sirenix.OdinInspector;

namespace Game.Engine
{
    [Serializable, InlineProperty]
    public sealed class CollectResourceAction : IAtomicAction<IAtomicObject>
    {
        private ResouceStorage myStorage;
        private IAtomicValue<int> extractCount;

        public void Compose(ResouceStorage myStorage, IAtomicValue<int> extractCount)
        {
            this.myStorage = myStorage;
            this.extractCount = extractCount;
        }

        [Button]
        public void Invoke(IAtomicObject target)
        {
            if (target.TryGet(ObjectAPI.ResourceStorage, out ResouceStorage otherStorage) &&
                otherStorage.ExtractResources(this.extractCount.Value))
            {
                this.myStorage.PutResources(this.extractCount.Value);
            }
        }
    }
}