using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace DungeonShooter
{
    public class FollowUI : MonoBehaviour
    {
        [HideInInspector] public Character player;

        public Canvas canvasAim;
        public Canvas canvasUI;

        [SerializeField] private Slider sliderHP;
        [SerializeField] private Text textNickname;

        [SerializeField] private Canvas canvasGameOver;
        [SerializeField] private Text textGameOver;
        [SerializeField] private Text textShadow;

        private IEnumerator gameOverCoroutine = null;
        private const float waitTime = 3.0f;

        private void Start()
        {
            Init();
        }
        private void LateUpdate()
        {
            if (player == null)
                return;
            CheckHP();
        }
        public void Init()
        {
            textNickname.text = GameManager.instance.currentUser.Nickname;
        }
        public void CheckHP()
        {
            sliderHP.value = (float)player.hp / (float)player.maxHp;
        }
        public void ActiveAim(bool _active)
        {
            canvasAim.gameObject.SetActive(_active);
        }
        public void ShowGameOver()
        {
            textGameOver.text = player.hp > 0 ? StringData.GameWin : StringData.GameLose;
            textShadow.text = player.hp > 0 ? StringData.GameWin : StringData.GameLose;

            canvasGameOver.gameObject.SetActive(true);

            if (gameOverCoroutine != null)
            {
                gameOverCoroutine = null;
            }
            gameOverCoroutine = GameOverCoroutine();
            StartCoroutine(gameOverCoroutine);
        }

        public IEnumerator GameOverCoroutine()
        {
            yield return new WaitForSecondsRealtime(waitTime);
            SceneManager.LoadScene(StringData.SceneLobby);
        }
    }

}
