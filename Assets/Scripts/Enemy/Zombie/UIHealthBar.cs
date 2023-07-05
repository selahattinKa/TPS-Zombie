using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Image foregrounImage;
    public Image backgrounImage;

    private void LateUpdate()
    {
        Vector3 direction = (target.position - Camera.main.transform.position).normalized;
        bool isBehind = Vector3.Dot(direction, Camera.main.transform.forward) <= 0.0f;
        foregrounImage.enabled = !isBehind;
        backgrounImage.enabled = !isBehind;
        transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
    }

    public void SetHealthBarPercentage(float percentage)
    {
        float parentWidth = GetComponent<RectTransform>().rect.width;
        float width = parentWidth * percentage;
        foregrounImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
    
}
