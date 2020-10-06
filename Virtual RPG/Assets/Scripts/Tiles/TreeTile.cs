using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;



public class TreeTile : Tile
{
#if UNITY_EDITOR
   
    [MenuItem("Assets/Create/Tiles/TreeTile")]
    public static void CreateTreeTile()
    {
      
        string path = EditorUtility.SaveFilePanelInProject("Save TreeTile", "treeTile", "asset", "Save TreeTile", "Assets");

        if (path == "")
        {
            return;
        }

        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<TreeTile>(), path);
    }
#endif
}
