using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonShooter
{
    public class RegisterViewModel : MonoBehaviour
    {
        [SerializeField] private InputField inputID;
        [SerializeField] private InputField inputPW;
        [SerializeField] private InputField inputNickname;

        [SerializeField] private Button btnRegister;
        [SerializeField] private Button btnLogin;

        [SerializeField] private ScrollRect scrollRect;

        [SerializeField] private GameObject scrollunit;

        private void Start()
        {
            GetUserList();

            btnRegister.onClick.AddListener(delegate
            {
                AddUser(inputID.text, inputPW.text, inputNickname.text);
            });
            btnLogin.onClick.AddListener(delegate
            {
                Login(inputID.text, inputPW.text);
            });
        }
        public void Login(string _id, string _pw)
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
            Debug.Log($"ID = {user.ID}, PW = {user.PW}, Nickname = {user.Nickname}, Win = {user.Win}, Lose = {user.Lose}");
        }
        public void AddUser(string _id, string _pw, string _nickname)
        {
            UserData sendData = new UserData
            {
                ID = _id,
                PW = _pw,
                Nickname = _nickname
            };

            WebRequestManager.instance.AddUser(sendData);
        }
        public void GetUserList()
        {
            WebRequestManager.instance.GetUserList((response) => ShowUserList(response));            
        }
        public void ShowUserList (WebRequestResponse _response)
        {
            if (_response.code == 400)
                return;

            List<UserData> userList = JsonConvert.DeserializeObject<List<UserData>>(_response.message);
            for (int i = 0; i < scrollRect.content.childCount; i++)
            {
                Destroy(scrollRect.content.GetChild(i).gameObject);
            }
            for (int i = 0; i < userList.Count; i++)
            {
                GameObject unit = Instantiate(scrollunit, scrollRect.content);
                unit.GetComponent<UserUnit>().Init(userList[i]);
            }
        }
    }
}
