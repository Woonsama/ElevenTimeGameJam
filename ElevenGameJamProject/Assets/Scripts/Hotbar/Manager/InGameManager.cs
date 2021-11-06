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

        //���� Ŭ���� �˾� ����
        //var view = await UIManager.Instance.OpenView(UIManager.ViewType.ClearResult);
        //await ((UIResultClearView)view).Show(500,(name) =>
        //{
        //    Debug.Log("Replay Button Click");
        //}, () =>
        //{
        //    Debug.Log("Back Button Click");
        //});


        //�ð� �ʰ� �˾� ����
        //var view = await UIManager.Instance.OpenView(UIManager.ViewType.FailResult);
        //await ((UIResultFailView)view).Show(500, () =>
        //{
        //    Debug.Log("Replay Button Click");
        //}, () =>
        //{
        //    Debug.Log("Back Button Click");
        //});

        //�귿 �˾� ����
        //var view = await UIManager.Instance.OpenView(UIManager.ViewType.Roulette);
        //view.Close();

        //Ÿ�̸� �����ϱ�
        //TimeManager.Instance.StartTimer(11, async () =>
        //{
        //    Debug.Log("Time Ended");

        //    var view = await UIManager.Instance.OpenView(UIManager.ViewType.FailResult);
        //    await ((UIResultFailView)view).Show(() => { Debug.Log("�ٽ� ���� ��ư ����"); });
        //});
        //uiHeaderView.SetRemainPlayTime();
    }

    private void Update()
    {
        uiHeaderView.RefreshRemainPlayTime();
    }
}
