using Game.Common;
using Game.Components;
using Pooling;
using UnityEngine;

namespace Game.Bullets
{
    public class BulletPool : GenericPool<GameObject, BulletPool.Args>
    {
        public struct Args
        {
            public Vector2 position;
            public Vector2 direction;
            public float speed;
            public Color color;
            public PhysicsLayer physicsLayer;
            public int damage;
            public TeamType teamType;
        }

        protected override GameObject Init(GameObject bullet, Args args)
        {
            bullet.transform.position = args.position;
            bullet.gameObject.layer = (int)args.physicsLayer;
            
            bullet.GetComponent<ColorComponent>().Color = args.color;
            bullet.GetComponent<DamageComponent>().Damage = args.damage;
            bullet.GetComponent<TeamComponent>().Type = args.teamType;
            bullet.GetComponent<MoveComponent>().Speed = args.speed;
            bullet.GetComponent<DirectionComponent>().Direction = args.direction;

            return bullet;
        }
    }

}