using System;
using UnityEngine;

namespace Game.Input
{
    public class MouseFireInput : MonoBehaviour, IFireInput
    {
        public event Action OnFire;

        private void Update()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
                OnFire?.Invoke();
        }
    }
}