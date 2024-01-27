using Atomic.Elements;
using Game.Common;
using UnityEngine;

namespace Game.Controllers
{
    public class HeroFireController : MonoBehaviour
    {
        private HeroService heroService;
        private AnimatorDispatcher animatorDispatcher;
        private const string FireEvent = "shoot";
        
        public void Constructs(HeroService heroService)
        {
            this.heroService = heroService;
        }

        private void Awake()
        {
            animatorDispatcher = heroService.Character.Get<AnimatorDispatcher>(ObjectAPI.AnimatorDispatcher);
        }

        private void OnEnable()
        {
            animatorDispatcher.OnEventReceived += OnEventReceived;
        }
        
        private void OnDisable()
        {
            animatorDispatcher.OnEventReceived -= OnEventReceived;
        }
        
        private void OnEventReceived(string eventКey)
        {
            if (eventКey == FireEvent)
            {
                var fireAction = heroService.Character.Get<IAtomicAction>(ObjectAPI.FireAction);
                fireAction?.Invoke();
            }
        }
    }
}