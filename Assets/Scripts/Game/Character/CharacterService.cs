using UnityEngine;

namespace Game.Character
{
    public class CharacterService : MonoBehaviour
    {
        [SerializeField]
        private GameObject character;
        
        public GameObject Character => character;
    }
}