using System.Collections;
using UnityEngine;

namespace DungeonShooter
{
    [RequireComponent(typeof(Character))]
    public class CharacterInput : MonoBehaviour
    {
        private Character player;

        public float moveX;
        public float moveZ;

        public float frontSpeed = 8.0f;
        public float backSpeed = 5.0f;

        public bool isJump = false;
        public bool isGround = true;
        public float jumpPower = 7.0f;

        public bool canAttack = true;

        private const float attackDelay = 1.0f;
        private void Start()
        {
            player = GetComponent<Character>();
        }
        private void Update()
        {
            GetInputValues();
        }
        private void FixedUpdate()
        {
            Move();
            Jump();
        }
        public void InGround()
        {
            isGround = true;
        }
        private void GetInputValues()
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveZ = Input.GetAxisRaw("Vertical");

            if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonUp(1))
                player.followCam.ChangeView();

            if (Input.GetMouseButtonDown(0) && canAttack)
                OnClickAttack();

            if (Input.GetButtonDown("Jump") && isGround)
                isJump = true;
        }
        private void Move()
        {
            if (moveZ > 0)
                player.transform.Translate(new Vector3(moveX * Time.fixedDeltaTime * frontSpeed, 0, moveZ * Time.fixedDeltaTime * frontSpeed));
            else
                player.transform.Translate(new Vector3(moveX * Time.fixedDeltaTime * backSpeed, 0, moveZ * Time.fixedDeltaTime * backSpeed));
        }
        private void Jump()
        {
            if (isJump && isGround)
            {
                player.animController.DoAnimation(AnimationType.Jump);
                player.rigid.AddForce(0, jumpPower, 0, ForceMode.Impulse);
                isJump = false;
                isGround = false;
            }
        }
        private void OnClickAttack()
        {
            canAttack = false;
            player.attackController.DoAttack();
            StartCoroutine(AttackCoroutine());
        }
        private IEnumerator AttackCoroutine()
        {
            yield return new WaitForSecondsRealtime(attackDelay);
            canAttack = true;
        }
    }
}
