using UnityEngine;

namespace Game.Level
{
    public class LevelBackground : MonoBehaviour
    {
        [SerializeField]
        private float startPositionY;
        
        [SerializeField]
        private float endPositionY;
        
        [SerializeField]
        private float movingSpeedY;
        
        [SerializeField]
        private Transform backgroundTransform;

        private float positionX;
        
        private float positionZ;

        private void Awake()
        {
            var position = backgroundTransform.position;
            positionX = position.x;
            positionZ = position.z;
        }

        private void FixedUpdate()
        {
            MoveBackground(Time.fixedDeltaTime);
        }
        
        private void MoveBackground(float deltaTime)
        {
            if (backgroundTransform.position.y <= endPositionY)
                backgroundTransform.position = new Vector3(positionX, startPositionY, positionZ);

            backgroundTransform.position -= new Vector3(positionX, movingSpeedY * deltaTime, positionZ);
        }
    }
}