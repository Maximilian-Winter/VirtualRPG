using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "new Dialogue Text", menuName = "VRPG/Dialogue Text", order = 51)]
public class DialogueText : ScriptableObject
{
    public UIText dialogueText;
    public DialogueText[] nextDialogueTexts;

    public IntegerGameEvent startQuestEvent;
    public int startQuestEventParameter;

    public IntegerGameEvent endQuestEvent;
    public int endQuestEventParameter;

    public GameEvent dialogueEvent;
}
