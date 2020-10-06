using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public void OnInteract()
    {
        RaycastHit2D hit;  
        hit = Physics2D.Raycast(transform.position, Vector2.zero, 0f, LayerMask.GetMask("Interactable"));
        
        if (hit.collider != null)
        {
            if (hit.collider.tag == "Interactable")
            {
                hit.collider.GetComponent<Interactable>().OnPlayerInteract();
            }
        }
    }
}
