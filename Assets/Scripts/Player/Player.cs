using Assets.Scripts.StringMapping;
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
    private Material myMaterial;

    //Attributes
    [SerializeField]
    private float jumpVelocity = 5f;

    [SerializeField]
    private float movementSpeed = 1f;

    [SerializeField]
    private float immunityDuration = 1f;

    private float timeHurt = 0f;

    [SerializeField]
    private Vector2 deathKick = new Vector2(5f, 5f);

    [SerializeField]
    private float fade;

    private bool isAlive = true;
    private bool isMoving = true;
    private bool startDissolve = false;
    private int health;

    private void Awake()
    {
        myControls = new InputMaster();
        myControls.Player.Jump.performed += context => Jump();
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myMaterial = GetComponent<SpriteRenderer>().material;

        isAlive = true;
        isMoving = true;
        startDissolve = false;
        //TODO: Load the health from player prefs
        health = 3;
        fade = 1f;
    }

    public void Jump()
    {
        if(!isAlive || !isMoving)
        { return; }

        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask(Layers.Ground)))
        {
            return;
        }

        myRigidBody.velocity = Vector2.up * jumpVelocity;
    }

    private void FixedUpdate()
    {
        if(!isAlive || !isMoving)
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

        CheckForDamage();

        if(isAlive)
        {
            if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask(Layers.Ground)))
            {
                myAnimator.SetBool(AnimationParameters.Running, true);
                myAnimator.SetBool(AnimationParameters.Jumping, false);
            }
            else
            {
                myAnimator.SetBool(AnimationParameters.Jumping, true);
                myAnimator.SetBool(AnimationParameters.Running, false);
            }
            Dissolve();
        }
    }

    private void MovePlayer()
    {
        Vector2 playerVelocity = new Vector2(Time.deltaTime * movementSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
    }

    private void StopPlayer()
    {
        isMoving = false;
        myRigidBody.velocity = new Vector2(0, 0);
        myAnimator.SetBool(AnimationParameters.Running, false);
        myAnimator.SetBool(AnimationParameters.Jumping, false);
        myAnimator.enabled = false;
    }

    private void CheckForDamage()
    {
        if(IsDamaged())
        {
            health--;

            if(health <= 0)
            {
                health = 0;
                Die();
            }
            else
            {
                myAnimator.SetBool(AnimationParameters.Running, false);
                myAnimator.SetBool(AnimationParameters.Jumping, false);
                myAnimator.SetTrigger(AnimationParameters.Hurt);
            }
        }
    }

    private bool IsDamaged()
    {
        bool isDamaged = false;

        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask(Layers.Enemy, Layers.Hazards)))
        {
            isDamaged = true;
        }

        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask(Layers.Hazards)))
        {
            isDamaged = true;
        }

        if(isDamaged && (Time.time - timeHurt) > immunityDuration)
        {
            timeHurt = Time.time;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Die()
    {
        isAlive = false;
        myRigidBody.velocity = deathKick;
        myAnimator.SetBool(AnimationParameters.Running, false);
        myAnimator.SetBool(AnimationParameters.Jumping, false);
        myAnimator.SetTrigger(AnimationParameters.Die);
    }

    public void Dissolve()
    {
        if(startDissolve)
        {
            fade -= Time.deltaTime;
            if(fade <= 0f)
            {
                fade = 0f;
                startDissolve = false;
            }
            myMaterial.SetFloat(Shaders.Fade, fade);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Equals(Tags.Finish) && !startDissolve)
        {
            StopPlayer();
            startDissolve = true;
        }
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