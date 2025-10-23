using System;
using TMPro;
using UnityEngine;

public class MultichoiceButton : MonoBehaviour
{
    public String flag;
    public TMP_Text tmp;
    public GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
    }

    public void OnClick()
    {
        Debug.Log("OnClick()");
        gm.dialogueManager.GoToFlag(flag);
    }
}
