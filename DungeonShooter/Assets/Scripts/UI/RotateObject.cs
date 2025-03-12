using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    public class RotateObject : MonoBehaviour
    {
        private float yRotate = 0.0f;
        private float rotateSpeed = 3.0f;
        void FixedUpdate()
        {
            yRotate += Time.fixedDeltaTime * rotateSpeed;
            if (yRotate >= 360.0f)
            {
                yRotate = 0.0f;
            }
            transform.rotation = Quaternion.Euler(new Vector3(0.0f, yRotate, 0.0f));
        }
    }

}