using System.Linq;
using UnityEditor;
using UnityEngine;

public class RandomizeTreeRotation : MonoBehaviour
{
    [MenuItem("Tools/RandomizeTreeRotation")]
    private static void RandomizeRotation()
    {
         MeshFilter[] meshFilters = Object.FindObjectsOfType<MeshFilter>().Where(go => go.name.Contains("tree")).ToArray();
         Debug.Log($"Rotating {meshFilters.Length} trees...");

         for (int i = 0; i < meshFilters.Length; i++)
         {
             Transform trasform = meshFilters[i].transform;
             trasform.rotation = Quaternion.Euler(0, Mathf.Round(Random.Range(-180f, 180f)), 0);
         }
    }
}
