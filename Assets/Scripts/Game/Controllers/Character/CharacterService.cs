using Atomic.Objects;
using UnityEngine;

namespace Game.Controllers.Character
{
    public class CharacterService : MonoBehaviour
    {
        [SerializeField]
        private AtomicObject character;
        
        public AtomicObject Character => character;
    }
}