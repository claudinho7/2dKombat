using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class UI_Handeler : MonoBehaviour
{
    [SerializeField] private CombatSystem player1;
    [SerializeField] private P2CombatSystem player2;
    
    [SerializeField] private Image P1HealthBar;
    [SerializeField] private Image P1Life1;
    [SerializeField] private Image P1Life2;
    [SerializeField] private Image P1Life3;
    [SerializeField] private Image P2HealthBar;
    [SerializeField] private Image P2Life1;
    [SerializeField] private Image P2Life2;
    [SerializeField] private Image P2Life3;
    
    [SerializeField] private GameObject RoundWonP1;
    [SerializeField] private GameObject GameWonP1;
    [SerializeField] private GameObject RoundWonP2;
    [SerializeField] private GameObject GameWonP2;

    public Timer timer;
    private bool isDone = false;
    [NonSerialized] public bool canPlay = true;

    void Start()
    {
        P1HealthBar.fillAmount = player1.p1Health /10;
        P2HealthBar.fillAmount = player2.p2Health /10;
    }

    void Update()
    {
        P1HealthBar.fillAmount = player1.p1Health / 10;
        P2HealthBar.fillAmount = player2.p2Health /10;

        //showing the life images
        if (CombatSystem.p1lifes == 3)
        {
            P1Life1.enabled = true;
            P1Life2.enabled = true;
            P1Life3.enabled = true;
        }
        else if (CombatSystem.p1lifes == 2)
        {
            P1Life1.enabled = true;
            P1Life2.enabled = true;
            P1Life3.enabled = false;
        }
        else if (CombatSystem.p1lifes == 1)
        {
            P1Life1.enabled = true;
            P1Life2.enabled = false;
            P1Life3.enabled = false;
        }
        else if (CombatSystem.p1lifes == 0)
        {
            P1Life1.enabled = false;
            P1Life2.enabled = false;
            P1Life3.enabled = false;
        }
        
        if (P2CombatSystem.p2lifes == 3)
        {
            P2Life1.enabled = true;
            P2Life2.enabled = true;
            P2Life3.enabled = true;
        }
        else if (P2CombatSystem.p2lifes == 2)
        {
            P2Life1.enabled = true;
            P2Life2.enabled = true;
            P2Life3.enabled = false;
        } 
        else if (P2CombatSystem.p2lifes == 1)
        {
            P2Life1.enabled = true;
            P2Life2.enabled = false;
            P2Life3.enabled = false;
        }
        else if (P2CombatSystem.p2lifes == 0)
        {
            P2Life1.enabled = false;
            P2Life2.enabled = false;
            P2Life3.enabled = false;
        }
        

        //showing the roundwon images
        if (player1.p1Health <= 0 && CombatSystem.p1lifes > 0)
        {
            RoundWonP2.SetActive(true);
            timer.activeTimer = false;
        }
        else if (player1.p1Health <= 0 && CombatSystem.p1lifes <= 0)
        {
            GameWonP2.SetActive(true);
            timer.activeTimer = false;
        }
        else if (player1.p1Health > 0 || CombatSystem.p1lifes > 0)
        {
            RoundWonP2.SetActive(false);
            GameWonP2.SetActive(false);
        }
        
        if (player2.p2Health <= 0 && P2CombatSystem.p2lifes > 0)
        {
            RoundWonP1.SetActive(true);
            timer.activeTimer = false;
        }
        else if (player2.p2Health <= 0 && P2CombatSystem.p2lifes <= 0)
        {
            GameWonP1.SetActive(true);
            timer.activeTimer = false;
        }
        else if (player2.p2Health > 0 || P2CombatSystem.p2lifes > 0)
        {
            RoundWonP1.SetActive(false);
            GameWonP1.SetActive(false);
        }

        //when timer gets to 0 show roundwon image and -life
        if (timer.currentTime == 0) 
        {
            if (player1.p1Health > player2.p2Health)
            {
                RoundWonP1.SetActive(true);
                canPlay = false;
                
                if (!isDone)
                {
                    P2CombatSystem.p2lifes -= 1;
                    isDone = true;
                }
            }
            else if (player2.p2Health > player1.p1Health)
            { 
                RoundWonP2.SetActive(true);
                canPlay = false;

                if (!isDone)
                {
                    CombatSystem.p1lifes -= 1;
                    isDone = true;
                }
            }
        }

        if (player1.p1Health <=0 || player2.p2Health <= 0)
        {
            canPlay = false;
        }
    }
}
