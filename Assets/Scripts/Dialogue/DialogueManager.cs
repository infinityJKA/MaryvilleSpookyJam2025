using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] float textSpeed;

    [Header("UI Objects")]
    public GameObject dialogueCanvas;
    public GameObject dialogueTriangle, dialogueBox;
    public TMP_Text textObject;
    public GameObject multichoiceParent;
    public MultichoiceButton multichoiceButtonPrefab;

    [Header("Automatically assigned through code")]
    public int dialogueIndex;
    public List<DialogueLine> currentDialogue;
    public bool dialogueAnimSkipped, usedCommand;
    private GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
    }

    public void StartDialogue(List<DialogueLine> lines, int index)
    {
        dialogueIndex = index;
        currentDialogue = lines;

        gm.controlState = ControlState.NoControls;

        multichoiceParent.SetActive(false);
        dialogueCanvas.SetActive(true);

        RunDialogue();
    }

    public void StartDialogue(List<DialogueLine> lines)
    {
        dialogueIndex = -1;
        currentDialogue = lines;
        
        gm.controlState = ControlState.NoControls;

        multichoiceParent.SetActive(false);
        dialogueCanvas.SetActive(true);
        dialogueBox.SetActive(false);

        usedCommand = false;
        dialogueAnimSkipped = false;

        RunDialogue();
    }

    public void RunDialogue() // This is the main function for handeling dialogue stuff
    {
        Debug.Log("RunDialogue with index of " + dialogueIndex);
        if (dialogueIndex == -1 || usedCommand || dialogueAnimSkipped || currentDialogue[dialogueIndex].dialogueText == textObject.text) // check if finished writing or skipped before writing again
        {
            gm.controlState = ControlState.NoControls;
            dialogueIndex++;
            dialogueAnimSkipped = false;
            usedCommand = false;
            DialogueLine line = currentDialogue[dialogueIndex];

            if (line.command != "") // if line is a command
            {
                Debug.Log("DialogueLine is a command");
                PerformCommand(line);
            }
            else // if line is something to print normally
            {
                textObject.text = "";
                dialogueBox.SetActive(true);
                dialogueTriangle.SetActive(false);
                gm.controlState = ControlState.Dialogue;
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
        usedCommand = true;
        if (line.command == "END")
        {
            dialogueCanvas.SetActive(false);
            gm.controlState = ControlState.Overworld;
        }
        else if (line.command == "SENDTO")
        {
            GoToFlag(line.dialogueText);
        }
        else if (line.command == "MULTI")
        {
            Debug.Log("MULTI");

            while (multichoiceParent.transform.childCount > 0) DestroyImmediate(multichoiceParent.transform.GetChild(0).gameObject);

            foreach (MultichoiceObject m in line.multichoice)
            {
                GameObject obj = Instantiate(multichoiceButtonPrefab.gameObject, multichoiceParent.transform);
                MultichoiceButton b = obj.GetComponent<MultichoiceButton>();
                b.tmp.text = m.buttonText;
                b.flag = m.flagName;
            }

            multichoiceParent.SetActive(true);
        }
        else if(line.command == "FLAG")
        {
            Debug.Log("Flag");
            RunDialogue();
        }
        else if(line.command == "SCENE")
        {
            SceneManager.LoadScene(line.dialogueText);
            RunDialogue();
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
    
    public void GoToFlag(String flag)
    {
        Debug.Log("GoToFlag("+flag+")");
        multichoiceParent.SetActive(false);

        bool found = false;
        int ind = 0;
        while (!found)
        {
            if(ind >= currentDialogue.Count)
            {
                Debug.Log("FLAG NOT FOUND");
                break;
            }

            if (currentDialogue[ind].command == "FLAG" && currentDialogue[ind].dialogueText == flag)
            {
                found = true;
            }
            ind++;
        }

        dialogueIndex = ind - 1;
        RunDialogue();
    }

}
