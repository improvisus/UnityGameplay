using System;
using Atomic.Elements;
using Atomic.Objects;
using Game.Actions;
using Game.Common;
using Game.Conditions;
using Game.Controllers;
using UnityEngine;

namespace Game.Components
{
    [Serializable]
    public class FireComponent
    {
        public IAtomicEvent FireEvent => fireEvent;
        public IAtomicEvent FireRequest => fireRequest;
        public AndExpression FireCondition => fireCondition;
        public IAtomicAction FireAction => fireBulletAction;
        
        [SerializeField]
        private BulletPool bulletPool;
        
        [SerializeField]
        private Transform firePoint;
        
        [SerializeField, Get(ObjectAPI.FireRequest)]
        private AtomicEvent fireRequest = new();
        
        private FireBulletAction fireBulletAction = new();
        
        private AtomicEvent fireEvent = new();
        private AndExpression fireCondition = new();

        public void Compose()
        {
            fireBulletAction.Compose(bulletPool, firePoint, fireCondition, fireEvent);
        }
    }
}