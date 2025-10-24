using UnityEngine;
using UnityEngine.SceneManagement;
public class MoveManager : MonoBehaviour
{
    [SerializeField] PlayerObject player;
    [SerializeField] float moveSpeed;
    [SerializeField] Camera freelookCamera;
    private GameManager gm;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        player = Object.FindFirstObjectByType<PlayerObject>();
    }

    void Start()
    {
        gm = GameManager.instance;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("MoveManager OnSceneLoaded()");
        player = Object.FindFirstObjectByType<PlayerObject>();
    }

    public void Move(Vector2 movement)
    {
        Vector3 playerMove = new Vector3(movement.x, 0f, movement.y);
        Vector3 screenForward = new Vector3(freelookCamera.transform.forward.x, 0, freelookCamera.transform.forward.z);

        Quaternion targetRotation = Quaternion.LookRotation(screenForward);
        targetRotation *= Quaternion.LookRotation(playerMove);
        player.transform.rotation = targetRotation;

        if(player.mirroedObjects.Count > 0)
        {
            foreach(GameObject m in player.mirroedObjects)
            {
                m.transform.rotation = targetRotation;
            }
        }

        Debug.Log("targetRotation: " + targetRotation);

        //playerMove.Normalize();
        //playerMove = playerMove * moveSpeed;
        playerMove = Vector3.forward * moveSpeed;
        //Vector3 screenForward = new Vector3(freelookCamera.transform.forward.x, 0, freelookCamera.transform.forward.z);
        //player.transform.forward = screenForward;
        //playerMove = screenForward * moveSpeed;
        player.transform.Translate(playerMove);

        if (player.mirroedObjects.Count > 0)
        {
            foreach (GameObject m in player.mirroedObjects)
            {
                m.transform.Translate(playerMove*-1);
            }
        }
    }
}
