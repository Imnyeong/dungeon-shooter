using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    public class FollowCam : MonoBehaviour
    {
        public Transform character;

        public float mouseX;
        public float mouseY;

        public float mouseSpeed = 15.0f;
        public float maxY = 60.0f;
        public float minY = -20.0f;
        public float camDistance = 4.0f;
        public float camHeight = 2.0f;

        public void SetTarget(int _id)
        {
            character = GameManager.instance.players.Find(x => x.id == _id).gameObject.transform;
        }
        private void FixedUpdate()
        {
            GetAxisValues();
        }
        private void LateUpdate()
        {
            MoveCamera();
        }
        private void GetAxisValues()
        {
            mouseX += Input.GetAxisRaw("Mouse X") * mouseSpeed;
            mouseY -= Input.GetAxisRaw("Mouse Y") * mouseSpeed;
            mouseY = Mathf.Clamp(mouseY, minY, maxY);
        }
        private void MoveCamera()
        {
            transform.eulerAngles = new Vector3(mouseY, mouseX, transform.rotation.z);
            character.localRotation = Quaternion.Euler(0, mouseX, 0);
            transform.position =
                new Vector3(character.position.x, character.position.y + camHeight, character.position.z) - transform.forward * camDistance;
        }
    }
}
