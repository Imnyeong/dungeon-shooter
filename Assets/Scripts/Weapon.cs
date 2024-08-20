using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Rigidbody rigid;
        [SerializeField] private Collider col;
        [SerializeField] private const float atkSpeed = 50.0f;

        public void Shoot(Vector3 _direction)
        {
            rigid.velocity = _direction * atkSpeed;
        }
        public void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag != StringData.TagPlayer)
                this.gameObject.SetActive(false);
        }
    }
}

