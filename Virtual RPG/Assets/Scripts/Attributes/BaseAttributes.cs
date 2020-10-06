using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseAttributes
{
    private int strength;
    private int endurance;
    private int toughness;
    private int agility;
    private int dexterity;

    private int intelligence;
    private int willpower;
    private int wisdom;
    private int charisma;


    public int Strength { get => strength; set => strength = value; }
    public int Endurance { get => endurance; set => endurance = value; }
    public int Toughness { get => toughness; set => toughness = value; }
    public int Agility { get => agility; set => agility = value; }
    public int Dexterity { get => dexterity; set => dexterity = value; }

    public int Intelligence { get => intelligence; set => intelligence = value; }
    public int Willpower { get => willpower; set => willpower = value; }
    public int Wisdom { get => wisdom; set => wisdom = value; }
    public int Charisma { get => charisma; set => charisma = value; }
}
