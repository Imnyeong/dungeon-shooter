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

        [SerializeField] private ScrollRect scrollRect;

        [SerializeField] private GameObject scrollunit;

        private void Start()
        {
            GetUserList();
            
            btnRegister.onClick.AddListener(delegate
            {
                AddUser(inputID.text, inputPW.text, inputNickname.text);
            });
        }
        public void AddUser(string _id, string _pw, string _nickname)
        {
            UserData sendData = new UserData
            {
                ID = _id,
                PW = _pw,
                Nickname = _nickname
            };

            List<UserData> sendList = new List<UserData>();
            sendList.Add(sendData);

            WebRequestManager.instance.AddUser(sendList);
        }
        public void GetUserList()
        {
            WebRequestManager.instance.GetUserList((response) => ShowUserList(response));            
        }
        public void ShowUserList (string _jsonstring)
        {
            List<UserData> userList = JsonConvert.DeserializeObject<List<UserData>>(_jsonstring);
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
