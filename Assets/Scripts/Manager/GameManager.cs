using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public GameObject characterPrefab;
        public GameObject cameraPrefab;
        public GameObject map;

        public string currentPlayer;

        public RoomData currentRoom = null;
        public UserData[] users = null;

        public List<Character> players;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        private void Start()
        {
            WebRequestManager.instance.GetRoomInfo(LocalDataBase.instance.currentRoom, (response) =>
            {
                SetGame(response);
            });
        }
        public void SetGame(WebRequestResponse _response)
        {
            if (_response.code == 400)
                return;

            currentPlayer = LocalDataBase.instance.loginData.ID;
            currentRoom = JsonConvert.DeserializeObject<RoomData>(_response.message);
            users = JsonConvert.DeserializeObject<UserData[]>(currentRoom.Players);

            foreach (UserData user in users)
            {
                SpawnCharacter(user.ID);
            }
        }
        public void SetCamera(string _id)
        {
            GameObject camera = Instantiate(cameraPrefab, map.transform);
            FollowCam followCam = camera.GetComponent<FollowCam>();
            followCam.SetTarget(_id);
        }
        public void SpawnCharacter(string _id)
        {
            GameObject character = Instantiate(characterPrefab, map.transform);
            Character player = character.GetComponent<Character>();

            player.SetInfo(_id);
            players.Add(player);

            if (currentPlayer == _id)
                SetCamera(_id);
        }
        public void SyncCharacters(string _data)
        {
            CharacterPacket info = JsonConvert.DeserializeObject<CharacterPacket>(_data);
            Character player = players.Find(x => x.id == info.id);

            if (player == null)
                return;

            player.DoSync(info.position, info.rotation, info.animation);
        }
    }
}