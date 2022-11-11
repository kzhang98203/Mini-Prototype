using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI plantDamageText;
    public int score;
    public int plantDamage;
    private void OnEnable()
    {
        CustomEventHandler<RaindropCaught>.Register(OnRaindropCaught);
        CustomEventHandler<RaindropMissed>.Register(OnRaindropMissed);
    }

    private void Awake()
    {
        scoreText.text = "Score: " + 0;
        plantDamageText.text = "Corrosion: " + 0;
    }

    private void OnDisable()
    {
        CustomEventHandler<RaindropCaught>.Unregister(OnRaindropCaught);
        CustomEventHandler<RaindropMissed>.Unregister(OnRaindropMissed);
    }

    private void OnRaindropCaught()
    {
        score += 5;
        scoreText.text = "Score: " + score;
    }

    private void OnRaindropMissed()
    {
        plantDamage += 10;
        plantDamageText.text = "Corrosion: " + plantDamage;
    }
    
}
