using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
    }

    void OnTriggerEnter(Collider c)
    {
        Debug.Log("Collision detected!");
        // try
        // {
        if (c.gameObject.GetComponent<InteractableObject>() == true) Debug.Log("Has interactableObject");
        else Debug.Log("NO INTERACTABLEOBJECT");
        
        gm.interactableObject = c.gameObject.GetComponent<InteractableObject>();
        Debug.Log("interactableObject added!");
        // }
        // catch
        // {
        //     Debug.LogError("YOU FORGOT TO ADD THE INTERACTABLE OBJECT SCRIPT TO THE OBJECT SO GO DO THAT SO THE GAME WORKS -Justin");
        // }
    }

    void OnTriggerExit(Collider c)
    {
        Debug.Log("OnTriggerExit");
        if(c.gameObject.GetComponent<InteractableObject>() == gm.interactableObject)
        {
            gm.interactableObject = null;
        }
    }
}
