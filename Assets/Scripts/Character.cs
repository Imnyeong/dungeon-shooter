using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    public class Character : MonoBehaviour
    {
        public Collider col;
        public Rigidbody rigid;
        public Animator animator;
        public Camera characterCamera;

        public int id { get; private set; }
        public int hp { get; private set; }
        public float moveSpeed = 10.0f;
        public float mouseSpeed = 15.0f;

        public float moveX;
        public float moveZ;

        public float mouseX;
        //public float mouseY;
        //public float maxX = 35.0f;
        //public float minX = -35.0f;

        private void Update()
        {
            if (id != GameManager.instance.currentPlayer)
                return;

            GetAxisValues();
        }
        private void FixedUpdate()
        {
            if (id != GameManager.instance.currentPlayer)
                return;

            MouseRotate();
            Move();
        }
        private void LateUpdate()
        {
            if (id != GameManager.instance.currentPlayer)
                return;

            if (moveX != 0 || moveZ != 0)
                WebSocketSync();
        }
        public void SetInfo(int _id)
        {
            id = _id;
            transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        }
        private void GetAxisValues()
        {
            moveX = Input.GetAxisRaw("Horizontal");
            moveZ = Input.GetAxisRaw("Vertical");

            mouseX += Input.GetAxisRaw("Mouse X");
            //mouseY += Input.GetAxisRaw("Mouse Y");
        }

        private void Move()
        {
            //Vector3 moveDir = new Vector3(moveX, 0.0f, moveZ);
            //Vector3 moveVector = moveDir.normalized * Time.fixedDeltaTime * moveSpeed;
            //rigid.MovePosition(rigid.position + moveVector);
            transform.Translate(moveZ * Vector3.forward * Time.fixedDeltaTime * moveSpeed);
            transform.Translate(moveX * Vector3.right * Time.fixedDeltaTime * moveSpeed);
        }
        private void MouseRotate()
        {
            //mouseY = Mathf.Clamp(mouseY, -45f, 45f);
            //characterCamera.transform.localRotation = Quaternion.Euler(mouseY, mouseX, 0);
            transform.localRotation = Quaternion.Euler(0, mouseX, 0);
            //characterCamera.transform.localRotation = Quaternion.Euler(mouseY, 0, 0);
            //transform.Rotate(0, mouseX * mouseSpeed, 0);
        }
        private void WebSocketSync()
        {
            TransformPacket packet = new TransformPacket() 
            {
                id = id,
                posX = transform.localPosition.x,
                posY = transform.localPosition.y,
                posZ = transform.localPosition.z,
                rotX = transform.eulerAngles.x,
                rotY = transform.eulerAngles.y,
                rotZ = transform.eulerAngles.z,
            };
            WebSocketManager.instance.SendPacket(JsonUtility.ToJson(packet));
        }
    }
}