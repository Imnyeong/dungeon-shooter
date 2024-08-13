using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using System;
using Newtonsoft.Json;

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

        IEnumerator GetRequest(string _uri, Action<WebRequestResponse> _action = null)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(_uri))
            {
                yield return webRequest.SendWebRequest();

                if(webRequest.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Success Received: " + webRequest.downloadHandler.text);
                    if (_action != null)
                    {
                        WebRequestResponse response = JsonConvert.DeserializeObject<WebRequestResponse>(webRequest.downloadHandler.text);
                        _action(response);
                    }
                }
                else
                {
                    Debug.LogError("Error: " + webRequest.error);
                }
            }
        }
        IEnumerator PostRequest(string _uri, string _packet, Action<WebRequestResponse> _action = null)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(_uri, _packet))
            {
                byte[] jsonToSend = new UTF8Encoding().GetBytes(_packet);
                webRequest.uploadHandler = new UploadHandlerRaw(jsonToSend);

                //JsonHeader
                webRequest.SetRequestHeader("Content-Type", "application/json");

                yield return webRequest.SendWebRequest();

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    Debug.Log("Success Received: " + webRequest.result);
                    Debug.Log("Success Received: " + webRequest.downloadHandler.text);
                    
                    if (_action != null)
                    {
                        WebRequestResponse response = JsonConvert.DeserializeObject<WebRequestResponse>(webRequest.downloadHandler.text);
                        _action(response);
                    }
                }
                else
                {
                    Debug.LogError("Error: " + webRequest.error);
                }
            }
        }

        #region User
        public void GetUserInfo(string _id, Action<WebRequestResponse> _action = null)
        {
            StartCoroutine(GetRequest($"{url}/getuserinfo/{_id}", _action));
        }
        public void GetUserList(Action<WebRequestResponse> _action)
        {
            StartCoroutine(GetRequest($"{url}/getuserlist", _action));
        }
        public void Login(LoginData _data, Action<WebRequestResponse> _action = null)
        {
            StartCoroutine(GetRequest($"{url}/login/{JsonConvert.SerializeObject(_data)}", _action));
        }
        public void Register(UserData _data, Action<WebRequestResponse> _action = null)
        {
            StartCoroutine(PostRequest($"{url}/register", JsonConvert.SerializeObject(_data), _action ));
        }
        #endregion

        #region Room
        public void GetRoomList(Action<WebRequestResponse> _action)
        {
            StartCoroutine(GetRequest($"{url}/getroomlist", _action));
        }
        public void CreateRoom(RoomData _data, Action<WebRequestResponse> _action = null)
        {
            StartCoroutine(PostRequest($"{url}/createroom", JsonConvert.SerializeObject(_data), _action));
        }
        public void ModifyRoom(RoomData _data, Action<WebRequestResponse> _action = null)
        {
            StartCoroutine(PostRequest($"{url}/modifyroom", JsonConvert.SerializeObject(_data), _action));
        }
        public void DeleteRoom(RoomData _data, Action<WebRequestResponse> _action = null)
        {
            StartCoroutine(PostRequest($"{url}/deleteroom", JsonConvert.SerializeObject(_data), _action));
        }
        public void GetRoomInfo(int _id, Action<WebRequestResponse> _action = null)
        {
            StartCoroutine(GetRequest($"{url}/getroominfo/{_id}", _action));
        }
        public void GetRoomMember(int _id, Action<WebRequestResponse> _action = null)
        {
            StartCoroutine(GetRequest($"{url}/getroommember/{_id}", _action));
        }
        #endregion
    }
}