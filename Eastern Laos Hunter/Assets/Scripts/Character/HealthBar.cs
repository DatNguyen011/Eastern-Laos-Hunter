using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Color low = Color.red;
    public Color high = Color.green;
    //public Image fill;
    //public Gradient gradient;
    public Vector3 offset;
    public Image fillHealth;
    public Image fillMana;

    private void Update()
    {
        if (slider != null)
        {
            slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position+offset);
        }
    }

    public void SetHealth(float maxHp, float hp)
    {
        slider.gameObject.SetActive(hp<maxHp);
        slider.maxValue = maxHp;
        slider.value = hp;
        //fill.color = gradient.Evaluate(slider.normalizedValue);
        slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low,high,slider.normalizedValue);
        
        
    }

    public void SetHealthByImage(float maxHp, float hp)
    {
        hp = Mathf.Clamp(hp, 0, maxHp);
        fillHealth.fillAmount = hp/maxHp;
        PlayerPrefs.SetFloat("hp",hp);
        PlayerPrefs.SetFloat("maxhp",maxHp);
        PlayerPrefs.Save();
    }

    public void SetManaByImage(float maxMp, float mp)
    {
        mp = Mathf.Clamp(mp, 0, maxMp);
        fillMana.fillAmount = mp / maxMp;
        PlayerPrefs.SetFloat("mp", mp);
        PlayerPrefs.SetFloat("maxmp", maxMp);
        PlayerPrefs.Save();
    }

}
