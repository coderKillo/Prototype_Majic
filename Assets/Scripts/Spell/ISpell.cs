using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ISpell : ScriptableObject
{
    [Header("Spell Information")]
    public Sprite spellIcon;
    public string spellName;
    public float cooldownTime = 1;
    [TextArea]
    public string spellDescription;

    [Header("Effects")]
    public GameObject spellCastFX;
    public GameObject spellWarmUpFX;
    public GameObject spellImpactFX;


    [Header("Animation")]
    public string spellAnimation;
    public float castTime = 1;

    public class MyFloatEvent : UnityEvent<float> { }
    public MyFloatEvent OnAbilityUse = new MyFloatEvent();

    private bool canUse = true;

    public void CastSpell(AnimationManager animationManager, Transform casterHand, Vector3 target, Transform parent)
    {
        if (canUse)
        {
            OnAbilityUse.Invoke(cooldownTime);
            AttemptToCastSpell(animationManager, casterHand, target, parent);
            animationManager.StartCoroutine(Cooldown());
        }
    }

    public void Reset()
    {
        canUse = true;
    }

    public virtual void AttemptToCastSpell(AnimationManager animationManager, Transform casterHand, Vector3 target, Transform parent)
    {
        Debug.Log("Attempt to cast spell");
    }

    public virtual void SuccessfullyCastSpell(AnimationManager animationManager, Transform casterHand, Vector3 target, Transform parent)
    {
        Debug.Log("Spell is cast successfully");
    }

    IEnumerator Cooldown()
    {
        canUse = false;
        yield return new WaitForSeconds(cooldownTime);
        canUse = true;
    }
}
