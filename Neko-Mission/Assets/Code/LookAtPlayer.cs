using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Player Player;

    public void Update()
    {
        transform.LookAt(Player.transform);
    }
}