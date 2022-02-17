using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellManager : MonoBehaviour
{
    [SerializeField] public ISpell[] spellSlots;
    [SerializeField] private Transform casterHandTransform;
    [SerializeField] private Transform spellCollectorTransform;

    private InputManager inputManager;
    private AnimationManager animationManager;
    private Transform cameraTransform;

    private int currentSpellIndex = 0;

    void Awake()
    {
        inputManager = GetComponent<InputManager>();
        animationManager = GetComponent<AnimationManager>();
        cameraTransform = Camera.main.transform;
    }

    void Start()
    {
        inputManager.shootAction.performed += _ => onEventCastSpell(0);

        foreach (var spell in spellSlots)
        {
            spell.Reset();
            InterfaceManager.instance.AddSpellSlot(spell);
        }
    }

    private Vector3 GetTarget()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
        {
            return hit.point;
        }
        return cameraTransform.position + cameraTransform.forward * 100f;
    }

    public void onEventCastSpell(int index)
    {
        if (index >= spellSlots.Length)
            return;

        currentSpellIndex = index;

        spellSlots[index].CastSpell(animationManager, casterHandTransform, GetTarget(), spellCollectorTransform);
    }

    public void onEventCastAnimationDone()
    {
        spellSlots[currentSpellIndex].SuccessfullyCastSpell(animationManager, casterHandTransform, GetTarget(), spellCollectorTransform);
    }
}
