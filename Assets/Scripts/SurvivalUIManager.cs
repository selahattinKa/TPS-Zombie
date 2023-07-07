using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurvivalUIManager : MonoBehaviour
{
    [SerializeField] private SurvivalManager _survivalManager;
    [SerializeField] private Image _hungerMeter, _thirstMeter, _StaminaMeter;

    private void FixedUpdate()
    {
        _hungerMeter.fillAmount = _survivalManager.HungerPercent;
        _thirstMeter.fillAmount = _survivalManager.ThirstPercent;
        _StaminaMeter.fillAmount = _survivalManager.StaminaPercent;
    }
}
