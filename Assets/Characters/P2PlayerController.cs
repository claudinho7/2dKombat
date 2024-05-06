using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class P2PlayerController : MonoBehaviour
{
    [SerializeField] private P2CombatSystem _P2CombatSystem;
    [SerializeField] private Timer timer;
    [SerializeField] private UI_Handeler _uiHandeler;
    
    public Rigidbody2D _rb;
    private Animator _anim;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public float movementSpeed = 5f;
    public float jumpForce = 12f;
    private bool isFacingRight = true;
    private float moveHorizontal;
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _anim = gameObject.GetComponent<Animator>();
    }
    
    void Update()
    {
        _rb.velocity = new Vector2(moveHorizontal * movementSpeed, _rb.velocity.y);

        //check the facing direction and velocity input to flip the sprite
        if (!isFacingRight && moveHorizontal > 0f)
        {
            Flip();
        } 
        else if (isFacingRight && moveHorizontal < 0f)
        {
            Flip();
        }
        
        if (!_uiHandeler.canPlay)
        {
            GetComponent<PlayerInput>().enabled = false;
        }
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        //check the input system for X axis values
        moveHorizontal = context.ReadValue<Vector2>().x;
        
        //if there is movement, transit to runAnimation
        if (moveHorizontal == 0f)
        {
            _anim.SetBool("IsRunning", false);
        }
        else
        {
            _anim.SetBool("IsRunning", true);
        }
    }
    
    private bool IsGrounded()
    {
        //isgrounded true if player's feet overlaps the layer of jump-able objects
        //_anim.SetBool("isJumping", false);
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        //jump if input key pressed & isgrounded true
        if (context.performed && IsGrounded())
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            //_anim.SetBool("isJumping", true);
        }
        //if key is held down jump higher
        if (context.canceled && _rb.velocity.y > 0f)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
            //_anim.SetBool("isJumping", true);
        }
    }

    public void HeavyAttack(InputAction.CallbackContext context)
    {
        //if attack, do anim
        if (context.performed)
        {
            _anim.SetTrigger("HeavyAttack");
            _P2CombatSystem.p2IsHeavyAttack = true;
        }
        else
        {
            _anim.ResetTrigger("HeavyAttack");
            _P2CombatSystem.p2IsHeavyAttack = false;
        }
    }
    
    public void LightAttack(InputAction.CallbackContext context)
    {
        //if attack, do anim
        if (context.performed)
        {
            _anim.SetTrigger("LightAttack");
            _P2CombatSystem.p2IsNormalAttack = true;
        }
        else
        {
            _anim.ResetTrigger("LightAttack");
            _P2CombatSystem.p2IsNormalAttack = false;
        }
    }
    
    public void blocking(InputAction.CallbackContext context)
    {
        //if blocking, do anim
        if (context.performed && _P2CombatSystem.p2CanBlock)
        {
            _P2CombatSystem.p2IsBlocking = true;
            _anim.SetBool("P2Blocking", true);
        }
        else
        {
            _anim.SetBool("P2Blocking", false);
            _P2CombatSystem.p2IsBlocking = false;
        }
    }
}