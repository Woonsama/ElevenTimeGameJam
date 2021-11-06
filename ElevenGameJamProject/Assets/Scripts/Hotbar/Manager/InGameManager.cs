using Hotbar.UI;
using Hotbar.UI.View;
using Hotbar.UI.View.Result;
using System.Collections;
using System.Collections.Generic;
using Tang3.Common.Management;
using UnityEngine;

public class InGameManager : SingletonMonoBase<InGameManager>
{
    public UIHeaderView uiHeaderView;

    private async void Awake()
    {
        await uiHeaderView.InitView();

        var view = await UIManager.Instance.OpenView(UIManager.ViewType.Roulette);
        view.Close();

        //TimeManager.Instance.StartTimer(11, async () =>
        //{
        //    Debug.Log("Time Ended");

        //    var view = await UIManager.Instance.OpenView(UIManager.ViewType.FailResult);
        //    await ((UIResultFailView)view).Show(() => { Debug.Log("다시 시작 버튼 누름"); });
        //});
        //uiHeaderView.SetRemainPlayTime();
    }

    private void Update()
    {
        uiHeaderView.RefreshRemainPlayTime();
    }
}
