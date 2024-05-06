using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.InputSystem;
using  UnityEngine.UI;

public class Timer : MonoBehaviour
{ 
    public float currentTime = 0f;
    private float startingTime = 180f;
    
    [NonSerialized] public bool activeTimer = true;
    
    public Text timerText;
    
    void Start()
    {
        currentTime = startingTime;
    }
    
    void Update()
    {
        if (activeTimer)
        {
            currentTime -= 1 * Time.deltaTime;
        }
        
        if (currentTime <= 0)
        {
            currentTime = 0;
            activeTimer = false;
            timerText.enabled = false;
        }

        timerText.text = currentTime.ToString("000");

        if (currentTime <= 15)
        {
            timerText.color = Color.red;
        }
    }
}