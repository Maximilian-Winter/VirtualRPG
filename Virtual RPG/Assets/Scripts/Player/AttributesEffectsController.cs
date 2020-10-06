using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum StatEffect
{
    Health,
    MaxHealth,
    Carryweight,
    Mana,
    MaxMana,
    DamageResistance,

    StrengthModifier,
    EnduranceModifier,
    ToughnessModifier,
    AgilityModifier,
    DexterityModifier,
    IntelligenceModifier,
    WisdomModifier,
    WillpowerModifier,
    CharismaModifier
}

public enum AttributeEffectType
{
    RepeatDuringDuration,
    ApplyOnce,
    PermanentRepeat,
    Permanent
}

[Serializable]
public struct AttributeEffect
{
  
    public StatEffect statEffect;
    public AttributeEffectType attributeEffectType;
    public int attributeEffectValue;
    public bool removeEffectFromAttributesWhenRemovingAttributeEffect;
    public float attributeEffectDuration;
    public float attributeEffectRestDuration;
    public bool effectApplied;

    public void SetEffectAppliedToTrue()
    {
        this.effectApplied = true;
    }

    public void CalculateRestDuration(float deltaTime)
    {
        this.attributeEffectRestDuration -= deltaTime;
    }
}

public class AttributesEffectsController : MonoBehaviour
{
    [SerializeField]
    private CombatController combatController;

    [SerializeField]
    private AttributesController attributesController;

    private List<AttributeEffect> statEffects;
    // Start is called before the first frame update
    void Start()
    {
        statEffects = new List<AttributeEffect>();
        InvokeRepeating("UpdateEffects", 1.0f, 1.0f);
    }

    public void AddAttributeEffect(AttributeEffect effect)
    {
        statEffects.Add(effect);
    }

    public void RemoveAttributeEffect(AttributeEffect effect)
    {
        if (effect.removeEffectFromAttributesWhenRemovingAttributeEffect)
        {
            attributesController.RemoveAttributeEffect(effect);
        }
        statEffects.Remove(effect);
    }


    // Update is called once per frame
    void UpdateEffects()
    {

        for(int i = 0; i < statEffects.Count;i++)
        {
            AttributeEffect effect = statEffects[i];

            if (!effect.effectApplied)
            {
                attributesController.ApplyAttributeEffect(effect);
                effect.SetEffectAppliedToTrue();
            }
            if (effect.attributeEffectType == AttributeEffectType.RepeatDuringDuration || effect.attributeEffectType == AttributeEffectType.PermanentRepeat)
            {
                attributesController.ApplyAttributeEffect(effect);
            }

            effect.CalculateRestDuration(Time.deltaTime);
            if (effect.attributeEffectRestDuration <= 0 && !(effect.attributeEffectType == AttributeEffectType.PermanentRepeat || effect.attributeEffectType == AttributeEffectType.Permanent))
            {
                if (effect.removeEffectFromAttributesWhenRemovingAttributeEffect)
                {
                    attributesController.RemoveAttributeEffect(effect);
                }
            }

            statEffects[i] = effect;
        }
        statEffects.RemoveAll(s => s.attributeEffectRestDuration <= 0 && !(s.attributeEffectType == AttributeEffectType.PermanentRepeat || s.attributeEffectType == AttributeEffectType.Permanent));
        
    }


}
