﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Cached components
    private InputMaster myControls;

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private Collider2D myCollider;

    //States
    private bool isAlive = true;

    //Attributes
    [SerializeField]
    private float jumpVelocity = 5f;

    [SerializeField]
    private float movementSpeed = 1f;

    private void Awake()
    {
        myControls = new InputMaster();
        myControls.Player.Jump.performed += context => Jump();
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
    }

    public void Jump()
    {
        if(!myCollider.IsTouchingLayers(LayerMask.GetMask(LayerNames.Ground)))
        {
            return;
        }

        myRigidBody.velocity = Vector2.up * jumpVelocity;

        myAnimator.SetBool(AnimatorStates.Jumping, true);
        myAnimator.SetBool(AnimatorStates.Running, false);
    }

    private void FixedUpdate()
    {
        MovePlayer();

        if(myCollider.IsTouchingLayers(LayerMask.GetMask(LayerNames.Ground)))
        {
            myAnimator.SetBool(AnimatorStates.Running, true);
            myAnimator.SetBool(AnimatorStates.Jumping, false);
        }
        else
        {
            myAnimator.SetBool(AnimatorStates.Running, false);
        }
    }

    private void MovePlayer()
    {
        Vector2 playerVelocity = new Vector2(Time.deltaTime * movementSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
    }

    private void OnEnable()
    {
        myControls.Enable();
    }

    private void OnDisable()
    {
        myControls.Disable();
    }
}