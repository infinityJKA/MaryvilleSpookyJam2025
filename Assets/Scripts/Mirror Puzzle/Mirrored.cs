using UnityEngine;

public class Mirrored : MonoBehaviour
{
    public PlayerObject playerObject;

    void Start()
    {
        playerObject = Object.FindFirstObjectByType<PlayerObject>();
        playerObject.mirroedObjects.Add(this.gameObject);
    }


}
