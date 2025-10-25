using UnityEngine;

public class PushDetector : MonoBehaviour
{
    public bool hasObject = false;
    public int valids = 0;
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collided with tag: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("PushObject"))
        {
            hasObject = true;
            valids++;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        Debug.Log("Exited with tag: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("PushObject"))
        {
            valids--;
            if(valids == 0) hasObject = false;
        }
    }
}
