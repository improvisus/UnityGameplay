using Game.Common;
using UnityEngine;

namespace Game.Bullets
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "Bullets/New BulletConfig")]
    public class BulletConfig : ScriptableObject
    {
        [SerializeField]
        public TeamType teamType;
        
        [SerializeField]
        public PhysicsLayer physicsLayer;

        [SerializeField]
        public Color color;

        [SerializeField]
        public int damage;

        [SerializeField]
        public float speed;
    }
}