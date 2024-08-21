using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    public class Weapon : MonoBehaviour
    {
        public string playerID = string.Empty;
        public int atkDamage = 10;

        [SerializeField] private Rigidbody rigid;
        [SerializeField] private Collider col;
        [SerializeField] private const float atkSpeed = 50.0f;

        public void Shoot(string _id, Vector3 _direction)
        {
            playerID = _id;
            rigid.velocity = _direction * atkSpeed;
        }
        public void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag != StringData.TagPlayer)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                other.GetComponent<Character>().HitWeapon(this);
            }
        }
    }
}

