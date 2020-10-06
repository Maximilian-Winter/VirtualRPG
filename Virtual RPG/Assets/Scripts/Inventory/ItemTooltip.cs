using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField]
    private GameObject tooltip;

    [SerializeField]
    private Text tooltipItemName;

    [SerializeField]
    private Text tooltipItemType;

    [SerializeField]
    private Text tooltipItemDescription;

    public Text TooltipItemName { get => tooltipItemName; set => tooltipItemName = value; }
    public Text TooltipItemType { get => tooltipItemType; set => tooltipItemType = value; }
    public Text TooltipItemDescription { get => tooltipItemDescription; set => tooltipItemDescription = value; }
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateTooltip()
    {
        tooltip.SetActive(true);
    }

    public void DeactivateTooltip()
    {
        tooltip.SetActive(false);
    }


}
