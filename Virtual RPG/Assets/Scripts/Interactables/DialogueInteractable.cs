using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Yarn.Unity;

public class DialogueInteractable : MonoBehaviour
{
    [SerializeField]
    private Interactable interactable;

    [SerializeField]
    private string talkToNode = "";

    [SerializeField]
    private YarnProgram scriptToLoad;

    private DialogueRunner dialogueRunner;

    private DialogueUI dialogueUI;
    void OnEnable()
    {
        interactable.Interact += OnInteract;

    }

    void OnDisable()
    {
        interactable.Interact -= OnInteract;
    }

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueUI = FindObjectOfType<DialogueUI>();
        if (scriptToLoad != null)
        {
            DialogueRunner dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
            dialogueRunner.Add(scriptToLoad);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnInteract()
    {
        if(!dialogueRunner.IsDialogueRunning)
        {
            // Kick off the dialogue at this node.
            dialogueRunner.StartDialogue(talkToNode);
        }
        else
        {
            dialogueUI.MarkLineComplete();
        }
    }

}
