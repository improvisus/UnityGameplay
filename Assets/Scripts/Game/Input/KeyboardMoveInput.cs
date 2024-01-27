using UnityEngine;

namespace Game.Input
{
    public class KeyboardMoveInput : MonoBehaviour, IMoveInput
    {
        public Vector3 Direction { get; private set; }

        private void Update()
        {
            Direction = Vector3.zero;

            if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
                Direction = Vector3.left;

            if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
                Direction = Vector3.right;

            if (UnityEngine.Input.GetKey(KeyCode.UpArrow))
                Direction = Vector3.forward;

            if (UnityEngine.Input.GetKey(KeyCode.DownArrow))
                Direction = Vector3.back;
        }
    }
}