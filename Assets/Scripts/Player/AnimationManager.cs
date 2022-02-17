using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public bool isInteracting;

    private CharacterController controller;
    private Animator animator;

    private int horizontal;
    private int vertical;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        animator.SetFloat(horizontal, horizontalMovement, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, verticalMovement, 0.1f, Time.deltaTime);
    }

    public void PlayTargetAnimation(string targetAnimation, bool interacting)
    {
        animator.SetBool("isInteracting", interacting);
        animator.CrossFade(targetAnimation, 0.2f);
    }

    private void LateUpdate()
    {
        isInteracting = animator.GetBool("isInteracting");
    }

}
