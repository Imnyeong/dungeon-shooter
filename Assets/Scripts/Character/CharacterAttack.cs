using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    [RequireComponent(typeof(Character))]
    public class CharacterAttack : MonoBehaviour
    {
        private Character player;
        private const float camHeight = 2.0f;

        private void Start()
        {
            player = GetComponent<Character>();
            SetCursor();
        }

        public void SetCursor()
        {
            if (player.id == GameManager.instance.currentUser.ID)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        public void DoAttack()
        {
            SyncAttack(player.id, player.transform.position, player.followCam.transform.forward);
            SendPacket();
        }
        private void SendPacket()
        {
            WeaponPacket packet = new WeaponPacket()
            {
                playerID = player.id,
                startPos = player.transform.position,
                direction = player.followCam.transform.forward
            };
            WebSocketRequest request = new WebSocketRequest()
            {
                packetType = PacketType.Weapon,
                data = JsonUtility.ToJson(packet)
            };
            WebSocketManager.instance.SendPacket(JsonUtility.ToJson(request));
        }
        public void SyncAttack(string _id, Vector3 _startPos, Vector3 _dir)
        {
            Vector3 startPos = new Vector3(_startPos.x, _startPos.y + camHeight, _startPos.z);
            Vector3 direction = _dir.normalized;

            Transform weapon = GameManager.instance.objectPool.GetWeapon(0).transform;
            weapon.position = startPos;
            weapon.rotation = Quaternion.FromToRotation(Vector3.down, direction);
            weapon.GetComponent<Weapon>().Shoot(_id, direction);

            player.animController.DoAnimation(AnimationType.Attack);
        }
    }
}

