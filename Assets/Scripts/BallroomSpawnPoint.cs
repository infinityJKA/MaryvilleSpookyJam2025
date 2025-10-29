using Unity.VisualScripting;
using UnityEngine;

public class BallroomSpawnPoint : MonoBehaviour
{
    void Start()
    {
        GameManager gm = GameManager.instance;
        if (gm.hollyFinished || gm.mirrorFinished || gm.dualityFinished)
        {
            gm.moveManager.player.transform.position = transform.position;
            Debug.Log("Move player to spawn point!");
        }
        else
        {
            Debug.Log("Spawn point: Play hasn't finished anything");
        }
    }
}
