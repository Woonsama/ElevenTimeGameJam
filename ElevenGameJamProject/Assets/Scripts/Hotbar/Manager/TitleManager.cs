using Hotbar.UI.View.Title;
using System.Collections;
using System.Collections.Generic;
using Tang3.Common.Management;
using UnityEngine;

public class TitleManager : SingletonMonoBase<TitleManager>
{
    public UITitleView uiTitleView;

    private async void Awake()
    {
#if UNITY_ANDROID
            int windowModeIndex = PlayerPrefs.GetInt("Window", 0);
            int resolutionModeIndex = PlayerPrefs.GetInt("Resolution", 0);
            int resolutionX = 1920, resolutionY = 1080;

            if (windowModeIndex == 0 && resolutionModeIndex == 0)
            {
                resolutionX = 1920;
                resolutionY = 1080;
            }
            else if (windowModeIndex == 0 && resolutionModeIndex == 1)
            {
                resolutionX = 1280;
                resolutionY = 720;
            }

            Screen.SetResolution(resolutionX, resolutionY, true);
#endif

        await uiTitleView.InitView();
    }
}
