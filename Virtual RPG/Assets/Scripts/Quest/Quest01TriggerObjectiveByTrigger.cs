using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Quest01TriggerObjectiveByTrigger : MonoBehaviour
{
    [SerializeField] private bool ready;
    // The objective to trigger, and how to trigger it.
    [SerializeField] ObjectiveTrigger objective = new ObjectiveTrigger();

    [SerializeField] NPCController npcController;

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
        if(ready)
        {
            npcController.SetFollowTargetOff();
            InMemoryVariableStorage varStorage = FindObjectOfType<InMemoryVariableStorage>();
            varStorage.SetValue("$kid_saved", new Yarn.Value(true));
            // We just completed or failed this objective!
            objective.Invoke();

            // Disable this component so that it doesn't get run twice
            this.enabled = false;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
       
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        
    }

    [YarnCommand("SetTriggerOn")]
    public void SetTriggerOn()
    {
        ready = true;
    }

    [YarnCommand("SetTriggerOff")]
    public void SetTriggerOff()
    {
        ready = false;
    }
}
