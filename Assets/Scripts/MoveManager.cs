using UnityEngine;

public class MoveManager : MonoBehaviour
{
    [SerializeField] CapsuleCollider player;
    [SerializeField] float moveSpeed;
    private GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
    }

    public void Move(Vector2 movement)
    {
        Vector3 playerMove = new Vector3(movement.x, 0f, movement.y);
        playerMove.Normalize();
        playerMove = playerMove * moveSpeed;
        player.transform.Translate(playerMove);
    }
}
