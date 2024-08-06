using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    [RequireComponent(typeof(Character))]
    public class CharacterInput : MonoBehaviour
    {
        private Character player;

        public float moveX;
        public float moveZ;

        public float frontSpeed = 10.0f;
        public float backSpeed = 5.0f;

        public bool isJump = false;
        public float jumpPower = 7.0f;
        public float jumpCooldown = 1.5f;
        public float checkJump;

        private void Start()
        {
            player = GetComponentInParent<Character>();
        }
        private void Update()
        {
            GetInputValues();
        }
        private void FixedUpdate()
        {
            Move();
            if(isJump)
            {
                checkJump -= Time.fixedDeltaTime;
                if (checkJump <= 0)
                    isJump = false;
            }
        }
        private void GetInputValues()
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveZ = Input.GetAxisRaw("Vertical");

            if (!isJump && Input.GetButtonDown("Jump"))
            {
                isJump = true;
                player.rigid.AddForce(0, jumpPower, 0, ForceMode.Impulse);
                checkJump = jumpCooldown;
            }
        }
        private void Move()
        {
            if (moveZ > 0)
                player.transform.Translate(new Vector3(moveX * Time.fixedDeltaTime * frontSpeed, 0, moveZ * Time.fixedDeltaTime * frontSpeed));
            else
                player.transform.Translate(new Vector3(moveX * Time.fixedDeltaTime * backSpeed, 0, moveZ * Time.fixedDeltaTime * backSpeed));
        }
    }
}
