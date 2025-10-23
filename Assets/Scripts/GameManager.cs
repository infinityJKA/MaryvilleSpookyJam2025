using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] SpookyInputManager inputManager;
    public DialogueManager dialogueManager;
    public MoveManager moveManager;
    public InventoryManager inventoryManager;

    public static GameManager instance;
    public ControlState controlState;
    public InteractableObject interactableObject;
    
    


    // [Header("TESTING")]
    // public List<DialogueLine> testLines;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this);

        inputManager.gm = this;
    }

    void Start()
    {
        //dialogueManager.StartDialogue(testLines);
    }

}
