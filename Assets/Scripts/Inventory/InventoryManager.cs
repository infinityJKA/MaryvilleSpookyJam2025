using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public GameManager gm;
    public List<InventoryItem> inventory;
    public InventoryItem currentlySelectedItem;
    public InventoryItemButton buttonPrefab;

    public GameObject inventoryCanvas, inventoryLayoutGroup;
    public TMP_Text itemNameText;

    void Start()
    {
        
    }

    public void OpenInventory()
    {
        gm.controlState = ControlState.NoControls;

        Debug.Log("inventory layout group exists = " + (inventoryLayoutGroup.gameObject != null));
        if (inventoryLayoutGroup.gameObject == null)
        {
            gm.controlState = ControlState.Overworld;
            return;
        }

        if (inventoryLayoutGroup.transform.childCount > 0)
        {
            while (inventoryLayoutGroup.transform.childCount > 0)
            {
                if (inventoryLayoutGroup.transform.GetChild(0).gameObject != null)
                {
                    DestroyImmediate(inventoryLayoutGroup.transform.GetChild(0).gameObject);
                }
            }
        }

        foreach (InventoryItem i in inventory)
        {
            GameObject obj = Instantiate(buttonPrefab.gameObject, inventoryLayoutGroup.transform);
            InventoryItemButton b = obj.GetComponent<InventoryItemButton>();

            b.nameText = itemNameText;
            b.spriteImage.sprite = i.sprite;
            b.inventoryItem = i;
            if (i == currentlySelectedItem) b.isSelectedObject.SetActive(true);
            else b.isSelectedObject.SetActive(false);
            b.gm = gm;
        }

        inventoryCanvas.SetActive(true);

        gm.controlState = ControlState.InventoryMenu;
        
        

        
    }

    public void CloseInventory()
    {
        inventoryCanvas.SetActive(false);
        gm.controlState = ControlState.Overworld;
    }

}
