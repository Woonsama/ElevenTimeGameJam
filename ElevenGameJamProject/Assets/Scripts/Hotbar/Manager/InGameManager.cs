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

        ////°ÔÀÓ Å¬¸®¾î ÆË¾÷ ¶ç¿ì±â
        //var clearView = await UIManager.Instance.OpenView(UIManager.ViewType.ClearResult);
        //await ((UIResultClearView)clearView).Show(500, (name) =>
        // {
        //     Debug.Log("Replay Button Click");
        // }, () =>
        // {
        //     Debug.Log("Back Button Click");
        // });

        ////·ê·¿ ÆË¾÷ ¶ç¿ì±â
        //var rouletteView = await UIManager.Instance.OpenView(UIManager.ViewType.Roulette);
        //rouletteView.Close();

        ////Å¸ÀÌ¸Ó ½ÃÀÛÇÏ±â
        //TimeManager.Instance.StartTimer(11, async () =>
        //{
        //    //½Ã°£ ÃÊ°ú ÆË¾÷ ¶ç¿ì±â
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
