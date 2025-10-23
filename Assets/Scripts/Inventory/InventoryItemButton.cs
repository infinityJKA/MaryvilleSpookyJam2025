using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class InventoryItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text nameText;
    public Image spriteImage;
    public InventoryItem inventoryItem;
    public GameObject isSelectedObject;
    public GameManager gm;

    public void OnPointerEnter(PointerEventData eventData)
    {
        nameText.text = inventoryItem.itemName;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        nameText.text = "";
    }

    public void OnClick()
    {
        if (gm.inventoryManager.currentlySelectedItem == inventoryItem)
        {
            gm.inventoryManager.currentlySelectedItem = null;
            isSelectedObject.SetActive(false);
        }
        else
        {
            if(gm.inventoryManager.currentlySelectedItem != null)
            {
                foreach(Transform t in gm.inventoryManager.inventoryLayoutGroup.transform)
                {
                    if(t.gameObject.GetComponent<InventoryItemButton>().inventoryItem == gm.inventoryManager.currentlySelectedItem)
                    {
                        t.gameObject.GetComponent<InventoryItemButton>().isSelectedObject.SetActive(false);
                    }
                }
            }
            gm.inventoryManager.currentlySelectedItem = inventoryItem;
            isSelectedObject.SetActive(true);
        }
    }

}
