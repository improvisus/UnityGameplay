using Atomic.Objects;
using UnityEngine;

namespace Game.Controllers
{
    public class HeroService : MonoBehaviour
    {
        [SerializeField]
        private AtomicObject character;
        
        public AtomicObject Character => character;
    }
}