using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMessageSystem : MonoBehaviour
{
    [SerializeField]
    GameObject playerMessageDialogueObject;

    [SerializeField]
    Text messageTextField;


    public void ShowPlayerMessage(string message)
    {
        messageTextField.text = message;
        playerMessageDialogueObject.SetActive(true);
        Invoke("HidePlayerMessage", 5.0f);
    }

    public void HidePlayerMessage()
    {
        playerMessageDialogueObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
