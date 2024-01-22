using Atomic.Objects;
using UnityEngine;

namespace Game.Controller
{
    public class CharacterService : MonoBehaviour
    {
        [SerializeField]
        private AtomicObject character;
        
        public AtomicObject Character => character;
    }
}