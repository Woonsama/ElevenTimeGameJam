using Hotbar.UI;
using Hotbar.UI.View;
using Hotbar.UI.View.Result;
using Hotbar.UI.View.Roulette;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tang3.Common.Management;
using UnityEngine;

public class InGameManager : SingletonMonoBase<InGameManager>
{
    public UIHeaderView uiHeaderView;

    private async void Awake()
    {
        await uiHeaderView.InitView();

        ////룰렛 팝업 띄우기
        var rouletteView = await UIManager.Instance.OpenView(UIManager.ViewType.Roulette);
        var mode = await ((UIRouletteView)rouletteView).StartSpin();

        //받아온 난이도로 게임 설정

        rouletteView.Close();

        //타이머 시작하기
        TimeManager.Instance.StartTimer(11, async () =>
        {
            await OpenGameFailView();
        });

        uiHeaderView.SetRemainPlayTime();

        //인게임 시작
    }

    private void Update()
    {
        uiHeaderView.RefreshRemainPlayTime();
    }

    #region Event

    public async Task OpenGameClearView()
    {
        var clearView = await UIManager.Instance.OpenView(UIManager.ViewType.ClearResult);
        await ((UIResultClearView)clearView).Show(500, (name) =>
        {
            Debug.Log("Replay Button Click");
        }, () =>
        {
            Debug.Log("Back Button Click");
        });
    }

    public async Task OpenGameFailView()
    {
        var failView = await UIManager.Instance.OpenView(UIManager.ViewType.FailResult);
        await ((UIResultFailView)failView).Show(500, () =>
        {
            Debug.Log("Replay Button Click");
        }, () =>
        {
            Debug.Log("Back Button Click");
        });
    }

    #endregion
}