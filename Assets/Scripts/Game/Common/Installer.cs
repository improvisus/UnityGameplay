using UnityEngine;

namespace Game.Common
{
    [DefaultExecutionOrder(-1000)]
    public abstract class Installer : MonoBehaviour
    {
        private void Awake()
        {
            Install();
        }

        protected abstract void Install();
    }
}