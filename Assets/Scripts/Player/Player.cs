using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private InputMaster controls;
    private Rigidbody2D rigidbody;

    private float jumpVelocity = 5;

    private void Awake()
    {
        controls = new InputMaster();
        controls.Player.Jump.performed += context => Jump();
        rigidbody = transform.GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        rigidbody.velocity = Vector2.up * jumpVelocity;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void FixedUpdate()
    {
    }
}