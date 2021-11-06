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

        ////���� Ŭ���� �˾� ����
        //var clearView = await UIManager.Instance.OpenView(UIManager.ViewType.ClearResult);
        //await ((UIResultClearView)clearView).Show(500, (name) =>
        // {
        //     Debug.Log("Replay Button Click");
        // }, () =>
        // {
        //     Debug.Log("Back Button Click");
        // });

        ////�귿 �˾� ����
        //var rouletteView = await UIManager.Instance.OpenView(UIManager.ViewType.Roulette);
        //rouletteView.Close();

        ////Ÿ�̸� �����ϱ�
        //TimeManager.Instance.StartTimer(11, async () =>
        //{
        //    //�ð� �ʰ� �˾� ����
        //    var failView = await UIManager.Instance.OpenView(UIManager.ViewType.FailResult);
        //    await ((UIResultFailView)failView).Show(500, () =>
        //    {
        //        Debug.Log("Replay Button Click");
        //    }, () =>
        //    {
        //        Debug.Log("Back Button Click");
        //    });
        //});
        //uiHeaderView.SetRemainPlayTime();
    }

    private void Update()
    {
        uiHeaderView.RefreshRemainPlayTime();
    }
}
