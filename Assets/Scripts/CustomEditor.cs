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
    }
}