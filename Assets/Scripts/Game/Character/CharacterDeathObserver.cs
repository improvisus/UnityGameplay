using Game.Components;
using UnityEngine;

namespace Game.Character
{
    public class CharacterDeathObserver : MonoBehaviour
    {
        [SerializeField]
        private GameManager.GameManager gameManager;
        
        private CharacterService characterService;
        
        private HitPointsComponent hitPointsComponent;
        
        public void Construct(CharacterService characterService)
        {
            this.characterService = characterService;
        }
        
        private void Awake()
        {
            hitPointsComponent = characterService.Character.GetComponent<HitPointsComponent>();
        }
        
        private void OnEnable()
        {
            hitPointsComponent.OnHitPointEmpty += OnCharacterDeath;
        }

        private void OnDisable()
        {
            hitPointsComponent.OnHitPointEmpty -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _)
        {
            gameManager.FinishGame();
        }
    }
}