using System;
using UnityEngine;

namespace Game.Elements
{
    [Serializable]
    public class HitPoints
    {
        public bool IsFull => hitPoints == maxHitPoints;
        public event Action<int> OnChanged;
        public event Action OnOver;

        [SerializeField]
        private int maxHitPoints;

        [SerializeField]
        private int hitPoints;

        public int Current
        {
            get
            {
                return hitPoints;
            }
            set
            {
                var prevHitPoints = hitPoints;
                
                value = Mathf.Clamp(value, 0, maxHitPoints);
                hitPoints = value;
                OnChanged?.Invoke(value);
                
                if(prevHitPoints != 0 && hitPoints == 0)
                    OnOver?.Invoke();
            }
        }
        
        public int Max
        {
            get
            {
                return maxHitPoints;
            }
            set
            {
                value = Math.Max(1, value);
                if (hitPoints > value)
                    hitPoints = value;

                maxHitPoints = value;
            }
        }
        
        public HitPoints() { }

        public HitPoints(int current, int max)
        {
            hitPoints = current;
            maxHitPoints = max;
        }

        public void Restore()
        {
            hitPoints = maxHitPoints;
        }
    }
}
