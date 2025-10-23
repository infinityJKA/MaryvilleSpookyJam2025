using UnityEngine;
using UnityEngine.SceneManagement;

public class DangerousToMirrored : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collided with layer: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Mirrored"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
