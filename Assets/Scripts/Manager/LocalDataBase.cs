using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    public class LocalDataBase : MonoBehaviour
    {
        public static LocalDataBase instance;
        public UserData userData;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
