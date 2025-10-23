using UnityEngine;
using UnityEngine.SceneManagement;
public class MoveManager : MonoBehaviour
{
    [SerializeField] PlayerObject player;
    [SerializeField] float moveSpeed;
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
        player = Object.FindFirstObjectByType<PlayerObject>();
    }

    public void Move(Vector2 movement)
    {
        Vector3 playerMove = new Vector3(movement.x, 0f, movement.y);
        playerMove.Normalize();
        playerMove = playerMove * moveSpeed;
        player.transform.Translate(playerMove);
    }
}
