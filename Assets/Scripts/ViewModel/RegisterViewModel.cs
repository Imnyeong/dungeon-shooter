using UnityEngine;
using UnityEngine.UI;

namespace DungeonShooter
{
    public class RegisterViewModel : ViewModel
    {
        public override ViewModelType type => ViewModelType.Register;
        [SerializeField] private InputField inputID;
        [SerializeField] private InputField inputNickname;
        [SerializeField] private InputField inputPW;

        [SerializeField] private Button btnRegister;
        [SerializeField] private Button btnCancel;

        private void Start()
        {
            inputID.onValueChanged.AddListener(delegate { AudioManager.instance.PlayClip(StringData.ClipTyping); });
            inputNickname.onValueChanged.AddListener(delegate { AudioManager.instance.PlayClip(StringData.ClipTyping); });
            inputPW.onValueChanged.AddListener(delegate { AudioManager.instance.PlayClip(StringData.ClipTyping); });

            btnRegister.onClick.AddListener(delegate
            {
                AudioManager.instance.PlayClip(StringData.ClipClick);
                if (inputID.text.Equals(string.Empty) || inputPW.text.Equals(string.Empty) || inputNickname.text.Equals(string.Empty))
                    return;

                Register(inputID.text, inputPW.text, inputNickname.text);
            });
            btnCancel.onClick.AddListener(delegate
            {
                AudioManager.instance.PlayClip(StringData.ClipClick);
                ClearData();
                LobbyCanvas.instance.ChangeView(ViewModelType.Login);
            });
        }
        private void ClearData()
        {
            inputID.text = string.Empty;
            inputNickname.text = string.Empty;
            inputPW.text = string.Empty;
        }
        public void Register(string _id, string _pw, string _nickname)
        {
            UserData sendData = new UserData
            {
                ID = _id,
                PW = _pw,
                Nickname = _nickname
            };
            WebRequestManager.instance.Register(sendData, (response) => 
            {
                ClearData();
                LobbyCanvas.instance.ChangeView(ViewModelType.Login);
            });
        }
    }
}
