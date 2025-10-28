using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueLine
{
    [TextArea(2, 10)]
    public String command = "",
    dialogueText;

    public GameObject commandObject; // object that may be called by a command

    public List<MultichoiceObject> multichoice;
    public SoundObj sfxToPlay;
    public InventoryItem item;


    public DialogueLine(String text)
    {
        this.dialogueText = text;
    }

}

[Serializable]
public class MultichoiceObject
{
    public String buttonText;
    public String flagName;
}