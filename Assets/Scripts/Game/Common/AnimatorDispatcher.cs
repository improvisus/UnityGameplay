using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Common
{
    public enum AnimEventKey
    {
        OnShoot
    }

    public class AnimatorDispatcher : MonoBehaviour
    {
        private readonly Dictionary<AnimEventKey, Action> eventListeners = new Dictionary<AnimEventKey, Action>();
        
        public void AddListener(AnimEventKey eventKeyEventKey, Action action)
        {
            if (eventListeners.ContainsKey(eventKeyEventKey))
                eventListeners[eventKeyEventKey] += action;
            else
                eventListeners[eventKeyEventKey] = action;
        }
        
        public void RemoveListener(AnimEventKey eventKeyEventKey, Action action)
        {
            if (eventListeners.ContainsKey(eventKeyEventKey))
                eventListeners[eventKeyEventKey] -= action;
        }
        
        public void ReceiveEvent(AnimEventKey eventKeyEventKey)
        {
            if (eventListeners.TryGetValue(eventKeyEventKey, out var listener))
                listener?.Invoke();
            else
                Debug.LogWarning($"{eventKeyEventKey} event is not handled");
        }
    }
}