using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace DungeonShooter
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public GameObject characterPrefab;
        public GameObject cameraPrefab;
        public GameObject map;

        public UserData currentUser;
        public Character currentPlayer;

        public RoomData currentRoom = null;
        public UserData[] users = null;

        public List<Character> players;
        public Transform[] spawnPoint;

        public ObjectPool objectPool;

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

            currentUser = LocalDataBase.instance.loginData;
            currentRoom = JsonConvert.DeserializeObject<RoomData>(_response.message);
            users = JsonConvert.DeserializeObject<UserData[]>(currentRoom.Players);

            for (int i = 0; i < users.Length; i++)
            {
                SpawnCharacter(i);
            }
        }
        public void CheckGame()
        {
            int liveCount = players.FindAll(player => player.hp != 0).Count();

            if(liveCount <= 1)
            {
                FinishGame();
            }
        }
        public void FinishGame()
        {
            WebSocketRequest request = new WebSocketRequest()
            {
                packetType = PacketType.Game,
                data = StringData.GameOver
            };
            WebSocketManager.instance.SendPacket(JsonUtility.ToJson(request));
            GameOver();
        }
        public void GameOver()
        {
            currentPlayer.followCam.ui.ShowGameOver();
        }
        public void SetCamera()
        {
            GameObject camera = Instantiate(cameraPrefab, map.transform);
            FollowCam followCam = camera.GetComponent<FollowCam>();
            followCam.SetTarget();
        }
        public void SpawnCharacter(int _index)
        {
            GameObject character = Instantiate(characterPrefab);
            character.transform.SetParent(map.transform);
            character.transform.position = spawnPoint[_index].position;

            Character player = character.GetComponent<Character>();
            player.SetInfo(users[_index]);
            players.Add(player);

            if (currentUser.ID == users[_index].ID)
            {
                currentPlayer = player;
                SetCamera();
            }
        }
        public void SyncCharacters(string _data)
        {
            CharacterPacket info = JsonConvert.DeserializeObject<CharacterPacket>(_data);
            Character player = players.Find(x => x.id == info.id);

            if (player == null)
                return;

            player.DoSync(info.hp, info.position, info.rotation, info.animation);
        }
        public void SyncWeapon(string _data)
        {
            WeaponPacket info = JsonConvert.DeserializeObject<WeaponPacket>(_data);
            Character player = players.Find(x => x.id == info.playerID);

            if (player == null)
                return;

            player.attackController.SyncAttack(info.playerID, info.startPos, info.direction);
        }
    }
}