using System;
using AIModule;
using Atomic.Behaviours;
using Atomic.Elements;
using Atomic.Extensions;
using Atomic.Objects;
using Engine.Look;
using Game.Engine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Sample
{
    [Is(ObjectType.Character)]
    public sealed class Character : AtomicObject
    {
        [Section]
        public Character_Core core;

        [Section]
        public Character_View view;

        [Section]
        public Character_AI ai;
        
        public override void Compose()
        {
            base.Compose();
            this.core.Compose(this);
            this.view.Compose(this.core);
            this.ai.Compose(this);
        }

        private void Awake()
        {
            this.Compose();
        }

        private void OnEnable()
        {
            this.view.Enable();
            this.ai.Enable();
        }

        private void OnDisable()
        {
            this.view.Disable();
            this.ai.Disable();
        }

        private void FixedUpdate()
        {
            float fixedDeltaTime = Time.fixedDeltaTime;
            this.core.OnFixedUpdate(fixedDeltaTime);
            this.ai.OnFixedUpdate(fixedDeltaTime);
        }

        private void LateUpdate()
        {
            this.view.OnLateUpdate(Time.deltaTime);
        }
        
        private void OnDrawGizmos()
        {
            this.ai.OnDrawGizmos();
        }
    }

    [Serializable]
    public sealed class Character_Core : IFixedUpdate
    {
        [Get(ObjectAPI.IsChopping)]
        public IAtomicValue<bool> IsChopping => this.isChopping;
        
        [Space]
        [Get(ObjectAPI.Transform)]
        public Transform transform;

        [Section, Space]
        public MoveComponent moveComponent;

        [Section, Space]
        public LookComponent lookComponent;

        [Space]
        public ActionComponent gatherComponent;
        
        [Space]
        [Get(ObjectAPI.ResourceStorage)]
        public ResouceStorage resourceStorage;

        [Space]
        public Axe axe;

        public CollectResourceAction collectResourceAction;
        
        [ShowInInspector, ReadOnly]
        private AtomicVariable<bool> isChopping = new();
        public void Compose(AtomicObject obj)
        {
            this.moveComponent.Compose();
            
            this.gatherComponent.Let(it =>
            {
                it.Condition.Append(new AtomicFunction<bool>(this.resourceStorage.IsNotFull));
                it.Condition.Append(new AtomicFunction<bool>(() => !this.isChopping.Value));
                it.Compose();

                it.Request.BindAs(ObjectAPI.GatherRequest, obj);
                
                it.Event.Subscribe(() => this.isChopping.Value = true);
            });
            
            this.axe.HitEvent.Subscribe(_ => this.isChopping.Value = false);
            this.axe.HitEvent.Subscribe(this.collectResourceAction);
            
            this.collectResourceAction.Compose(this.resourceStorage, 1.AsValue());
            
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (this.moveComponent.IsMoving.Value)
            {
                this.lookComponent.LookDirection.Value = this.moveComponent.MoveDirection.Value;
                
                this.isChopping.Value = false;
            }

            this.moveComponent.OnFixedUpdate(deltaTime);
            this.lookComponent.OnFixedUpdate(deltaTime);
        }
    }

    [Serializable]
    public sealed class Character_View : IEnable, IDisable, ILateUpdate
    {
        private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
        private static readonly int ChopAnimHash = Animator.StringToHash("Chop");

        private const string ChopAnimEvent = "chop";

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private AnimatorDispatcher dispatcher;

        private MoveAnimMechanics moveAnimMechanics;
        private AnimatorTriggerMechanics chopAnimatorTriggerMechanics;
        private AnimationEventListener chopAnimListener;
        
        public void Compose(Character_Core core)
        {
            this.moveAnimMechanics = new MoveAnimMechanics(
                this.animator, IsMovingHash, core.moveComponent.IsMoving
            );
            this.chopAnimatorTriggerMechanics = new AnimatorTriggerMechanics(
                this.animator, ChopAnimHash, core.gatherComponent.Event
            );
            this.chopAnimListener = new AnimationEventListener(
                this.dispatcher, ChopAnimEvent, core.axe.HitAction
            );
        }

        public void Enable()
        {
            this.chopAnimListener.Enable();
            this.chopAnimatorTriggerMechanics.Enable();
        }

        public void Disable()
        {
            this.chopAnimatorTriggerMechanics.Disable();
            this.chopAnimListener.Disable();
        }

        public void OnLateUpdate(float deltaTime)
        {
            this.moveAnimMechanics.OnUpdate(deltaTime);
        }
    }
    
    [Serializable]
    public sealed class Character_AI : IFixedUpdate, IEnable, IDisable
    {
        [SerializeField, HideInPlayMode]
        private bool aiEnabled = true;
        
        [Space]
        public Blackboard blackboard;
        public AIBehaviour behaviour;

        public void Compose(AtomicObject character)
        {
            this.blackboard.SetObject(BlackboardAPI.Character, character);
            character.AddProperty(nameof(Blackboard), this.blackboard);
        }

        public void OnFixedUpdate(float deltaTime)
        {
            if (this.aiEnabled)
            {
                this.behaviour.OnUpdate(deltaTime);
            }
        }

        public void OnDrawGizmos()
        {
            if (this.aiEnabled)
            {
                this.behaviour.OnGizmos();
            }
        }

        public void Enable()
        {
            if (this.aiEnabled)
            {
                this.behaviour.OnStart();
            }
        }

        public void Disable()
        {
            if (this.aiEnabled)
            {
                this.behaviour.OnStop();
            }
        }
    }
}