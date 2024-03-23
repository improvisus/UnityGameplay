using System;
using Atomic.Extensions;
using Atomic.Objects;
using Game.Engine;
using UnityEngine;

namespace Sample
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField]
        private AtomicObject character;

        private void Update()
        {
            this.MoveInput();
            this.GatherInput();
        }

        private void GatherInput()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.character.InvokeAction(ObjectAPI.GatherRequest);
            }
        }

        private void MoveInput()
        {
            Vector3 direction = Vector3.zero;
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                direction.x = -1;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                direction.x = 1;
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                direction.z = 1;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                direction.z = -1;
            }

            if (direction != Vector3.zero)
            {
                this.character.InvokeAction(ObjectAPI.MoveRequest, direction);
            }
        }
    }
}