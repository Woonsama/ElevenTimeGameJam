using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace Hotbar.UI.View
{
    public class UISettingView : UIViewBase
    {
        [Header("CloseButton")]
        public Button closeButton;

        [Header("Window Mode")]
        public List<string> windowModeList;
        public Text windowModeText;

        [Header("Resolution Mode")]
        public List<string> resolutionModeList;
        public Text resolutionModeText;

        [Header("Sonud Slider")]
        public Slider soundSlider;

        [Header("Save Popup")]
        public GameObject savePopup;


        private int windowModeIndex;
        private int resolutionModeIndex;
        private float soundVolume;

        private int resolutionX;
        private int resolutionY;
        private bool isFullScreen;


        public override async Task InitView()
        {
            GetSettingData();
            Time.timeScale = 0;
            closeButton.onClick?.AddListener(Close);
        }

        #region Event

        public void OnClickClose()
        {
            if(CheckClose() == true)
            {
                Close();
            }
            else
            {
                savePopup.SetActive(true);
            }
        }

        public override void Close()
        {
            Time.timeScale = 1;
            base.Close();
        }

        public void OnClickWindowLeft()
        {
            windowModeIndex = windowModeIndex == 0 ? windowModeList.Count - 1 : --windowModeIndex;
            windowModeText.text = windowModeList[windowModeIndex];
            RefreshResolutionInfo();
        }

        public void OnClickWindowRight()
        {
            windowModeIndex = windowModeIndex == windowModeList.Count - 1 ? 0 : ++windowModeIndex;
            windowModeText.text = windowModeList[windowModeIndex];
            RefreshResolutionInfo();
        }

        public void OnClickResolutionLeft()
        {
            resolutionModeIndex = resolutionModeIndex == 0 ? resolutionModeList.Count - 1 : --resolutionModeIndex;
            resolutionModeText.text = resolutionModeList[resolutionModeIndex];
            RefreshResolutionInfo();
        }

        public void OnClickResolutionRight()
        {
            resolutionModeIndex = resolutionModeIndex == resolutionModeList.Count - 1 ? 0 : ++resolutionModeIndex;
            resolutionModeText.text = resolutionModeList[resolutionModeIndex];
            RefreshResolutionInfo();
        }

        public void OnClickSave()
        {
            SetLocalSettingData();
            Close();
        }

        public void OnClickNotSave()
        {
            Close();
        }

        #endregion

        #region Private

        private bool CheckClose()
        {
            if (windowModeIndex == PlayerPrefs.GetInt("Window") &&
                resolutionModeIndex == PlayerPrefs.GetInt("Resolution") &&
                soundVolume == PlayerPrefs.GetFloat("Sound"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void GetSettingData()
        {
            windowModeIndex = PlayerPrefs.GetInt("Window");
            resolutionModeIndex = PlayerPrefs.GetInt("Resolution");
            soundVolume = PlayerPrefs.GetFloat("Sound");

            windowModeText.text = windowModeList[windowModeIndex];
            resolutionModeText.text = resolutionModeList[resolutionModeIndex];
            soundSlider.value = soundVolume;
            RefreshResolutionInfo();
        }

        private void SetLocalSettingData()
        {
            PlayerPrefs.SetInt("Window", windowModeIndex);
            PlayerPrefs.SetInt("Resolution", resolutionModeIndex);
            PlayerPrefs.SetFloat("Sound", soundVolume);

            Screen.SetResolution(resolutionX, resolutionY, isFullScreen);
        }

        private void RefreshResolutionInfo()
        {
            if (windowModeIndex == 0 && resolutionModeIndex == 0)
            {
                resolutionX = 1920;
                resolutionY = 1080;
                isFullScreen = true;
            }
            else if (windowModeIndex == 0 && resolutionModeIndex == 1)
            {
                resolutionX = 1280;
                resolutionY = 720;
                isFullScreen = true;
            }
            else if (windowModeIndex == 1 && resolutionModeIndex == 0)
            {
                resolutionX = 1920;
                resolutionY = 1080;
                isFullScreen = false;
            }
            else if (windowModeIndex == 1 && resolutionModeIndex == 1)
            {
                resolutionX = 1280;
                resolutionY = 720;
                isFullScreen = false;
            }
        }

        #endregion
    }
}
