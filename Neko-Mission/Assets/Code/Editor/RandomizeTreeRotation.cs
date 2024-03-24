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
    
    [MenuItem("Tools/RandomizeTreeModels")]
    private static void RandomizeModels()
    {
         MeshFilter[] meshFilters = Object.FindObjectsOfType<MeshFilter>().Where(go => go.name.Contains("tree")).ToArray();
         Debug.Log($"Changing models for {meshFilters.Length} trees...");


         GameObject[] trees =
         {
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Art/KenneysTowerDefenseKit/Our models/Trees/tree_1.fbx"),
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Art/KenneysTowerDefenseKit/Our models/Trees/tree_2.fbx"),
            AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Art/KenneysTowerDefenseKit/Our models/Trees/tree_3.fbx"),
         };
#if true
         for (int i = 0; i < meshFilters.Length; i++)
         {
             GameObject o = (GameObject)PrefabUtility.InstantiatePrefab(trees[Random.Range(0, trees.Length)]);
             o.transform.position = meshFilters[i].transform.position;
             o.transform.localScale = meshFilters[i].transform.localScale;
         }
#endif
    }
}
