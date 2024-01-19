using Game.Common;
using UnityEngine;

namespace Game.Components
{
    public class TeamComponent : MonoBehaviour
    {
        [SerializeField]
        private TeamType teamType;

        public TeamType Type
        {
            get { return teamType; }
            set { teamType = value; }
        }
    }
}