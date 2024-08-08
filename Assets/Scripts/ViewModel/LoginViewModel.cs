using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonShooter
{
    public class LoginViewModel : ViewModel
    {
        public override ViewModelType type => ViewModelType.Login;
        [SerializeField] private InputField inputID;
        [SerializeField] private InputField inputPW;

        [SerializeField] private Button btnLogin;
        [SerializeField] private Button btnRegister;

        private void Start()
        {
            btnLogin.onClick.AddListener(delegate
            {
                if (inputID.text.Equals(string.Empty) || inputPW.text.Equals(string.Empty))
                    return;

                Login(inputID.text, inputPW.text);
            });
            btnRegister.onClick.AddListener(delegate
            {
                ClearData();
                LobbyCanvas.instance.ChangeView(ViewModelType.Register);
            });
        }
        private void ClearData()
        {
            inputID.text = string.Empty;
            inputPW.text = string.Empty;
        }
        private void Login(string _id, string _pw)
        {
            LoginData sendData = new LoginData
            {
                ID = _id,
                PW = _pw
            };
            WebRequestManager.instance.Login(sendData, (response) => LoginSuccess(response));
        }
        public void LoginSuccess(WebRequestResponse _response)
        {
            if (_response.code == 400)
                return;

            UserData user = JsonConvert.DeserializeObject<UserData>(_response.message);

            LocalDataBase.instance.loginData = user;
            LobbyCanvas.instance.ChangeView(ViewModelType.Lobby);
            //Debug.Log($"ID = {user.ID}, PW = {user.PW}, Nickname = {user.Nickname}, Win = {user.Win}, Lose = {user.Lose}");
        }
    }
}
