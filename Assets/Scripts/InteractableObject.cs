using System;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public List<DialogueLine> dialogue;
    [Header("Place puzzle stuff")]
    public InventoryItem placedItem;
    public GameObject placedObject, placeObjectPoint;

    [Header("Push puzzle stuff")]
    public Vector3 moveTo;
    public bool canInteract = true, moving = false;
    public float speed;

    void Update()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, moveTo, speed * Time.deltaTime);
            if (transform.position == moveTo)
            {
                moving = false;
                canInteract = true;
            }
        }
    }

}
