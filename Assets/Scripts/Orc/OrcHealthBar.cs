using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrcHealthBar : MonoBehaviour
{
    public Image OrcHealth;

    private int mCurrentValue;

    private float mCurrentPercent;

    public int Min;
    public int Max;

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
            OrcHealth.fillAmount = mCurrentPercent;
        }
    }

}
