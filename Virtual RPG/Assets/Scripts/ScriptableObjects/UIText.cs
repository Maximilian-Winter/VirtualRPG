using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName ="new UI Text", menuName = "VRPG/UI Text", order = 51)]
public class UIText : ScriptableObject
{
    [TextArea(3, 10)]
    public string text;
    public TMPro.TMP_FontAsset textFont;
    public int textSize = 1;
    public int textLineSpacing = 1;
    public TMPro.TextAlignmentOptions textAnchor = TMPro.TextAlignmentOptions.TopLeft;
}
