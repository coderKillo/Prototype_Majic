using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceManager : MonoBehaviour
{
    [Header("Spells")]
    [SerializeField] private AbilityUI spellIconPrefab;
    [SerializeField] private Transform spellIconSlot;

    protected static InterfaceManager s_Instance;
    public static InterfaceManager instance { get { return s_Instance; } }

    private void Awake()
    {
        s_Instance = this;
    }

    public void AddSpellSlot(ISpell spell)
    {
        AbilityUI spellUI = Instantiate(spellIconPrefab, spellIconSlot);
        spell.OnAbilityUse.AddListener((cooldown) => spellUI.ShowCoolDown(cooldown));
        spellUI.SetIcon(spell.spellIcon);
    }

}
