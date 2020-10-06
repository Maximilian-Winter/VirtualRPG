using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField]
    private Inventory inventory;

    [SerializeField]
    private GameObject inventoryGameObject;

    [SerializeField]
    private GameObject inventoryUIContainer;

    [SerializeField]
    private GameObject inventorySlotPrefab;

    [SerializeField]
    private ItemTooltip itemTooltip;

    [SerializeField]
    private bool inventoryIsOpen;

    private List<GameObject> tempInventoryItems;

    public bool InventoryIsOpen { get => inventoryIsOpen; set => inventoryIsOpen = value; }

    // Start is called before the first frame update
    void Start()
    {
        tempInventoryItems = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        int removedItemCount = inventory.Items.RemoveAll(s => s.count == 0);
        if(InventoryIsOpen && removedItemCount > 0)
        {
            UpdateInventory();
        }
    }

    public void RemoveInventoryItem(InventoryItem item, int count)
    {
        foreach (InventoryItem i in inventory.Items)
        {
            if (i == item)
            {
                i.count -= count;
                if (InventoryIsOpen)
                {
                    UpdateInventory();
                }
                break;
            }
        }
    }

    public void RemoveInventoryItem(BaseItem item, int count)
    {
        foreach (InventoryItem i in inventory.Items)
        {
            if(i.item == item)
            {
                i.count -= count;
                if (InventoryIsOpen)
                {
                    UpdateInventory();
                }
                break;
            }
        }
    }

    public void AddInventoryItem(BaseItem item, int count)
    {
        foreach (InventoryItem i in inventory.Items)
        {
            if (i.item == item)
            {
                i.count += count;
                if (InventoryIsOpen)
                {
                    UpdateInventory();
                }
                break;
            }
        }
    }


    public int GetItemCount(BaseItem item)
    {
        foreach (InventoryItem i in inventory.Items)
        {
            if (i.item == item)
            {
                return i.count;
            }
        }

        return 0;
    }


    public void ToggleInventory()
    {
        if(!InventoryIsOpen)
        {
            OpenInventory();
        }
        else
        {
            CloseInventory();
        }
    }

    public void OpenInventory()
    {
        inventoryGameObject.SetActive(true);
        foreach (InventoryItem item in inventory.Items)
        {
            GameObject itemUI = Instantiate(inventorySlotPrefab, inventoryUIContainer.transform);
            itemUI.GetComponent<InventorySlotUI>().SetItem(item);
            itemUI.GetComponent<InventorySlotUI>().SetItemCount(item.count);
            tempInventoryItems.Add(itemUI);
        }
        InventoryIsOpen = true;
    }

    public void CloseInventory()
    {
        inventoryGameObject.SetActive(false);
        foreach (GameObject item in tempInventoryItems)
        {
            Destroy(item);
        }

        tempInventoryItems.Clear();
        InventoryIsOpen = false;
        itemTooltip.DeactivateTooltip();
    }

    public void UpdateInventory()
    {
        foreach (GameObject item in tempInventoryItems)
        {
            Destroy(item);
        }

        tempInventoryItems.Clear();

        foreach (InventoryItem item in inventory.Items)
        {
            GameObject itemUI = Instantiate(inventorySlotPrefab, inventoryUIContainer.transform);
            itemUI.GetComponent<InventorySlotUI>().SetItem(item);
            itemUI.GetComponent<InventorySlotUI>().SetItemCount(item.count);
            tempInventoryItems.Add(itemUI);
        }
    }
}
