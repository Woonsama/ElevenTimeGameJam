using Hotbar.UI;
using Hotbar.UI.View.Result;
using System.Collections;
using System.Collections.Generic;
using Tang3.Common.Management;
using UnityEngine;

public class InGameManager : SingletonMonoBase<InGameManager>
{
    private async void Awake()
    {
        var view = await UIManager.Instance.OpenView(UIManager.ViewType.Result);
        await ((UIResultFailView)view).Show(() => { Debug.Log("다시 시작 버튼 누름"); });
    }
}
