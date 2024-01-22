using System;
using UnityEngine;

namespace Game.Input
{
    public class KeyboardFireInput : MonoBehaviour, IFireInput
    {
        public event Action OnFire;

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                OnFire?.Invoke();
        }
    }
}