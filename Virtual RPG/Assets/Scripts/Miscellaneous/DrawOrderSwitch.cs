using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOrderSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        col.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = -1;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        col.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = -1;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        col.gameObject.GetComponentInChildren<SpriteRenderer>().sortingOrder = 2;
    }
}
