using UnityEngine;

namespace Game.Input
{
    public class KeyboardHorizontalMoveInput : MonoBehaviour, IHorizontalMoveInput
    {
        public Vector2 Direction { get; private set; }

        private void Update()
        {
            Direction = Vector2.zero;

            if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
                Direction = Vector2.left;

            if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
                Direction = Vector2.right;
        }
    }
}