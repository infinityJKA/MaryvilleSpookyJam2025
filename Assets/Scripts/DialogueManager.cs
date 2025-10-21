using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] float textSpeed;

    [Header("UI Objects")]
    public GameObject dialogueCanvas;
    public GameObject dialogueTriangle;
    public TMP_Text textObject;

    [Header("Automatically assigned through code")]
    public int dialogueIndex;
    public List<DialogueLine> currentDialogue;
    public bool dialogueAnimSkipped;
    private GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
    }

    public void StartDialogue(List<DialogueLine> lines, int index)
    {
        dialogueIndex = index;
        currentDialogue = lines;

        gm.controlState = ControlState.Dialogue;

        RunDialogue();
    }

    public void StartDialogue(List<DialogueLine> lines)
    {
        dialogueIndex = -1;
        currentDialogue = lines;
        gm.controlState = ControlState.Dialogue;
        RunDialogue();
    }

    public void RunDialogue() // This is the main function for handeling dialogue stuff
    {
        if (dialogueIndex == -1 || dialogueAnimSkipped || currentDialogue[dialogueIndex].dialogueText == textObject.text) // check if finished writing or skipped before writing again
        {
            dialogueIndex++;
            dialogueAnimSkipped = false;
            DialogueLine line = currentDialogue[dialogueIndex];

            if (line.command != "") // if line is a command
            {
                Debug.Log("DialogueLine is a command");
                PerformCommand(line);
            }
            else // if line is something to print normally
            {
                textObject.text = "";
                dialogueTriangle.SetActive(false);
                StartCoroutine(TypeLine(line.dialogueText));
            }

        }
        else
        {
            Debug.Log("skipped dialogue anim");
            dialogueAnimSkipped = true;
            textObject.text = currentDialogue[dialogueIndex].dialogueText;
            dialogueTriangle.SetActive(true);

        }
    }

    public void PerformCommand(DialogueLine line) // These are the different commands
    {
        if(line.command == "END")
        {
            dialogueCanvas.SetActive(false);
            gm.controlState = ControlState.Overworld;
        }
    }
    
    private IEnumerator TypeLine(String l)
    {
        foreach (char c in l)
        {
            if (dialogueAnimSkipped) { break; }
            textObject.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        dialogueTriangle.SetActive(true);
    }

}
