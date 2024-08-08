using UnityEngine;

namespace DungeonShooter
{
    public class LobbyCanvas : MonoBehaviour
    {
        public static LobbyCanvas instance;

        public ViewModel[] viewModels;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void ChangeView(ViewModelType _type)
        {
            foreach(ViewModel view in viewModels)
            {
                view.gameObject.SetActive(view.type == _type);
            }
        }
    }
}

