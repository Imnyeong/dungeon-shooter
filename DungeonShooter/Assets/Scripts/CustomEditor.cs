using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DungeonShooter
{
    public class CustomEditor : EditorWindow
    {
        public int mapSize;
        public int tileSize;
        public GameObject tile;
        public GameObject wall;

        public GameObject furniture;
        public int furnitureCount;

        [MenuItem("Custom/EditorWindow")]
        private static void Init()
        {
            CustomEditor editor = (CustomEditor)GetWindow(typeof(CustomEditor));
            editor.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Map Editor", EditorStyles.boldLabel);
            mapSize = EditorGUILayout.IntField("Map Size", mapSize);
            tileSize = EditorGUILayout.IntField("Tile Size", tileSize);
            tile = (GameObject)EditorGUILayout.ObjectField("Tile", tile, typeof(GameObject), true);
            if (GUILayout.Button("Make Map")) { MakeMap(mapSize, tile); };

            EditorGUILayout.LabelField("Wall Editor", EditorStyles.boldLabel);
            wall = (GameObject)EditorGUILayout.ObjectField("Wall", wall, typeof(GameObject), true);
            if (GUILayout.Button("Make Wall")) { MakeWall(mapSize, wall); };

            EditorGUILayout.LabelField("Furniture Editor", EditorStyles.boldLabel);
            furnitureCount = EditorGUILayout.IntField("Furniture Count", furnitureCount);
            furniture = (GameObject)EditorGUILayout.ObjectField("Furniture", furniture, typeof(GameObject), true);
            if (GUILayout.Button("Make Furniture")) { MakeFurniture(mapSize, furnitureCount); };
        }
        private void MakeMap(int _mapSize, GameObject _tile)
        {
            GameObject floor = new GameObject("Floor");

            int value = _mapSize / 2;

            for (int x = value * -1; x <= value;)
            {
                for (int z = value * -1; z <= value;)
                {
                    int random = Random.Range(0, 4);

                    GameObject tile = Instantiate(_tile, floor.transform);
                    tile.AddComponent<MeshCollider>();
                    tile.transform.localPosition = new Vector3(x, 0.0f, z);
                    tile.transform.localRotation = new Quaternion(0.0f, 90.0f * random, 0.0f, 0.0f);

                    z += tileSize;
                }
                x += tileSize;
            }
        }
        private void MakeWall(int _mapSize, GameObject _tile)
        {
            GameObject wall = new GameObject("Wall");

            int value = _mapSize / 2;
            int side = (_mapSize / 2) + 2;

            for (int x = 1; x <= 4; x++)
            {
                for (int z = value * -1; z <= value;)
                {
                    GameObject tile = Instantiate(_tile, wall.transform);
                    tile.AddComponent<MeshCollider>();
                    switch (x)
                    {
                        case 1:
                            {
                                tile.transform.localPosition = new Vector3(side, 0.0f, z);
                                break;
                            }
                        case 2:
                            {
                                tile.transform.localPosition = new Vector3(z, 0.0f, side);
                                break;
                            }
                        case 3:
                            {
                                tile.transform.localPosition = new Vector3(side * -1, 0.0f, z);
                                break;
                            }
                        case 4:
                            {
                                tile.transform.localPosition = new Vector3(z, 0.0f, side * -1);
                                break;
                            }
                    }

                    tile.transform.localRotation = Quaternion.Euler(0.0f, 90.0f * (x % 2), 0.0f);

                    z += tileSize;
                }
            }
        }
        private void MakeFurniture(int _mapSize, int _count)
        {
            GameObject objects = GameObject.Find("Objects");
            if (objects == null)
            {
                objects = new GameObject("Objects");
            }

            int value = _mapSize / 2;

            for(int i = 0; i < _count; i++)
            {
                int randomX = Random.Range(value * -1, value + 1);
                int randomZ = Random.Range(value * -1, value + 1);
                int rotation = Random.Range(0, 5);

                GameObject obj = Instantiate(furniture, objects.transform);
                obj.AddComponent<MeshCollider>();
                obj.transform.localPosition = new Vector3(randomX, 0.05f, randomZ);
                obj.transform.localRotation = new Quaternion(0.0f, 90.0f * rotation, 0.0f, 0.0f);
            }
        }
    }
}
