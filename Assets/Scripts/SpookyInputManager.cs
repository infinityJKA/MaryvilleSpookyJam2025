using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpookyInputManager : MonoBehaviour
{
    [SerializeField] InputActionReference ref_select, ref_return;
    public GameManager gm;

    void OnEnable()
    {
        ref_select.action.started += action_SELECT;
        ref_return.action.started += action_RETURN;
    }

    void OnDisable()
    {
        ref_select.action.started -= action_SELECT;
        ref_return.action.started -= action_RETURN;
    }

    private void action_SELECT(InputAction.CallbackContext obj)
    {
        if (gm.controlState == ControlState.Dialogue)
        {
            // progress dialogue
            Debug.Log("SELECT (state = dialogue)");
            gm.dialogueManager.RunDialogue();
        }
        else if (gm.controlState == ControlState.Overworld)
        {
            // interact
            Debug.Log("SELECT (state = overworld)");
        }
    }

    private void action_RETURN(InputAction.CallbackContext obj)
    {
        if (gm.controlState == ControlState.Menu)
        {
            // exit menu
            Debug.Log("RETURN (state = menu)");
        }
    }
}
