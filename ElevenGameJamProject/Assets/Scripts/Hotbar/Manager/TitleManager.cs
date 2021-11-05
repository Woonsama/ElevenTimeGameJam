using Hotbar.UI.View.Title;
using System.Collections;
using System.Collections.Generic;
using Tang3.Common.Management;
using UnityEngine;

public class TitleManager : SingletonMonoBase<TitleManager>
{
    public UITitleView uiTitleView;

    private async void Awake() => await uiTitleView.InitView();
}
