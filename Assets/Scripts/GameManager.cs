using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] SpookyInputManager inputManager;

    public static GameManager instance;
    public ControlState controlState;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        inputManager.gm = this;
    }

}
