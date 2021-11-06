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

    int score = 0;

    private async void Awake()
    {
        await uiHeaderView.InitView();

        ////룰렛 팝업 띄우기
        var rouletteView = await UIManager.Instance.OpenView(UIManager.ViewType.Roulette);
        var mode = await((UIRouletteView)rouletteView).StartSpin();

        //받아온 난이도로 게임 설정
        switch (mode)
        {
            case Hotbar.Enum.RouletteType.Hard:
                BackgroundController.Instance.groundMapManager.SquidCount = 5;
                BackgroundController.Instance.groundMapManager.TunaCount = 5;

                BackgroundController.Instance.groundMapManager.ObstacleBananaCount = 5;
                BackgroundController.Instance.groundMapManager.ObstacleSealionCount = 4;
                BackgroundController.Instance.groundMapManager.ObstaclePuddle1Count = 1;
                BackgroundController.Instance.groundMapManager.ObstaclePuddle2Count = 0;
                BackgroundController.Instance.groundMapManager.ObstaclePuddle3Count = 0;

                break;
            case Hotbar.Enum.RouletteType.Lucky:
                BackgroundController.Instance.groundMapManager.SquidCount = 3;
                BackgroundController.Instance.groundMapManager.TunaCount = 3;

                BackgroundController.Instance.groundMapManager.ObstacleBananaCount = 3;
                BackgroundController.Instance.groundMapManager.ObstacleSealionCount = 2;
                BackgroundController.Instance.groundMapManager.ObstaclePuddle1Count = 0;
                BackgroundController.Instance.groundMapManager.ObstaclePuddle2Count = 0;
                BackgroundController.Instance.groundMapManager.ObstaclePuddle3Count = 0;

                break;
            case Hotbar.Enum.RouletteType.Normal:
                BackgroundController.Instance.groundMapManager.SquidCount = 4;
                BackgroundController.Instance.groundMapManager.TunaCount = 4;

                BackgroundController.Instance.groundMapManager.ObstacleBananaCount = 4;
                BackgroundController.Instance.groundMapManager.ObstacleSealionCount = 3;
                BackgroundController.Instance.groundMapManager.ObstaclePuddle1Count = 0;
                BackgroundController.Instance.groundMapManager.ObstaclePuddle2Count = 0;
                BackgroundController.Instance.groundMapManager.ObstaclePuddle3Count = 0;

                break;
            default:
                break;
        }

        rouletteView.Close();        
        StartGame();
    }

    void StartGame()
    {
        //타이머 시작하기
        TimeManager.Instance.StartTimer(11, async () =>
        {
            await OpenGameFailView();
        });

        uiHeaderView.SetRemainPlayTime();

        //인게임 시작
        SetScore(0);
        uiHeaderView.SetScore(this.score);

        BackgroundController.Instance.Init();
        BackgroundController.Instance.StartGame();
    }

    private void Update()
    {
        uiHeaderView.RefreshRemainPlayTime();
    }

    public void SetScore(int score)
    {
        this.score = score;
        uiHeaderView.SetScore(this.score);
    }

    public void AddScore(int score)
    {
        SetScore(this.score + score);
    }

    #region Event

    public async Task OpenGameClearView()
    {
        BackgroundController.Instance.PauseScroll();
        TimeManager.Instance.StopTimer();

        var clearView = await UIManager.Instance.OpenView(UIManager.ViewType.ClearResult);
        await ((UIResultClearView)clearView).Show(this.score, (name) =>
        {
            Debug.Log("Replay Button Click");
            StartGame();

        }, () =>
        {
            Debug.Log("Back Button Click");
            SceneManager.LoadScene("TitleScene");  //타이틀로
        });
    }

    public async Task OpenGameFailView()
    {
        BackgroundController.Instance.PauseScroll();
        TimeManager.Instance.StopTimer();

        var failView = await UIManager.Instance.OpenView(UIManager.ViewType.FailResult);
        await ((UIResultFailView)failView).Show(this.score, () =>
        {
            Debug.Log("Replay Button Click");
            StartGame();
        }, () =>
        {
            Debug.Log("Back Button Click");
            SceneManager.LoadScene("TitleScene");  //타이틀로
        });
    }

    #endregion
}