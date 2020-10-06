using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Application States", menuName = "VRPG/Application States", order = 51)]
public class ApplicationStates : ScriptableObject
{
    public bool uiIsOpen;
    public bool isInCombatMode;
}
