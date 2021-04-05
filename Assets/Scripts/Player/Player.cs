using System;
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
    private CapsuleCollider2D myBodyCollider;
    private BoxCollider2D myFeetCollider;

    //Attributes
    [SerializeField]
    private float jumpVelocity = 5f;

    [SerializeField]
    private float movementSpeed = 1f;

    [SerializeField]
    private Vector2 deathKick = new Vector2(5f, 5f);

    private bool isAlive = true;
    private int health;

    private void Awake()
    {
        myControls = new InputMaster();
        myControls.Player.Jump.performed += context => Jump();
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        isAlive = true;
        //TODO: Load the health from player prefs
        health = 2;
    }

    public void Jump()
    {
        if(!isAlive)
        { return; }

        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask(LayerNames.Ground)))
        {
            return;
        }

        myRigidBody.velocity = Vector2.up * jumpVelocity;
    }

    private void FixedUpdate()
    {
        if(!isAlive)
        { return; }

        MovePlayer();
    }

    private void Update()
    {
        if(!isAlive)
        {
            myRigidBody.velocity = new Vector2(0, 0);
            return;
        }

        Hurt();

        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask(LayerNames.Ground)))
        {
            myAnimator.SetBool(AnimatorStates.Running, true);
            myAnimator.SetBool(AnimatorStates.Jumping, false);
        }
        else
        {
            myAnimator.SetBool(AnimatorStates.Jumping, true);
            myAnimator.SetBool(AnimatorStates.Running, false);
        }
    }

    private void MovePlayer()
    {
        Vector2 playerVelocity = new Vector2(Time.deltaTime * movementSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
    }

    private void Hurt()
    {
        if(IsDamaged())
        {
            health--;

            if(health <= 0)
            {
                Die();
            }
            else
            {
                myAnimator.SetBool(AnimatorStates.Hurt, true);
                myAnimator.SetBool(AnimatorStates.Running, false);
                myAnimator.SetBool(AnimatorStates.Jumping, false);
            }
        }
    }

    private bool IsDamaged()
    {
        bool isDamaged = false;

        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask(LayerNames.Enemy, LayerNames.Hazards)))
        {
            isDamaged = true;
        }

        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask(LayerNames.Hazards)))
        {
            isDamaged = true;
        }

        return isDamaged;
    }

    private void Die()
    {
        isAlive = false;
        myRigidBody.velocity = deathKick;
        myAnimator.SetBool(AnimatorStates.Running, false);
        myAnimator.SetBool(AnimatorStates.Jumping, false);
        myAnimator.SetTrigger(AnimatorStates.Die);
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