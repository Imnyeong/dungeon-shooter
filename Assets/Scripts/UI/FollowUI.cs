using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    }

}
