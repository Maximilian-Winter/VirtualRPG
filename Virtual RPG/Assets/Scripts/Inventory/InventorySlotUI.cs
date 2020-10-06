using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField]
    private Image itemBarImage;

    [SerializeField]
    private Text itemCountText;

    [SerializeField]
    private InventoryItem item;

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

    public int GetItemCount()
    {
        return item.count;
    }

    public void SetItemCount(int value)
    {
        item.count = value;
        itemCountText.text = "x" + item.count;
    }

    void Start()
    {
        if (GetItem() != null)
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
}
