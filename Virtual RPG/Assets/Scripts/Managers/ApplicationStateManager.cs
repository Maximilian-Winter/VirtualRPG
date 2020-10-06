using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationStateManager : MonoBehaviour
{
    private bool uiIsOpen;

    public bool UIIsOpen { get => uiIsOpen; set => uiIsOpen = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test()
    {
        Debug.Log("TEST");
    }
}
