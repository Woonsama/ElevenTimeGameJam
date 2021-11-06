using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tang3.Common.Management;
using UnityEngine;
using UnityEngine.Events;

public class TimeManager : SingletonMonoBase<TimeManager>
{
    public float currentTime = 0;
    public bool IsEndTimer = true;

    public void StartTimer(float second, UnityAction timerEndAction = null)
    {
        MeasureBySecond(second);
        //MeasureByMilliSecond(second);
    }
    
    public async void MeasureBySecond(float second, UnityAction timerEndAction = null)
    {
        currentTime = second;
        IsEndTimer = false;

        for (int i = 0; i < second; i++)
        {
            await UniTask.Delay(1000);
            currentTime--;
        }

        currentTime = 0;
        IsEndTimer = true;

        timerEndAction?.Invoke();
    }

    public async void MeasureByMilliSecond(float second, UnityAction timerEndAction = null)
    {
        currentTime = second;
        IsEndTimer = false;

        while (currentTime <= 0)
        {
            currentTime -= Time.deltaTime;
            await UniTask.NextFrame();
        }

        currentTime = 0;
        IsEndTimer = true;
        timerEndAction?.Invoke();
    }
}
