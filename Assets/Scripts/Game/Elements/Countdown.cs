using System;
using UnityEngine;

namespace Game.Elements
{
    [Serializable]
    public class Countdown
    {
        [SerializeField]
        public float duration;
        private float currentTime;

        private bool enable = false;
        
        public bool IsPlaying => enable && currentTime > 0;
        
        public bool IsFinished => enable && ( currentTime <= 0 || currentTime >= duration);


        public Countdown()
        {
            Reset();
        }

        public Countdown(float duration)
        {
            this.duration = duration;
            Reset();
        }

        public void Stop()
        {
            enable = false;
        }
        
        public void Update(float deltaTime)
        {
            if(enable)
                currentTime = Mathf.Max(currentTime - deltaTime, 0);
        }

        public void Reset()
        {
            enable = true;
            currentTime = duration;
        }
    }
}
