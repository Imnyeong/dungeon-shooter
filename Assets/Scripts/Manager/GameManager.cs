using System.Collections;
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

        public int currentPlayer;
        public List<Character> players;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        public void SetCamera(int _id)
        {
            GameObject camera = Instantiate(cameraPrefab, map.transform);
            FollowCam followCam = camera.GetComponent<FollowCam>();
            Debug.Log($"currentPlayer = {currentPlayer}, _id = {_id}");
            followCam.SetTarget(_id);
        }
        public void SpawnCharacter(int _id)
        {
            Debug.Log($"currentPlayer = {currentPlayer}, _id = {_id}");
            GameObject character = Instantiate(characterPrefab, map.transform);
            Character player = character.GetComponent<Character>();

            player.SetInfo(_id);
            players.Add(player);

            if (currentPlayer == _id)
                SetCamera(_id);
        }

        public void MoveCharacter(string _data)
        {
            TransformPacket info = JsonUtility.FromJson<TransformPacket>(_data);

            if (currentPlayer == info.id)
                return;

            Character player = players.Find(x => x.id == info.id);
            player.TransformSync(info.position, info.rotation);
        }
    }
}