using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MirroredGoal : MonoBehaviour
{
    public List<DialogueLine> dialogue;
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collided with layer: " + collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Mirrored"))
        {
            GameManager.instance.dialogueManager.StartDialogue(dialogue, null);
        }
    }
}
