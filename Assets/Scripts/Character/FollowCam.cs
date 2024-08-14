using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    public class FollowCam : MonoBehaviour
    {
        public Transform player;

        public float mouseX;
        public float mouseY;

        public float mouseSpeed = 15.0f;
        public float maxY = 60.0f;
        public float minY = -20.0f;
        public float camDistance = 3.0f;
        public float camHeight = 3.0f;

        [SerializeField] private LayerMask wall;

        public void SetTarget(string _id)
        {
            player = GameManager.instance.players.Find(x => x.id == _id).gameObject.transform;
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
            player.localRotation = Quaternion.Euler(0, mouseX, 0);

            Vector3 rayDirection = (transform.position - player.position).normalized;
            
            if (Physics.Raycast(player.position, rayDirection, out RaycastHit hit, float.MaxValue, wall))
            {
                float distance = Mathf.Min(Vector3.Distance(player.position, hit.point), camDistance);
                transform.position = new Vector3(player.position.x, player.position.y + camHeight, player.position.z) - transform.forward * distance;
            }
            else
            {
                transform.position = new Vector3(player.position.x, player.position.y + camHeight, player.position.z) - transform.forward * camDistance;
            }
        }
    }
}
