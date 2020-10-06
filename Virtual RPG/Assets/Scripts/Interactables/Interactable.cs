using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public delegate void Interaction();
    public Interaction Interact;

    public void OnPlayerInteract()
    {
        if (Interact != null)
        {
            Interact();
        }
    }
}
