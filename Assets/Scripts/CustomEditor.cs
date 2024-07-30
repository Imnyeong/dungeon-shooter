using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomEditor : EditorWindow
{ 
    public int mapSize;
    public int tileSize;
    public GameObject baseTile;
    public GameObject additionalTile;

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
        baseTile = (GameObject)EditorGUILayout.ObjectField("Base Tile", baseTile, typeof(GameObject), true);
        additionalTile = (GameObject)EditorGUILayout.ObjectField("Additional Tile", additionalTile, typeof(GameObject), true);
        if (GUILayout.Button("Make Map")) { MakeMap(mapSize, baseTile, additionalTile); };
    }
    private void MakeMap(int _mapSize, GameObject _baseTile, GameObject _additionalTile)
    {
        GameObject dungeon = Instantiate(new GameObject("Dungeon"));

        int value = _mapSize / 2;

        for (int x = value * -1; x <= value;)
        {
            for(int z = value * -1; z <= value;)
            {
                GameObject tile = Instantiate(_baseTile, dungeon.transform);
                tile.transform.localPosition = new Vector3(x, 0.0f, z);
                z += tileSize;
            }
            x += tileSize;
        }
    }
}