using UnityEngine;

namespace DungeonShooter
{
    public class DontDestroyObject : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}