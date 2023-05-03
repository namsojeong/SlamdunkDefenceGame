using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightCycle : MonoBehaviour
{

    [SerializeField] private Gradient gradient;
    [SerializeField] private float secondsPerDay =10f;

    float dayTime;
    float dayTimeSpeed;
    Light2D light2D;

    private void Awake()
    {
        light2D= GetComponent<Light2D>();
        dayTimeSpeed = 1 / secondsPerDay;
    }
    private void Update()
    {
        dayTime += Time.deltaTime * dayTimeSpeed;
        light2D.color = gradient.Evaluate(dayTime % 1f);
    }


}
