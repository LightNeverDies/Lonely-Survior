using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image Imghealth;
    public Text health_Text;

    public int Min;
    public int Max;

    private int mCurrentValue;

    private float mCurrentPercent;


    public void SetHealth(int health)
    {
        if (health != mCurrentValue)
        {
            if (Max - Min == 0)
            {
                mCurrentValue = 0;
                mCurrentPercent = 0;
            }
            else
            {
                mCurrentValue = health;
                mCurrentPercent = (float)mCurrentValue / (float)(Max - Min);
            }

            health_Text.text = string.Format("Health: {0} %", Mathf.RoundToInt(mCurrentPercent * 100));
            Imghealth.fillAmount = mCurrentPercent;
        }
    }
    public void SetFood(int health)
    {
        if (health != mCurrentValue)
        {
            if (Max - Min == 0)
            {
                mCurrentValue = 0;
                mCurrentPercent = 0;
            }
            else
            {
                mCurrentValue = health;
                mCurrentPercent = (float)mCurrentValue / (float)(Max - Min);
            }

            health_Text.text = string.Format("Food: {0} %", Mathf.RoundToInt(mCurrentPercent * 100));
            Imghealth.fillAmount = mCurrentPercent;
        }
    }
    public void SetWater(int health)
    {
        if (health != mCurrentValue)
        {
            if (Max - Min == 0)
            {
                mCurrentValue = 0;
                mCurrentPercent = 0;
            }
            else
            {
                mCurrentValue = health;
                mCurrentPercent = (float)mCurrentValue / (float)(Max - Min);
            }

            health_Text.text = string.Format("Water: {0} %", Mathf.RoundToInt(mCurrentPercent * 100));
            Imghealth.fillAmount = mCurrentPercent;
        }
    }

    public float CurrentPercent
    {
        get
        {
            return mCurrentPercent;
        }
    }

    public int CurrentValue
    {
        get
        {
            return mCurrentValue;
        }
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
