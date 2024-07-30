using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Text;
using System;

namespace DungeonShooter
{
    public class WebRequestManager : MonoBehaviour
    {
        public static WebRequestManager instance;
        private const string url = "";

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
        }
        public class Response
        {
            public int code;
            public string message;
        }
        IEnumerator GetRequest(string _uri, Action<string> _action = null)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(_uri))
            {
                yield return webRequest.SendWebRequest();

                if(webRequest.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Success Received: " + webRequest.result);
                    if (_action != null)
                    {
                        Response response = JsonConvert.DeserializeObject<Response>(webRequest.downloadHandler.text);
                        _action(response.message);
                    }
                }
                else
                {
                    Debug.LogError("Error: " + webRequest.error);
                }
            }
        }
        IEnumerator PostRequest(string _uri, string _packet)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Post(_uri, _packet))
            {
                byte[] jsonToSend = new UTF8Encoding().GetBytes(_packet);
                webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);

                //JsonHeader
                webRequest.SetRequestHeader("Content-Type", "application/json");

                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Success Received: " + webRequest.result);
                }
                else
                {
                    Debug.LogError("Error: " + webRequest.error);
                }
            }
        }

        #region User
        public void GetUserList(Action<string> _action)
        {
            StartCoroutine(GetRequest($"{url}/getuserlist", _action));
        }
        public void AddUser(List<UserData> _data)
        {
            StartCoroutine(PostRequest($"{url}/register", JsonConvert.SerializeObject(_data)));
        }
        #endregion
    }
}