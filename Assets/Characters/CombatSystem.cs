using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CombatSystem : MonoBehaviour
{
    public float p1Health = 10;
    [NonSerialized] public static int p1lifes = 3;
    [NonSerialized] public bool isDeadP1;
    [NonSerialized] public float p1DamageDone;
    [NonSerialized] public bool p1IsHeavyAttack;
    [NonSerialized] public bool p1IsNormalAttack;
    [NonSerialized] public bool p1IsBlocking;
    [NonSerialized] public bool p1CanBlock = true;
    

    [SerializeField] P2CombatSystem _p2CombatSystem;
    [SerializeField] private Timer timer;
    private float blockCooldown;

    private Animator _anim;

    private void Start()
    {
        _anim = gameObject.GetComponent<Animator>();
    }

    public void P1TakeDamage(float damageTaken)
    {
        p1Health = Mathf.Clamp(p1Health - damageTaken, 0, 10);

        if (p1Health <= 0)
        {
            //die
            _anim.SetTrigger("Died"); 
            isDeadP1 = true;

            p1lifes = p1lifes - 1;
        }
    }

    void Update()
    {
        if (p1IsHeavyAttack)
        {
            p1DamageDone = 1.5f;
        }
        else if (p1IsNormalAttack)
        {
            p1DamageDone = 0.8f;
        }
        Debug.Log("player1 lifes:" + p1lifes);

        if (!p1CanBlock)
        {
            Debug.Log("player1 CANT block:");
        }
        if (blockCooldown - 3 > timer.currentTime)
        {
            p1CanBlock = true;
            Debug.Log("player1 can block:");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if not blocking do damage
        if (collision.CompareTag("P2WeaponTag") && !p1IsBlocking)
        {
            P1TakeDamage(_p2CombatSystem.p2DamageDone);
            _anim.SetTrigger("Hit");
            
            Debug.Log("player1 health:" + p1Health);
        }
        
        //if blocking and p2 normal attack block
        if (collision.CompareTag("P2WeaponTag") && p1IsBlocking && _p2CombatSystem.p2IsNormalAttack)
        {
            Debug.Log("player1 blocked the normal hit");
        }
        
        //if blocking and p2 heavy attack block
        if (collision.CompareTag("P2WeaponTag") && p1IsBlocking && _p2CombatSystem.p2IsHeavyAttack)
        {
            Debug.Log("player1 blocked the heavy hit, can't block for 3 seconds");
            p1CanBlock= false;
            blockCooldown = timer.currentTime;
        }
    }
}
