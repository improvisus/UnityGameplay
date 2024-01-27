using Atomic.Elements;
using Atomic.Extensions;
using Game.Common;
using Game.Input;
using UnityEngine;

namespace Game.Controllers
{
    public class HeroMoveController : MonoBehaviour
    {
        private HeroService heroService;
        
        private IMoveInput moveInput;
        
        public void Constructs(HeroService heroService, IMoveInput moveInput)
        {
            this.heroService = heroService;
            this.moveInput = moveInput;
        }

        private void Update()
        {
            IAtomicVariable<Vector3> heroDirection = heroService.Character.GetVariable<Vector3>(ObjectAPI.MoveDirection);
            
            if (heroDirection != null)
                heroDirection.Value = moveInput.Direction;
        }
    }
}