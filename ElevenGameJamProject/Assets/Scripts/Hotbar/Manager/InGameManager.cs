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
        //var view = await UIManager.Instance.OpenView(UIManager.ViewType.FailResult);
        //await ((UIResultFailView)view).Show(() => { Debug.Log("�ٽ� ���� ��ư ����"); });

        var view = await UIManager.Instance.OpenView(UIManager.ViewType.ClearResult);
        await ((UIResultClearView)view).Show((name) => { Debug.Log(name); });
    }
}
