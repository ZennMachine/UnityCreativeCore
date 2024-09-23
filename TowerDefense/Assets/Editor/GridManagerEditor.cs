using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(GridTileMap.TileMapManager))]
public class GridManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GridTileMap.TileMapManager myTileMap = (GridTileMap.TileMapManager)target;

        EditorGUILayout.Space();

        if(EditorApplication.isPlaying)
        {
            if (GUILayout.Button("Generate Terrain Map"))
            {
                myTileMap.BuildTerrain();
            }
        }
    }
}
