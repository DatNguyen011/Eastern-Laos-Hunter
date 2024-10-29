using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Color low = Color.red;
    public Color high = Color.green;

    public Vector3 offset;

    private void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position+offset);
    }

    public void SetHealth(float maxHp, float hp)
    {
       
        slider.gameObject.SetActive(hp<maxHp);
        slider.maxValue = maxHp;
        slider.value = hp;
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low,high,slider.normalizedValue);
        
    }

}
