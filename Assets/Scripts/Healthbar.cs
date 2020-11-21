using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Healthbar : MonoBehaviour {

    public Slider slider;

    public Gradient gradient;
    public Image fill;
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
     {
        StartCoroutine(reduceHealthBar(health));
        //slider.value = health;
        //fill.color = gradient.Evaluate(slider.normalizedValue);
        //for health to have a nice effect replaced this with reduceHealthBar
    }

    private IEnumerator reduceHealthBar(float health)
    {
        if (health < slider.value)
        {
            //For health to be decreased 
            for (float sliderValue = slider.value; health <= sliderValue; sliderValue -= 2)
            {
                slider.value = sliderValue;
                fill.color = gradient.Evaluate(slider.normalizedValue);
                yield return null;
            }
        }
        else
        {
            //For health to be increased 
            for (float sliderValue = slider.value; sliderValue <= health; sliderValue += 2)
            {
                slider.value = sliderValue;
                fill.color = gradient.Evaluate(slider.normalizedValue);
                yield return null;
            }
        }
        
    }

}