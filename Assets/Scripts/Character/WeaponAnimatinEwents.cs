using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : UnityEvent<string>
{
    
}
public class WeaponAnimatinEwents : MonoBehaviour
{
    public AnimationEvent WeaponAnimationEvent = new AnimationEvent();
    public void OnAnimationEvent(string eventName)
    {
        WeaponAnimationEvent.Invoke(eventName);
    }
}
