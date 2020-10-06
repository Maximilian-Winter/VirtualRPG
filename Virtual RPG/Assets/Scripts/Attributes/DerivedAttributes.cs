using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DerivedAttributes
{
    private int health;
    private int carryweight;
    private int mana;
    private int damageResistance;

    private int strengthModifier;
    private int enduranceModifier;
    private int toughnessModifier;
    private int agilityModifier;
    private int dexterityModifier;
    private int intelligenceModifier;
    private int wisdomModifier;
    private int willpowerModifier;
    private int charismaModifier;



    public int Health { get => health; set => health = value; }
    public int Carryweight { get => carryweight; set => carryweight = value; }
    public int Mana { get => mana; set => mana = value; }
    public int StrengthModifier { get => strengthModifier; set => strengthModifier = value; }
    public int EnduranceModifier { get => enduranceModifier; set => enduranceModifier = value; }
    public int ToughnessModifier { get => toughnessModifier; set => toughnessModifier = value; }
    public int AgilityModifier { get => agilityModifier; set => agilityModifier = value; }
    public int DexterityModifier { get => dexterityModifier; set => dexterityModifier = value; }
    public int IntelligenceModifier { get => intelligenceModifier; set => intelligenceModifier = value; }
    public int WisdomModifier { get => wisdomModifier; set => wisdomModifier = value; }
    public int WillpowerModifier { get => willpowerModifier; set => willpowerModifier = value; }
    public int CharismaModifier { get => charismaModifier; set => charismaModifier = value; }
    public int DamageResistance { get => damageResistance; set => damageResistance = value; }

    public void InitDerivedAttributes(BaseAttributes baseAttributes)
    {
        health = (baseAttributes.Strength + baseAttributes.Toughness / 4) + 10;
        carryweight = (baseAttributes.Strength + baseAttributes.Endurance / 6) + 250;
        mana = (baseAttributes.Intelligence + baseAttributes.Willpower / 2) + 5;
        damageResistance = (baseAttributes.Toughness / 4) + 10;

        strengthModifier = (baseAttributes.Strength - 5) / 2;
        enduranceModifier = (baseAttributes.Endurance - 5) / 2;
        toughnessModifier = (baseAttributes.Toughness - 5) / 2;
        agilityModifier = (baseAttributes.Agility - 5) / 2;
        dexterityModifier = (baseAttributes.Dexterity - 5) / 2;
        intelligenceModifier = (baseAttributes.Intelligence - 5) / 2;
        wisdomModifier = (baseAttributes.Wisdom - 5) / 2;
        willpowerModifier = (baseAttributes.Willpower - 5) / 2;
        charismaModifier = (baseAttributes.Charisma - 5) / 2;

    }
}
