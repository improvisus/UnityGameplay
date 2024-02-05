using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Pooling
{
    public abstract class GenericPool<T, TArg> : MonoBehaviour where T : UnityEngine.Object
    {
        public Action<T> OnCreate;
        public Action<T> OnRelease;
        
        [SerializeField]
        protected T prefab;
        
        [SerializeField]
        protected GameObject poolContainer;
        
        [SerializeField]
        protected GameObject instanceContainer;
        
        [SerializeField]
        private int count;
        
        private readonly Queue<T> memoryPool = new Queue<T>();
        private readonly List<T> activePool = new List<T>();

        public void Awake()
        {
            poolContainer.SetActive(false);
            Build(prefab);
        }
        
        protected virtual void Build(T model)
        {
            for (var n = 0; n < count; n++)
            {
                var newInstance = CreateInstance(model);
                memoryPool.Enqueue(newInstance);
            }
        }
        
        public virtual T Create(TArg args)
        {
            if (!memoryPool.TryDequeue(out var obj))
            {
                if (activePool.Count == 0)
                    throw new Exception("Pull size error");
 
                obj = activePool[0];
                activePool.Remove(obj);
            }
            
            activePool.Add(obj);
            
            SetTransform(obj, instanceContainer.transform);
            obj = Init(obj, args);
            OnCreate?.Invoke(obj);
            return obj;
        }
        
        public virtual void Release(T obj)
        {
            activePool.Remove(obj);
            memoryPool.Enqueue(obj);
            OnRelease?.Invoke(obj);
            SetTransform(obj, poolContainer.transform);
        }
        
        protected abstract T Init(T projectile, TArg arg);

        protected virtual T CreateInstance(T model)
        {
            var obj = poolContainer != null ? Instantiate(model, poolContainer.transform) : Instantiate(model);
            Install(obj);
            return obj;
        }

        protected virtual void Install(T obj){ }
        
        protected virtual void RemoveInstance(T model)
        {
            var go = model switch
            {
                GameObject gameObjectCast => gameObjectCast,
                MonoBehaviour monoBehaviourCast => monoBehaviourCast.gameObject,
                _ => null
            };

            if (go != null)
                Destroy(go);
        }
        
        protected void SetTransform(T obj, Transform newTransform)
        {
            var go = obj switch
            {
                GameObject gameObjectCast => gameObjectCast,
                MonoBehaviour monoBehaviourCast => monoBehaviourCast.gameObject,
                _ => null
            };

            if (go != null)
                go.transform.parent = newTransform;
        }
    }
}