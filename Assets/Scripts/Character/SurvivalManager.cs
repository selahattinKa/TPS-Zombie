using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SurvivalManager : MonoBehaviour
{

    [Header("Hunger")] 
    [SerializeField] private float _maxHunger = 100f;
    [Tooltip("Dayanıklılık tükenme oranı")]
    [SerializeField] private float _hungerDepletionRate = 1f;
    [Tooltip("Mevcut Açlık")]
    private float _currentHunger;
    [Tooltip("Açlık yüzdeliği hesaplama")]
    public float HungerPercent => _currentHunger / _maxHunger;
    
    [Header("Thirst")] 
    [SerializeField] private float _maxThirst = 100f;
    [Tooltip("susuzluk tükenme oranı")]
    [SerializeField] private float _thirstDepletionRate = 1f;
    [Tooltip("Mevcut susuzluk")]
    private float _currentThirst;
    [Tooltip("susuzluk yüzdeliği hesaplama")]
    public float ThirstPercent => _currentThirst / _maxThirst;

    [Header("Stamina")]
    [SerializeField] private float _maxStamina;
    [Tooltip("Stamina Tükenme Oranı")]
    [SerializeField] private float _staminaDepletionRate = 1f; 
    [Tooltip("Stamina Zıplarken Tükenme Oranı")]
    [SerializeField] private float _staminaJumpingDepletionRate = 1f;
    //[Tooltip("Stamina yeniden dolum oranı")]
    //[SerializeField] private float _staminaRechargeRate = 2f;
    [Tooltip("Stamina Yeniden dolum gecikmesi")]
    [SerializeField] private float _staminaRechargeDelay = 1f;
    [Tooltip("Mevcut stamina")]
    private float _currentStamina;
    [Tooltip("Mevcut stamina gecikme sayacı")]
    private float _currentStaminaDelayCounter;
    [Tooltip("Stamina yüzdeliği hesaplama")]
    public float StaminaPercent => _currentStamina / _maxStamina;

    public bool HasStamina = true;
    private bool isJumping = false;

    [Header("Player References")]
    private CharacterLocomotion locomotion;

    public static UnityAction OnPlayerDied;

    private void Start()
    {
        _currentHunger = _maxHunger;
        _currentStamina = _maxStamina;
        _currentThirst = _maxThirst;
        locomotion = GetComponent<CharacterLocomotion>();

    }

    private void Update()
    {
        _currentHunger -= _hungerDepletionRate * Time.deltaTime;
        _currentThirst -= _thirstDepletionRate * Time.deltaTime;
        if (_currentHunger <= 0 || _currentThirst <= 0)
        {
            OnPlayerDied?.Invoke();
            _currentHunger = 0;
            _currentThirst = 0;
        }

        if (locomotion.isSprintings)
        {
            _currentStamina -= _staminaDepletionRate * Time.deltaTime;
            if (_currentStamina <= 0)
            {
                _currentStamina = 0;
                HasStamina = false;
            }
            _currentStaminaDelayCounter = 0;
        }

        if (locomotion.isJumping & !isJumping)
        {
            isJumping = true;

            _currentStamina -= _staminaJumpingDepletionRate;
            if (_currentStamina <= 0)
            {
                _currentStamina = 0;
                HasStamina = false;
            }
            _currentStaminaDelayCounter = 0;
            
        }
        else if (!locomotion.isJumping && isJumping)
        {
            // isJumping değeri false ve bir önceki isJumping değeri true ise çalışacak olan kısım
            isJumping = false;
        }

        //karakter koşmuyorsa ve stamina max değilse
        if (!locomotion.isSprintings && _currentStamina < _maxStamina)
        {
            //dayanıklılık gecikme sayacı, yeniden doldurma gecikmesinden küçükse mevcut staminayı arttır
            if (_currentStaminaDelayCounter < _staminaRechargeDelay)
            {
                _currentStaminaDelayCounter += Time.deltaTime;
                HasStamina = true;
            }
            if (_currentStaminaDelayCounter >= _staminaRechargeDelay)
            {
                _currentStamina += _staminaDepletionRate * Time.deltaTime;
                if (_currentStamina > _maxStamina) _currentStamina = _maxStamina;
            }
        }
        
    }

    public void ReplenishHungerThirst(float hungerAmount, float thirstAmount)
    {
        _currentHunger += hungerAmount;
        _currentThirst += thirstAmount;
        if (_currentHunger > _maxHunger) _currentHunger = _maxHunger;
        if (_currentThirst > _maxThirst) _currentThirst = _maxThirst;
    }
}
