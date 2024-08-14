using System;
using UnityEngine;

namespace DungeonShooter
{
    public class LobbyCanvas : MonoBehaviour
    {
        public static LobbyCanvas instance;

        public GameObject blockPanel;
        public ViewModel[] viewModels;
        public Popup[] popups;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        public ViewModel FindViewModel(ViewModelType _type)
        {
            return Array.Find(viewModels, x => x.type == _type);
        }
        public void ChangeView(ViewModelType _type)
        {
            foreach(ViewModel view in viewModels)
            {
                view.gameObject.SetActive(view.type == _type);
            }
        }
        public void ShowPopup(PopupType _type)
        {
            blockPanel.SetActive(true);
            Popup popup = Array.Find(popups, x => x.type == _type);
            popup.gameObject.SetActive(true);
        }
        public void HidePopup(PopupType _type)
        {
            blockPanel.SetActive(false);
            Popup popup = Array.Find(popups, x => x.type == _type);
            popup.gameObject.SetActive(false);
        }
    }
}

