using UnityEngine;

namespace Game.Components
{
    public class ColorComponent : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public Color Color
        {
            set { spriteRenderer.color = value; }
        }
    }
}