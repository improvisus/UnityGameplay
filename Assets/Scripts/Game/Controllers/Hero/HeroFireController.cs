using Atomic.Elements;
using Game.Common;
using Game.Input;
using UnityEngine;

namespace Game.Controllers
{
    public class HeroFireController : MonoBehaviour
    {
        private HeroService heroService;
        
        private IFireInput fireInput;
        
        public void Constructs(HeroService heroService, IFireInput fireInput)
        {
            this.heroService = heroService;
            this.fireInput = fireInput;
        }

        private void OnEnable()
        {
            fireInput.OnFire += OnFireRequest;
        }
        
        private void OnDisable()
        {
            fireInput.OnFire -= OnFireRequest;
        }
        
        private void OnFireRequest()
        {
            var fireRequest = heroService.Character.Get<IAtomicAction>(ObjectAPI.AttackRequest);
            fireRequest?.Invoke();
        }
    }
}

