using UnityEditor;
using UnityEngine;

public static class AlignTiles
{
    [MenuItem("Tools/Align")]
    public static void Align()
    {
         MeshFilter[] meshFilters = Object.FindObjectsOfType<MeshFilter>();

         for (int i = 0; i < meshFilters.Length; i++)
         {
             Transform transform = meshFilters[i].transform;
             Vector3 position = transform.position;
             position.x = (int)Mathf.Round(position.x);
             position.z = (int)Mathf.Round(position.z);
             transform.position = position;
         }
    }
}
