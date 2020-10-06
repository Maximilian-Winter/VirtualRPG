using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthManaDisplay : MonoBehaviour
{
    [SerializeField]
    AttributesController attributesController;

    [SerializeField]
    CombatController combatController;

    [SerializeField]
    private Text healthText;
    [SerializeField]
    public Image healthBarImage;

    [SerializeField]
    private Text manaText;
    [SerializeField]
    public Image manaBarImage;

    [SerializeField]
    private Text movementInCombatText;
    [SerializeField]
    public Image movementInCombatBarImage;

    [SerializeField]
    public GameObject turnIndicatorField;
    [SerializeField]
    public GameObject actionIndicatorField;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = attributesController.CurrentHealth + " / " + attributesController.MaxHealth;
        manaText.text = attributesController.CurrentMana + " / " + attributesController.MaxMana;
        movementInCombatText.text = (int)(combatController.MovementSpeed - combatController.Delta) + " / " + (int)combatController.MovementSpeed;

        healthBarImage.fillAmount = (float)attributesController.CurrentHealth / (float)attributesController.MaxHealth;
        manaBarImage.fillAmount = (float)attributesController.CurrentMana / (float)attributesController.MaxMana;
        movementInCombatBarImage.fillAmount = (float)(combatController.MovementSpeed - combatController.Delta) / (float)combatController.MovementSpeed;

        if(combatController.HasDoneAction)
        {
            actionIndicatorField.SetActive(true);
        }
        else
        {
            actionIndicatorField.SetActive(false);
        }

        if (combatController.IsOnTurn)
        {
            turnIndicatorField.SetActive(true);
        }
        else
        {
            turnIndicatorField.SetActive(false);
        }
    }
}
