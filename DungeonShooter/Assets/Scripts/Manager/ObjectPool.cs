using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DungeonShooter
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject[] weaponPrefabs;
        private List<GameObject>[] weaponPools;

        private void Awake()
        {
            weaponPools = new List<GameObject>[weaponPrefabs.Length];

            for (int key = 0; key < weaponPools.Length; key++)
            {
                weaponPools[key] = new List<GameObject>();
            }
        }
        public GameObject GetWeapon(int _key)
        {
            GameObject result = null;

            foreach (GameObject go in weaponPools[_key])
            {
                if (!go.activeSelf)
                {
                    result = go;
                    result.SetActive(true);
                    break;
                }
            }
            if (result == null)
            {
                result = Instantiate(weaponPrefabs[_key], transform);
                weaponPools[_key].Add(result);
            }
            return result;
        }
    }
}