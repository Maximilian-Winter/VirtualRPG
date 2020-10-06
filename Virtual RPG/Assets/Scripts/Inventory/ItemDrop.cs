using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrop : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private GameObject itemDropDialogue;

    [SerializeField]
    private InventoryController inventoryController;

    private InventoryItem item;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ShowItemDropDialogue()
    {
        itemDropDialogue.SetActive(true);
    }

    public void HideItemDropDialogue()
    {
        itemDropDialogue.SetActive(false);
    }

    public void RemoveItemDropped()
    {
        if (item != null)
        {
            inventoryController.RemoveInventoryItem(item, 1);
        }
        itemDropDialogue.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<InventorySlotUI>() != null)
            {
                item = eventData.pointerDrag.GetComponent<InventorySlotUI>().GetItem();
                ShowItemDropDialogue();
            }
        }
    }

   
}
