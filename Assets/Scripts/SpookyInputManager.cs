using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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

    void OnDisable()
    {
        ref_select.action.started -= action_SELECT;
        ref_return.action.started -= action_RETURN;
        ref_move.action.Disable();
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
            if (moveValue != Vector2.zero)
            {
                gm.moveManager.Move(moveValue);
                Debug.Log("RETURN (state = overworld)");
            }
        }
    }
}
