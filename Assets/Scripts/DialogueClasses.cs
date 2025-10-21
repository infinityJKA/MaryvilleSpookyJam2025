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


}

public class MultichoiceObject
{
    String buttonText;
    String flagName;
}