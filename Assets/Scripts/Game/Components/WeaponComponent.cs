using Game.Bullets;
using UnityEngine;

namespace Game.Components
{
    public class WeaponComponent : MonoBehaviour
    {
        [SerializeField]
        private BulletConfig bulletConfig;

        [SerializeField]
        private Transform firePoint;

        public BulletConfig BulletConfig => bulletConfig;

        public Vector2 Position => firePoint.position;

        public Quaternion Rotation => firePoint.rotation;
    }
}