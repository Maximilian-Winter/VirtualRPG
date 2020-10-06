using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemBarSlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private GameObject itemBarParent;

    [SerializeField]
    private Image itemBarImage;

    [SerializeField]
    private GameObject itemBarSelector;

    private InventoryItem item;

    void Start()
    {
        if(GetItem() != null)
        {
            itemBarImage.sprite = GetItem().item.itemSprite;
            itemBarImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else
        {
            itemBarImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public InventoryItem GetItem()
    {
        return item;
    }

    public void SetItem(InventoryItem value)
    {
        item = value;
        itemBarImage.sprite = GetItem().item.itemSprite;
        itemBarImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    public void RemoveItem()
    {
        item = null;
        itemBarImage.sprite = null;
        itemBarImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f); 
    }

    public void ActivateSelector()
    {
        itemBarSelector.SetActive(true);
    }

    public void DeactivateSelector()
    {
        itemBarSelector.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<InventorySlotUI>() != null)
            {
                SetItem(eventData.pointerDrag.GetComponent<InventorySlotUI>().GetItem());
            }
            if (eventData.pointerDrag.GetComponent<ItemBarSlot>() != null)
            {
                SetItem(eventData.pointerDrag.GetComponent<ItemBarSlot>().GetItem());
            }
        }
    }
}
