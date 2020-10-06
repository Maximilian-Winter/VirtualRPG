using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributesController : MonoBehaviour
{
    [SerializeField]
    int strength = 9;

    [SerializeField]
    int endurance = 9;

    [SerializeField]
    int toughness = 9;

    [SerializeField]
    int agility = 9;

    [SerializeField]
    int dexterity = 9;

    [SerializeField]
    int intelligence = 9;

    [SerializeField]
    int wisdom = 9;

    [SerializeField]
    int willpower = 9;

    [SerializeField]
    int charisma = 9;

    private BaseAttributes baseAttributes;
    private DerivedAttributes derivedAttributes;

    [SerializeField]
    private int maxHealth;
    private int maxMana;

    [SerializeField]
    private int currentHealth;
    private int currentMana;

    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int MaxMana { get => maxMana; set => maxMana = value; }
    public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
    public int CurrentMana { get => currentMana; set => currentMana = value; }
    public BaseAttributes BaseAttributes { get => baseAttributes; set => baseAttributes = value; }
    public DerivedAttributes DerivedAttributes { get => derivedAttributes; set => derivedAttributes = value; }

    // Start is called before the first frame update
    void Start()
    {
        BaseAttributes = new BaseAttributes
        {
            Strength = strength,
            Endurance = endurance,
            Toughness = toughness,
            Agility = agility,
            Dexterity = dexterity,
            Intelligence = intelligence,
            Willpower = willpower,
            Wisdom = wisdom,
            Charisma = charisma
        };

        DerivedAttributes = new DerivedAttributes();
        DerivedAttributes.InitDerivedAttributes(BaseAttributes);

        MaxHealth = DerivedAttributes.Health;
        MaxMana = DerivedAttributes.Mana;

        CurrentHealth = MaxHealth;
        CurrentMana = MaxMana;
    }

    // Update is called once per frame
    void Update()
    {
       //Debug.Log("Health: " + derivedAttributes.Health);
       //Debug.Log("Mana: " + derivedAttributes.Mana);
       //Debug.Log("Carryweight: " + derivedAttributes.Carryweight);
    }

    public DerivedAttributes GetDerivedAttributes()
    {
        return DerivedAttributes;
    }

    public void ApplyAttributeEffect(AttributeEffect effect)
    {
        if (effect.statEffect == StatEffect.Health)
        {
            currentHealth += effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.Mana)
        {
            currentMana += effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.MaxHealth)
        {
            derivedAttributes.Health += effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.MaxMana)
        {
            derivedAttributes.Mana += effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.Carryweight)
        {
            derivedAttributes.Carryweight += effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.DamageResistance)
        {
            derivedAttributes.DamageResistance += effect.attributeEffectValue;
        }


        if (effect.statEffect == StatEffect.AgilityModifier)
        {
            derivedAttributes.AgilityModifier += effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.CharismaModifier)
        {
            derivedAttributes.CharismaModifier += effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.DexterityModifier)
        {
            derivedAttributes.DexterityModifier += effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.EnduranceModifier)
        {
            derivedAttributes.EnduranceModifier += effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.IntelligenceModifier)
        {
            derivedAttributes.IntelligenceModifier += effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.StrengthModifier)
        {
            derivedAttributes.StrengthModifier += effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.ToughnessModifier)
        {
            derivedAttributes.ToughnessModifier += effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.WillpowerModifier)
        {
            derivedAttributes.WillpowerModifier += effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.WisdomModifier)
        {
            derivedAttributes.WisdomModifier += effect.attributeEffectValue;
        }

        MaxHealth = DerivedAttributes.Health;
        MaxMana = DerivedAttributes.Mana;

        if(currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth;
        }

        if (currentMana > MaxMana)
        {
            currentMana= MaxMana;
        }
    }

    public void RemoveAttributeEffect(AttributeEffect effect)
    {
        if (effect.statEffect == StatEffect.Health)
        {
            currentHealth -= effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.Mana)
        {
            currentMana -= effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.MaxHealth)
        {
            derivedAttributes.Health -= effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.MaxMana)
        {
            derivedAttributes.Mana -= effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.Carryweight)
        {
            derivedAttributes.Carryweight -= effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.DamageResistance)
        {
            derivedAttributes.DamageResistance -= effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.AgilityModifier)
        {
            derivedAttributes.AgilityModifier -= effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.CharismaModifier)
        {
            derivedAttributes.CharismaModifier -= effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.DexterityModifier)
        {
            derivedAttributes.DexterityModifier -= effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.EnduranceModifier)
        {
            derivedAttributes.EnduranceModifier -= effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.IntelligenceModifier)
        {
            derivedAttributes.IntelligenceModifier -= effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.StrengthModifier)
        {
            derivedAttributes.StrengthModifier -= effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.ToughnessModifier)
        {
            derivedAttributes.ToughnessModifier -= effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.WillpowerModifier)
        {
            derivedAttributes.WillpowerModifier -= effect.attributeEffectValue;
        }

        if (effect.statEffect == StatEffect.WisdomModifier)
        {
            derivedAttributes.WisdomModifier -= effect.attributeEffectValue;
        }



        MaxHealth = DerivedAttributes.Health;
        MaxMana = DerivedAttributes.Mana;

        if (currentHealth > MaxHealth)
        {
            currentHealth = MaxHealth;
        }

        if (currentMana > MaxMana)
        {
            currentMana = MaxMana;
        }
    }
}
