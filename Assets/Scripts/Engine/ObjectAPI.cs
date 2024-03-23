using Atomic.Elements;
using Atomic.Extensions;
using UnityEngine;

namespace Game.Engine
{
    public static class ObjectAPI
    {
        [Contract(typeof(Transform))]
        public const string Transform = nameof(Transform);
        
        /// <summary>
        ///     <para>Move an object towards direction one frame.</para>>
        /// </summary>
        [Contract(typeof(IAtomicAction<Vector3>))]
        public const string MoveRequest = nameof(MoveRequest);

        /// <summary>
        ///     <para>Makes one gather request for a object.</para>>
        /// </summary>
        [Contract(typeof(IAtomicAction<Vector3>))]
        public const string GatherRequest = nameof(GatherRequest);
        
        /// <summary>
        ///     <para>Returns resource storage of an object.</para>>
        /// </summary>
        [Contract(typeof(ResouceStorage))]
        public const string ResourceStorage = nameof(ResourceStorage);
        
        [Contract(typeof(IAtomicValue<bool>))]
        public const string IsChopping = nameof(IsChopping);
        
    }
}