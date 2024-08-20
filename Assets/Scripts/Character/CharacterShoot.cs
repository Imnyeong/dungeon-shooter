using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    public class CharacterShoot : MonoBehaviour
    {
        private Character player;
        private const float camHeight = 2.0f;
        Vector3 dir;

        private void Start()
        {
            player = GetComponent<Character>();
            SetCursor();
        }
        private void Update()
        {
            if (player.followCam != null)
                SetDirection();
        }
        public void SetCursor()
        {
            if (player.id == GameManager.instance.currentPlayer)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        public void SetDirection()
        {
            dir = player.followCam.transform.forward;
        }
        public void DoShoot()
        {
            Vector3 startPos = new Vector3(player.gameObject.transform.position.x, player.gameObject.transform.position.y + camHeight, player.gameObject.transform.position.z);
            Vector3 direction = dir.normalized;

            Transform weapon = GameManager.instance.objectPool.GetWeapon(player.weaponId).transform;
            weapon.position = startPos;
            weapon.rotation = Quaternion.FromToRotation(Vector3.up, direction);
            weapon.GetComponent<Weapon>().Shoot(direction);
        }
    }
}

