using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    public class FollowCam : MonoBehaviour
    {
        public Transform player;
        public Camera cam;
        public Canvas aim;

        public float mouseX;
        public float mouseY;

        public float mouseSpeed = 15.0f;
        public float maxY = 60.0f;
        public float minY = -20.0f;

        public float camDistance;
        public float camHeight;

        private const float distanceThree = 3.0f;
        private const float heightThree = 2.0f;

        private const float distanceOne = -1.0f;
        private const float heightOne = 2.0f;

        [SerializeField] private LayerMask wall;

        private void Awake()
        {
            cam = GetComponent<Camera>();
        }
        public void SetTarget(string _id)
        {
            player = GameManager.instance.players.Find(x => x.id == _id).gameObject.transform;
            player.GetComponent<Character>().followCam = this;

            camDistance = distanceThree;
            camHeight = heightThree;
            aim.gameObject.SetActive(false);
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
        public void ChangeView()
        {
            if(camDistance == distanceThree)
            {
                camDistance = distanceOne;
                camHeight = heightOne;
                aim.gameObject.SetActive(true);
            }
            else
            {
                camDistance = distanceThree;
                camHeight = heightThree;
                aim.gameObject.SetActive(false);
            }
        }
    }
}
