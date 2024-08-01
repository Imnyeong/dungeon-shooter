using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public GameObject characterPrefab;
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

        public void SpawnCharacter(int _id)
        {
            GameObject character = Instantiate(characterPrefab, map.transform);
            Character player = character.GetComponent<Character>();
            player.SetInfo(_id);
            players.Add(player);
        }

        public void MoveCharacter(string _data)
        {
            TransformPacket info = JsonUtility.FromJson<TransformPacket>(_data);
            if (currentPlayer == info.id)
                return;

            Character player = players.Find(x => x.id == info.id);
            player.transform.localPosition = new Vector3(info.posX, info.posY, info.posZ);
            player.transform.localRotation = Quaternion.Euler(info.rotX, info.rotY, info.rotZ);
        }
    }
}