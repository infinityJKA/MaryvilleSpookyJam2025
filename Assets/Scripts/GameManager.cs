using System;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] SpookyInputManager inputManager;
    public DialogueManager dialogueManager;

    public static GameManager instance;
    public ControlState controlState;

    [Header("TESTING")]
    public List<DialogueLine> testLines;

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

    void Start()
    {
        dialogueManager.StartDialogue(testLines);
    }

}
