using UnityEngine;

[CreateAssetMenu(menuName = "Wave", fileName = "Wave")]
public class Wave : ScriptableObject
{
    public GameObject[] Enemies;
    public GameObject DefaultEnemy; // Все null в Enemies будут заменены на это значение.
    public float[] Cooldowns;
    public float DefaultCooldown; // Все 0 в Cooldowns будут заменены на это значение.

    public int Length => Enemies.Length;
}
