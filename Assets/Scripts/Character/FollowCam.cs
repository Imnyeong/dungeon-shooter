using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DungeonShooter
{
    public class FollowCam : MonoBehaviour
    {
        [HideInInspector] public Character player;
        [HideInInspector] public Transform playerTran;
        public FollowUI ui;

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

        public void SetTarget(string _id)
        {
            player = GameManager.instance.players.Find(x => x.id == _id);
            playerTran = player.gameObject.transform;
            player.followCam = this;

            camDistance = distanceThree;
            camHeight = heightThree;
            ui.ActiveAim(false);
        }
        private void Start()
        {
            SetPlayerUI();
        }
        private void FixedUpdate()
        {
            if (!playerTran.GetComponent<Character>().isLive)
                return;

            GetAxisValues();
        }
        private void LateUpdate()
        {
            MoveCamera();
        }
        public void SetPlayerUI()
        {
            ui.player = player;
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
            playerTran.localRotation = Quaternion.Euler(0, mouseX, 0);

            Vector3 rayDirection = (transform.position - playerTran.position).normalized;
            
            if (Physics.Raycast(playerTran.position, rayDirection, out RaycastHit hit, float.MaxValue, wall))
            {
                float distance = Mathf.Min(Vector3.Distance(playerTran.position, hit.point), camDistance);
                transform.position = new Vector3(playerTran.position.x, playerTran.position.y + camHeight, playerTran.position.z) - transform.forward * distance;
            }
            else
            {
                transform.position = new Vector3(playerTran.position.x, playerTran.position.y + camHeight, playerTran.position.z) - transform.forward * camDistance;
            }
        }
        public void ChangeView()
        {
            if (camDistance == distanceThree)
            {
                camDistance = distanceOne;
                camHeight = heightOne;
                ui.ActiveAim(true);
            }
            else
            {
                camDistance = distanceThree;
                camHeight = heightThree;
                ui.ActiveAim(false);
            }
        }

    }
}
