using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Objects
{
    [Serializable]
    public class WeaponMagazine
    {
        public event Action OnChanged;

        [SerializeField, Min(0), MaxValue(nameof(max))]
        private int current;

        [SerializeField, Min(0)]
        private int max;
        
        public int Current
        {
            get => current;
            private set
            {
                current = Math.Max(0, value);
                OnChanged?.Invoke();
            }
        }

        public int Max
        {
            get => max;
            set => max = Math.Max(0, value);
        }

        public bool IsEmpty => Current == 0;

        [Button]
        public void Reload()
        {
            Current = Max;
        }
        
        public void SpendCharge()
        {
            if (Current <= 0)
                return;
            
            Current--;
        }

        public void AddCharges(int range)
        {
            Current = Mathf.Min(Current + range, Max);
        }
    }
}