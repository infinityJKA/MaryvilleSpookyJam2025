using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public bool mirrorFinished = false, hollyFinished = false, dualityFinished = false;

    [SerializeField] SpookyInputManager inputManager;
    public DialogueManager dialogueManager;
    public MoveManager moveManager;
    public InventoryManager inventoryManager;
    public AudioManager audioManager;

    public Vector3 ballroomSpawnCords;


    public EventSystem eventSystem;

    public static GameManager instance;
    public ControlState controlState;
    public InteractableObject interactableObject;

    public List<DialogueLine> introDialogue;

   




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

        eventSystem.gameObject.SetActive(true);

    }

    void Start()
    {
        //dialogueManager.StartDialogue(testLines);
    }

    public void IntroCutscene()
    {
        Debug.Log("IntroCutscene()");
        dialogueManager.StartDialogue(introDialogue,null);
    }

}
