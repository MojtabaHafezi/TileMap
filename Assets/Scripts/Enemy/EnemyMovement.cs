using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private CapsuleCollider2D myBodyCollider;
    private BoxCollider2D myFeetCollider;
    private Rigidbody2D myRigidBody;

    [SerializeField]
    private float movementSpeed = 100f;

    private void Awake()
    {
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        myRigidBody.velocity = new Vector2(movementSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        FlipSprite();
        ChangeDirection();
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }

    private void ChangeDirection()
    {
        movementSpeed *= -1;
    }
}