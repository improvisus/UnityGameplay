using Game.Components;
using Game.Input;
using UnityEngine;

namespace Game.Character
{
    public class CharacterMoveController : MonoBehaviour
    {
        private CharacterService characterService;
        
        private IHorizontalMoveInput horizontalMoveInput;
        
        private MoveComponent moveComponent;
        
        public void Construct(CharacterService characterService, IHorizontalMoveInput horizontalMoveInput)
        {
            this.characterService = characterService;
            this.horizontalMoveInput = horizontalMoveInput;
        }
        
        private void Awake()
        {
            moveComponent = characterService.Character.GetComponent<MoveComponent>();
        }
        
        private void FixedUpdate()
        {
            moveComponent.Move(horizontalMoveInput.Direction * Time.fixedDeltaTime);
        }
    }
}