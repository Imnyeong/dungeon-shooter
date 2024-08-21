using UnityEngine;
using UnityEngine.SceneManagement;

namespace DungeonShooter
{
    public class DontDestroyObject : MonoBehaviour
    {
        void Awake()
        {
            if (GameObject.FindGameObjectsWithTag(StringData.TagManagers).Length > 1)
            {
                Destroy(this.gameObject);
            }
            else
            {
                DontDestroyOnLoad(this.gameObject);
            }
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if(scene.name == StringData.SceneLobby)
            {
                LobbyCanvas.instance.CheckRoom();
            }
        }
    }
}