using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    protected static PlayerManager s_Instance;
    public static PlayerManager instance { get { return s_Instance; } }

    public PlayerSpellManager spellManager;

    private PlayerMovement playerMovement;
    private InputManager inputManager;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        inputManager = GetComponent<InputManager>();
        spellManager = GetComponent<PlayerSpellManager>();

        Cursor.lockState = CursorLockMode.Locked;

        s_Instance = this;
    }

    void Start()
    {
    }

    private void Update()
    {
        inputManager.HandleAllInputs();

        playerMovement.HandleMovement();
        playerMovement.HandleRotation();
    }

    private void CastSpell()
    {
    }

}
