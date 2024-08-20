using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    public class CharacterShoot : MonoBehaviour
    {
        private Character player;

        Vector3 dir;
        Ray ray;

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
            dir = player.followCam.cam.ScreenPointToRay(Input.mousePosition).direction;
        }
        public void DoShoot()
        {
            ray = new Ray(player.gameObject.transform.position, dir);
            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue))
            {
                Debug.Log($"{hit.transform.name}");
            }
        }
    }
}

