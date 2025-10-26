using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SpookyInputManager : MonoBehaviour
{
    [SerializeField] InputActionReference ref_select, ref_return, ref_move;
    public GameManager gm;

    void OnEnable()
    {
        ref_select.action.started += action_SELECT;
        ref_return.action.started += action_RETURN;
        ref_move.action.Enable();
    }

    //void OnDisable()
    //{
    //    ref_select.action.started -= action_SELECT;
    //    ref_return.action.started -= action_RETURN;
    //    ref_move.action.Disable();
    //    Debug.Log("OnDisable spookyinput");
    //}



    private void action_SELECT(InputAction.CallbackContext obj)
    {
        if (gm.controlState == ControlState.Dialogue)
        {
            gm.dialogueManager.RunDialogue();
        }
        else if (gm.controlState == ControlState.Overworld)
        {
            if (gm.interactableObject != null && gm.interactableObject.canInteract) gm.dialogueManager.StartDialogue(gm.interactableObject.dialogue, gm.interactableObject);   
        }
    }

    private void action_RETURN(InputAction.CallbackContext obj)
    {
        if (gm.controlState == ControlState.InventoryMenu)
        {
            gm.inventoryManager.CloseInventory();
        }
        else if(gm.controlState == ControlState.Overworld)
        {
            gm.inventoryManager.OpenInventory();
        }
    }

    private void Update()
    {
        if (gm.controlState == ControlState.Overworld)
        {  
            Vector2 moveValue = ref_move.action.ReadValue<Vector2>();
            //Debug.Log("moveValue " + moveValue);
            if (moveValue != Vector2.zero)
            {
                gm.moveManager.Move(moveValue);
                //Debug.Log("RETURN (state = overworld)");
            }
            else
            {
                gm.moveManager.StopWalkSound();
            }
        }
    }
}
