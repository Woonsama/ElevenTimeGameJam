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
using System;

[Serializable]
public struct LevelInfo
{
    public int squidCount;
    public int tunaCount;

    public int obstacleBananaCount;
    public int obstacleSealionCount;
    public int obstaclePuddle1Count;
    public int obstaclePuddle2Count;
    public int obstaclePuddle3Count;
}

public class InGameManager : SingletonMonoBase<InGameManager>
{
    public UIHeaderView uiHeaderView;

    int score = 0;

    [SerializeField]
    LevelInfo hard, normal, lucky;

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
                BackgroundController.Instance.groundMapManager.SquidCount = hard.squidCount;
                BackgroundController.Instance.groundMapManager.TunaCount = hard.tunaCount;

                BackgroundController.Instance.groundMapManager.ObstacleBananaCount = hard.obstacleBananaCount;
                BackgroundController.Instance.groundMapManager.ObstacleSealionCount = hard.obstacleSealionCount;
                BackgroundController.Instance.groundMapManager.ObstaclePuddle1Count = hard.obstaclePuddle1Count;
                BackgroundController.Instance.groundMapManager.ObstaclePuddle2Count = hard.obstaclePuddle2Count;
                BackgroundController.Instance.groundMapManager.ObstaclePuddle3Count = hard.obstaclePuddle3Count;

                break;
            case Hotbar.Enum.RouletteType.Lucky:
                BackgroundController.Instance.groundMapManager.SquidCount = lucky.squidCount;
                BackgroundController.Instance.groundMapManager.TunaCount = lucky.tunaCount;

                BackgroundController.Instance.groundMapManager.ObstacleBananaCount = lucky.obstacleBananaCount;
                BackgroundController.Instance.groundMapManager.ObstacleSealionCount = lucky.obstacleSealionCount;
                BackgroundController.Instance.groundMapManager.ObstaclePuddle1Count = lucky.obstaclePuddle1Count;
                BackgroundController.Instance.groundMapManager.ObstaclePuddle2Count = lucky.obstaclePuddle2Count;
                BackgroundController.Instance.groundMapManager.ObstaclePuddle3Count = lucky.obstaclePuddle3Count;

                break;
            case Hotbar.Enum.RouletteType.Normal:
                BackgroundController.Instance.groundMapManager.SquidCount = normal.squidCount;
                BackgroundController.Instance.groundMapManager.TunaCount = normal.tunaCount;

                BackgroundController.Instance.groundMapManager.ObstacleBananaCount = normal.obstacleBananaCount;
                BackgroundController.Instance.groundMapManager.ObstacleSealionCount = normal.obstacleSealionCount;
                BackgroundController.Instance.groundMapManager.ObstaclePuddle1Count = normal.obstaclePuddle1Count;
                BackgroundController.Instance.groundMapManager.ObstaclePuddle2Count = normal.obstaclePuddle2Count;
                BackgroundController.Instance.groundMapManager.ObstaclePuddle3Count = normal.obstaclePuddle3Count;

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