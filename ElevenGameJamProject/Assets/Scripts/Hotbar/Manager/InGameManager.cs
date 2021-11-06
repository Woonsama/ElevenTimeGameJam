using Hotbar.UI;
using Hotbar.UI.View;
using Hotbar.UI.View.Result;
using Hotbar.UI.View.Roulette;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tang3.Common.Management;
using UnityEngine;
using eleven.game;
using UnityEngine.SceneManagement;

public class InGameManager : SingletonMonoBase<InGameManager>
{
    public UIHeaderView uiHeaderView;

    private async void Awake()
    {
        await uiHeaderView.InitView();



        ////�귿 �˾� ����
        var rouletteView = await UIManager.Instance.OpenView(UIManager.ViewType.Roulette);
        var mode = await((UIRouletteView)rouletteView).StartSpin();

        //�޾ƿ� ���̵��� ���� ����
        switch (mode)
        {
            case Hotbar.Enum.RouletteType.Hard:
                break;
            case Hotbar.Enum.RouletteType.Lucky:
                break;
            case Hotbar.Enum.RouletteType.Normal:
                break;
            default:
                break;
        }

        rouletteView.Close();

        StartGame();
    }

    void StartGame()
    {
        //Ÿ�̸� �����ϱ�
        TimeManager.Instance.StartTimer(11, async () =>
        {
            await OpenGameFailView();
        });

        uiHeaderView.SetRemainPlayTime();

        //�ΰ��� ����
        BackgroundController.Instance.Init();
        BackgroundController.Instance.StartGame();
    }

    private void Update()
    {
        uiHeaderView.RefreshRemainPlayTime();
    }

    #region Event

    public async Task OpenGameClearView()
    {
        BackgroundController.Instance.PauseScroll();
        TimeManager.Instance.StopTimer();

        var clearView = await UIManager.Instance.OpenView(UIManager.ViewType.ClearResult);
        await ((UIResultClearView)clearView).Show(500, (name) =>
        {
            Debug.Log("Replay Button Click");
            StartGame();

        }, () =>
        {
            Debug.Log("Back Button Click");
            SceneManager.LoadScene("TitleScene");  //Ÿ��Ʋ��
        });
    }

    public async Task OpenGameFailView()
    {
        BackgroundController.Instance.PauseScroll();
        TimeManager.Instance.StopTimer();

        var failView = await UIManager.Instance.OpenView(UIManager.ViewType.FailResult);
        await ((UIResultFailView)failView).Show(500, () =>
        {
            Debug.Log("Replay Button Click");
            StartGame();
        }, () =>
        {
            Debug.Log("Back Button Click");
            SceneManager.LoadScene("TitleScene");  //Ÿ��Ʋ��
        });
    }

    #endregion
}