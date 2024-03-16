using System;
using Atomic.Objects;
using Game.Common;
using Game.Components;
using Game.Effects;
using Game.Mechanics;
using UnityEngine;

namespace Game.Objects
{
    [Serializable]
    public class CharacterCore : IDisposable
    {
        [Section]
        public MoveComponent moveComponent;
        [Section]
        public HealthComponent healthComponent;
        [Section]
        public WeaponComponent weaponComponent;
        [Section]
        public AttackComponent attackComponent;
        
        [SerializeField, Get(ObjectAPI.Transform)]
        private Transform mainTransform;
        
        [Get(ObjectAPI.EffectManager)]
        [SerializeField]
        private EffectManager effectManager;
        
        private RotationMechanics rotationMechanics;
        private DestroyMechanics destroyMechanics;
        
        public void Compose(Character character)
        {
            moveComponent.Compose(mainTransform);
            healthComponent.Compose();
            attackComponent.Compose(weaponComponent);
            weaponComponent.Compose();
            
            effectManager.Compose(character);
            
            attackComponent.AttackCondition.Compose(() =>
                moveComponent.IsNotMoving.Value &&
                weaponComponent.CurrentWeapon.Value.CanAttack.Value);
            
            destroyMechanics = new DestroyMechanics(healthComponent.DeathEvent, character);
            rotationMechanics = new RotationMechanics(mainTransform, moveComponent.Direction);
        }

        public void OnEnable()
        {
            healthComponent.OnEnable();
            destroyMechanics.OnEnable();
        }

        public void OnDisable()
        {
            healthComponent.OnDisable();
            destroyMechanics.OnDisable();
        }
        
        public void Update(float deltaTime)
        {
            moveComponent.Update(deltaTime);
            rotationMechanics.Update(deltaTime);
        }
        
        public void Dispose()
        {
            healthComponent?.Dispose();
        }
    }
}