using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveReticle : MonoBehaviour
{
    public Texture2D normalCursorTexture;
    public Texture2D combatCursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 normalHotSpot = Vector2.zero;
    public Vector2 combatHotSpot = Vector2.zero;

    void Start()
    {
        Cursor.SetCursor(normalCursorTexture, normalHotSpot, cursorMode);
    }

    public void SetCursorToNormalMode()
    {
        Cursor.SetCursor(normalCursorTexture, normalHotSpot, cursorMode);
    }

    public void SetCursorToCombatMode()
    {
        Cursor.SetCursor(combatCursorTexture, combatHotSpot, cursorMode);
    }

}
