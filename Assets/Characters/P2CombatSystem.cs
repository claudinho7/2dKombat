using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class P2CombatSystem : MonoBehaviour
{
    public float p2Health = 10;
    [NonSerialized] public static int p2lifes = 3;
    [NonSerialized] public float p2DamageDone;
    [NonSerialized] public bool isDeadP2;
    [NonSerialized] public bool p2IsHeavyAttack;
    [NonSerialized] public bool p2IsNormalAttack;
    [NonSerialized] public bool p2IsBlocking;
    [NonSerialized] public bool p2CanBlock = true;

    [SerializeField] CombatSystem _combatSystem;
    [SerializeField] private Timer timer;
    private float blockCooldown;
    
    private Animator _anim;
    
    private void Start()
    {
        _anim = gameObject.GetComponent<Animator>();
    }

    public void P2TakeDamage(float damageTaken)
    {
        p2Health = Mathf.Clamp(p2Health - damageTaken, 0, 10);

        if (p2Health <= 0)
        {
            //die
            _anim.SetTrigger("Died"); 
            isDeadP2 = true;
            
            p2lifes = p2lifes - 1;
        }
    }

    void Update()
    {
        if (p2IsHeavyAttack)
        {
            p2DamageDone = 1.5f;
        } 
        else if (p2IsNormalAttack)
        {
            p2DamageDone = 0.8f;
        }
        Debug.Log("player2 lifes:" + p2lifes);
        
        if (!p2CanBlock)
        {
            Debug.Log("player2 CANT block:");
        }
        if (blockCooldown - 3 > timer.currentTime)
        {
            p2CanBlock = true;
            Debug.Log("player2 can block:");
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("WeaponTag") && !p2IsBlocking)
        {
            P2TakeDamage(_combatSystem.p1DamageDone);
            _anim.SetTrigger("Hit");
            
            Debug.Log("player2 health:" + p2Health); 
        }
        
        //if blocking and p1 normal attack block
        if (collision.CompareTag("WeaponTag") && p2IsBlocking && _combatSystem.p1IsNormalAttack)
        {
            Debug.Log("player2 blocked the normal hit");
        }
        
        //if blocking and p1 heavy attack block
        if (collision.CompareTag("WeaponTag") && p2IsBlocking && _combatSystem.p1IsHeavyAttack)
        {
            Debug.Log("player2 blocked the heavy hit, can't block for 3 seconds");
            p2CanBlock= false;
            blockCooldown = timer.currentTime;
        }
    }
}