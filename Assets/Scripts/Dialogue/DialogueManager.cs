using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
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
    public GameObject inputParent;
    public TMP_InputField inputField;
    public Image blackBg;
    public GameManager gm;

    [Header("Automatically assigned through code")]
    public int dialogueIndex;
    public List<DialogueLine> currentDialogue;
    public bool dialogueAnimSkipped, usedCommand;
    public InteractableObject currentInteractableObject;
    public String commandDialogueText;
    public String correctCode;
    
    void Start()
    {
        
    }

    public void StartDialogue(List<DialogueLine> lines, int index, InteractableObject currentInteractableObject)
    {
        dialogueIndex = index;
        currentDialogue = lines;
        this.currentInteractableObject = currentInteractableObject;

        gm.controlState = ControlState.NoControls;

        multichoiceParent.SetActive(false);
        dialogueCanvas.SetActive(true);

        RunDialogue();
    }

    public void StartDialogue(List<DialogueLine> lines, InteractableObject currentInteractableObject)
    {
        Debug.Log("StartDialogue()");

        dialogueIndex = -1;
        currentDialogue = lines;
        this.currentInteractableObject = currentInteractableObject;
        
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
        //Debug.Log("RunDialogue with index of " + dialogueIndex);
        if (dialogueIndex == -1 || usedCommand || dialogueAnimSkipped || currentDialogue[dialogueIndex].dialogueText == textObject.text || textObject.text == commandDialogueText) // check if finished writing or skipped before writing again
        {
            gm.controlState = ControlState.NoControls;
            commandDialogueText = "no commandDialogueText";
            dialogueIndex++;
            dialogueAnimSkipped = false;
            usedCommand = false;
            DialogueLine line = currentDialogue[dialogueIndex];

            if (line.command != "") // if line is a command
            {
                //Debug.Log("DialogueLine is a command");
                PerformCommand(line);

            }
            else // if line is something to print normally
            {
                //gm.audioManager.PlaySfx("click");
                PrintNormally(line);
            }

        }
        else
        {
            Debug.Log("skipped dialogue anim");
            dialogueAnimSkipped = true;

            if (commandDialogueText != "no commandDialogueText") textObject.text = commandDialogueText;
            else textObject.text = currentDialogue[dialogueIndex].dialogueText;
            
            dialogueTriangle.SetActive(true);

        }
    }

    public void PrintNormally(DialogueLine line)
    {
        textObject.text = "";
        if (usedCommand)
        {
            commandDialogueText = line.dialogueText;
            usedCommand = false;
        }
        dialogueBox.SetActive(true);
        dialogueTriangle.SetActive(false);
        gm.controlState = ControlState.Dialogue;
        Debug.Log("Print normally: " + line.dialogueText);
        StartCoroutine(TypeLine(line.dialogueText));
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

            //gm.eventSystem.SetSelectedGameObject(multichoiceParent.transform.GetChild(0).gameObject);

            multichoiceParent.SetActive(true);
        }
        else if (line.command == "FLAG")
        {
            Debug.Log("Flag");
            RunDialogue();
        }
        else if (line.command == "SCENE")
        {
            SceneManager.LoadScene(line.dialogueText);
            RunDialogue();
        }
        else if (line.command == "PUSH")
        {
            Transform obj = line.commandObject.transform;
            Transform plyr = gm.moveManager.player.gameObject.transform;
            float x = plyr.position.x - obj.position.x;
            float y = plyr.position.z - obj.position.z;
            Vector3 moveTo;
            float moveAmount = 3f;
            if (math.abs(x) > math.abs(y))
            {
                if (x > 0) moveTo = obj.position + Vector3.left * moveAmount;
                else moveTo = obj.position + Vector3.right * moveAmount;
            }
            else
            {
                if (y > 0) moveTo = obj.position + Vector3.back * moveAmount;
                else moveTo = obj.position + Vector3.forward * moveAmount;
            }

            InteractableObject intr = obj.transform.GetComponent<InteractableObject>();
            intr.moveTo = moveTo;
            intr.canInteract = false;
            intr.moving = true;


            RunDialogue();
        }
        else if (line.command == "IF PLACED")
        {
            if (currentInteractableObject.placedItem != null)
            {
                GoToFlag(line.dialogueText);
            }
            else
            {
                RunDialogue();
            }
        }
        else if (line.command == "PLACE EQUIPPED")
        {
            if (gm.inventoryManager.currentlySelectedItem == null)
            {
                PrintNormally(new DialogueLine("[You need to equip an item to place it...]"));
            }
            else if (gm.inventoryManager.currentlySelectedItem.canPlaced == false)
            {
                PrintNormally(new DialogueLine("[This item can't be placed...]"));
            }
            else
            {
                String n = gm.inventoryManager.currentlySelectedItem.itemName;

                currentInteractableObject.placedItem = gm.inventoryManager.currentlySelectedItem;
                currentInteractableObject.placedObject = Instantiate(gm.inventoryManager.currentlySelectedItem.objectWhenPlaced, currentInteractableObject.placeObjectPoint.transform);

                gm.inventoryManager.inventory.Remove(gm.inventoryManager.currentlySelectedItem);
                gm.inventoryManager.currentlySelectedItem = null;

                PrintNormally(new DialogueLine("[You placed down " + n + "]"));
            }
        }
        else if (line.command == "READ PLACED")
        {
            if (currentInteractableObject.placedItem != null)
            {
                PrintNormally(new DialogueLine(currentInteractableObject.placedItem.placedInteractLine));
            }
            else
            {
                RunDialogue();
            }
        }
        else if (line.command == "REMOVE PLACED")
        {
            Debug.Log("REMOVE PLACED");

            String n = currentInteractableObject.placedItem.itemName;

            gm.inventoryManager.inventory.Add(currentInteractableObject.placedItem);
            Destroy(currentInteractableObject.placedObject);
            currentInteractableObject.placedItem = null;

            PrintNormally(new DialogueLine("[You picked up " + n + "]"));
        }
        else if (line.command == "GET ITEM")
        {
            gm.inventoryManager.inventory.Add(currentInteractableObject.placedItem);
            RunDialogue();
        }
        else if (line.command == "DESTROY")
        {
            Destroy(currentInteractableObject.gameObject);
            dialogueCanvas.SetActive(false);
            gm.controlState = ControlState.Overworld;
        }
        else if (line.command == "ENTER CODE")
        {
            inputField.text = "";
            correctCode = line.dialogueText;
            inputParent.SetActive(true);
        }
        else if (line.command == "ENABLE BLACK")
        {
            StartCoroutine(FadeInBlack());
        }
        else if (line.command == "DISABLE BLACK")
        {
            StartCoroutine(FadeOutBlack());
        }
        else if(line.command == "WAIT")
        {
            StartCoroutine(DialogueWait(int.Parse(line.dialogueText)));
        }
        else if(line.command == "IF ALL COMPLETE")
        {
            if (gm.mirrorFinished && gm.hollyFinished && gm.dualityFinished)
            {
                GoToFlag(line.dialogueText);
            }
            else RunDialogue();
        }
        else if(line.command == "IF MIRROR FINISHED")
        {
            if (gm.mirrorFinished) GoToFlag(line.dialogueText);
            else RunDialogue();
        }
        else if (line.command == "IF HOLLY FINISHED")
        {
            if (gm.hollyFinished) GoToFlag(line.dialogueText);
            else RunDialogue();
        }
        else if (line.command == "IF DUALITY FINISHED")
        {
            if (gm.dualityFinished) GoToFlag(line.dialogueText);
            else RunDialogue();
        }
        else if (line.command == "FINISHED PUZZLE")
        {
            if (line.dialogueText == "holly") gm.hollyFinished = true;
            else if (line.dialogueText == "mirror") gm.mirrorFinished = true;
            else if (line.dialogueText == "duality") gm.dualityFinished = true;

            RunDialogue();
        }
        else if (line.command == "BALLROOM")
        {
            SceneManager.LoadScene("Ballroom");
            DialogueWait(1f);

            gm.moveManager.player.transform.position = gm.ballroomSpawnCords;


            RunDialogue();
        }
        else if (line.command == "ADD ITEM")
        {
            gm.inventoryManager.inventory.Add(line.item);
            RunDialogue();
        }

    }

    private IEnumerator DialogueWait(float t)
    {
        yield return new WaitForSeconds(t);
        RunDialogue();
    }

    private IEnumerator FadeInBlack()
    {
        Debug.Log("FadeInBlack");
        while (blackBg.color.a < 1)
        {
            Debug.Log("BlackBg.color.a == " + blackBg.color.a);
            var tempColor = blackBg.color;
            tempColor.a += 0.03f;
            blackBg.color = tempColor;
            yield return new WaitForSeconds(0.07f);
        }
        RunDialogue();
    }
    
    private IEnumerator FadeOutBlack()
    {
        while (blackBg.color.a > 0)
        {
            var tempColor = blackBg.color;
            tempColor.a -= 0.03f;
            blackBg.color = tempColor;
            yield return new WaitForSeconds(0.07f);
        }
        RunDialogue();
    }

    private IEnumerator TypeLine(String l)
    {
        foreach (char c in l)
        {
            if (dialogueAnimSkipped) { break; }
            textObject.text += c;
            if(c != ' ') gm.audioManager.PlaySfxRandomPitch("shortClick");
            yield return new WaitForSeconds(textSpeed);
        }
        dialogueTriangle.SetActive(true);
    }

    public void GoToFlag(String flag)
    {
        Debug.Log("GoToFlag(" + flag + ")");
        multichoiceParent.SetActive(false);

        bool found = false;
        int ind = 0;
        while (!found)
        {
            if (ind >= currentDialogue.Count)
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
    
    public void CodeButtonPress()
    {
        inputParent.SetActive(false);

        Debug.Log("code entered is " + inputField.text);
        Debug.Log("correct code is " + correctCode);

        if (inputField.text == correctCode)
        {
            Debug.Log("code is correct");
            GoToFlag("correct code");
        }
        else
        {
            Debug.Log("code is wrong");
            GoToFlag("wrong code");
        }
    }

}
