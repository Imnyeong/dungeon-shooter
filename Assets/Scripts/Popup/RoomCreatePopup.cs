using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonShooter
{
    public class RoomCreatePopup : Popup
    {
        public override PopupType type => PopupType.CreateRoom;

        [SerializeField] private InputField inputRoomName;

        [SerializeField] private Button btnCreate;
        [SerializeField] private Button btnCancel;

        private void Start()
        {
            btnCreate.onClick.AddListener(delegate
            {
                //GetRoomList();
            });
            btnCancel.onClick.AddListener(delegate
            {
                ClearData();
                LobbyCanvas.instance.HidePopup(PopupType.CreateRoom);
            });
        }

        private void ClearData()
        {
            inputRoomName.text = string.Empty;
        }
    }
}

