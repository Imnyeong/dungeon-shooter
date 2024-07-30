using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public GameObject characterPrefab;
        public GameObject dungeon;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }

        public void SpawnCharacter()
        {
            Debug.Log("Check Point");
            GameObject character = Instantiate(characterPrefab, dungeon.transform);
            character.transform.localPosition = new Vector3(0.0f, 1.0f, 0.0f);
        }
        void Update()
        {

        }
    }
}