using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNight : MonoBehaviour
{
    [SerializeField]
    private float _dayLenght = 0.5f;
    public Text day;
    [SerializeField]
    private Transform dailyRotation;
    [SerializeField]
    private Light sun;
    private float intensity;
    [SerializeField]
    private float sunBaseIntensity = 1f;
    [SerializeField]
    private float sunVariation = 1.5f;

    [SerializeField]
    private Gradient sunColor;

    [Header("Modules")]
    private List<DN_Modulebase> modulList = new List<DN_Modulebase>();

    public float dayLenght
    {
        get
        {
            return _dayLenght;
        }
    }
    [SerializeField]
    [Range(0f, 1f)]
    private float _timeOfDay;
    public float timeOfDay
    {
        get
        {
            return _timeOfDay;
        }
    }
    [SerializeField]
    private int _dayNumber = 0;
    public int dayNumber
    {
        get
        {
            return _dayNumber; 
        }
    }
    private int _yearNumber = 0;
    public int yearNumber
    {
        get
        {
            return _yearNumber;
        }
    }
    private float _timeScale = 100;
    [SerializeField]
    private int _yearLenght = 100;
    public float yearLenght
    {
        get
        {
            return _yearLenght;
        }
    }
    public bool pause = false;

    private void Update()
    {
        if(!pause)
        {
            UpdateTimeScale();
                UpdateTime();
        }
        SunRotation();
        SunIntensity();
        AjdustSunColor();
        UpdateModules();
    }

    private void UpdateTimeScale()
    {
        _timeScale = 24 / (dayLenght / 60);
    }
    
    private void UpdateTime()
    {
        _timeOfDay += Time.deltaTime * _timeScale / 86400;
        if(_timeOfDay >1)
        {
            _dayNumber++;
            _timeOfDay -= 1;
            day.text = ("Days:" + _dayNumber);
            if(_dayNumber > _yearLenght)
            {
                _yearNumber++;
                _dayNumber = 0;
            }
        }
    }

    private void SunRotation()
    {
        float sunAngle = timeOfDay * 360f;
        dailyRotation.transform.localRotation = Quaternion.Euler(new Vector3(0f,0f,sunAngle));
    }

    private void SunIntensity()
    {
        intensity = Vector3.Dot(sun.transform.forward, Vector3.down);
        intensity = Mathf.Clamp01(intensity);

        sun.intensity = intensity * sunVariation + sunBaseIntensity;
    }
    
    private void AjdustSunColor()
    {
        sun.color = sunColor.Evaluate(intensity);
    }
    public void AddModule(DN_Modulebase module)
    {
        modulList.Add(module);
    }
    public void RemoveModule(DN_Modulebase module)
    {
        modulList.Remove(module);
    }

    private void UpdateModules()
    {
        foreach (DN_Modulebase module in modulList)
        {
            module.UpdateModule(intensity);
        }
    }
}
