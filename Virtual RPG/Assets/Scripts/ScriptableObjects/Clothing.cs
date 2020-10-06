using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClothingSlot
{
    HeadClothing,
    ChestClothing,
    HandsClothing,
    LegsClothing,
    FootClothing
}

[CreateAssetMenu(fileName = "new Clothing", menuName = "VRPG/Items/Clothing", order = 51)]
public class Clothing : BaseItem
{
    public ClothingSlot clothingSlot;
    public AttributeEffect clothingEffect;
}
