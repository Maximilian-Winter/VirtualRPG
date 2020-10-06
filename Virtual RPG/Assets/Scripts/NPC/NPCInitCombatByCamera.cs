using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInitCombatByCamera : MonoBehaviour
{
    [SerializeField]
    private GameEvent enterCombatEvent;
    [SerializeField]
    private NPCCombatController combatController;

    private Camera cam;

    void Start()
    {
        cam = UnityEngine.Camera.main;
    }

    void Update()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(transform.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1 && viewPos.z > 0)
        {
            combatController.EnterCombatMode();
            enterCombatEvent.Raise();
            gameObject.SetActive(false);
        }
    }
}
