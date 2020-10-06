using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SignInteractable : MonoBehaviour
{

    [SerializeField]
    private UIText uiText;

    [SerializeField]
    private GameObject signUIPanel;

    [SerializeField]
    private TextMeshProUGUI signContent;

    [SerializeField]
    private Interactable interactable;

    [SerializeField]
    private ApplicationStates applicationStates;

    private bool isTriggered;
    private string triggerTag;

    void OnEnable()
    {
        interactable.Interact += OnInteract;
    }

    void OnDisable()
    {
        interactable.Interact -= OnInteract;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        isTriggered = true;
        triggerTag = col.tag;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        isTriggered = true;
        triggerTag = col.tag;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        isTriggered = false;
        triggerTag = "";
    }

    void OnInteract()
    {
        if (!signUIPanel.activeSelf)
        {
            signContent.text = uiText.text;
            signContent.font = uiText.textFont;
            signContent.fontSize = uiText.textSize;
            signContent.lineSpacing = uiText.textLineSpacing;
            signContent.alignment = uiText.textAnchor;

            signUIPanel.SetActive(true);
            applicationStates.uiIsOpen = true;
        }
        else
        {
            signUIPanel.SetActive(false);
            signContent.text = "";
            applicationStates.uiIsOpen = false;
        }
    }
}
