using System;
using UnityEngine;

namespace Game.Elements
{
    [Serializable]
    public class Countdown
    {
        public float duration;
        public float currentTime;

        public bool IsPlaying => currentTime > 0;
        
        public bool IsFinished => currentTime <= 0;

        public Countdown()
        {
            Reset();
        }

        public Countdown(float duration)
        {
            this.duration = duration;
            Reset();
        }
        
        public void Update(float deltaTime)
        {
            //currentTime -= deltaTime;
            currentTime = Mathf.Max(currentTime - deltaTime, 0);
        }

        public void Reset()
        {
            currentTime = duration;
        }
    }
}
