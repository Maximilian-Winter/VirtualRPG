using CsvHelper.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventoryItem
{
    public BaseItem item;
    public int count;
}

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private List<InventoryItem> items;

    /*[SerializeField]
    private List<BaseItem> randomItemPossibilitys;*/

    public List<InventoryItem> Items { get => items; set => items = value; }


    // Start is called before the first frame update
    void Start()
    {
        /*System.Random rnd = new System.Random();
        for(int i = 0; i <= 500; i++)
        {
            int itemNumber = rnd.Next(0, randomItemPossibilitys.Count);
            int itemCount = rnd.Next(1, 255);
            InventoryItem newItem = new InventoryItem();
            newItem.item = randomItemPossibilitys[itemNumber];
            newItem.count = itemCount;
            Items.Add(newItem);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
