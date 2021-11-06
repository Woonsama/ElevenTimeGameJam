using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace Hotbar.UI.View
{
    public class UIInGameSettingView : UIViewBase
    {
        [Header("Window")]
        public Dropdown dropdown;

        [Header("CloseButton")]
        public Button closeButton;

        private bool isFullScreen;
        private Vector2 resolution;

        public override async Task InitView()
        {
            Time.timeScale = 0;
            dropdown.onValueChanged?.AddListener(OnValueChangedDropdownCallBack);
            closeButton.onClick?.AddListener(Close);
        }

        #region Event

        private void OnValueChangedDropdownCallBack(int index)
        {
            switch(index)
            {
                case 1: //전체 화면
                    Screen.SetResolution(Screen.width, Screen.height, true);
                    isFullScreen = true;
                    break;

                case 2: //창모드
                    Screen.SetResolution(Screen.width, Screen.height, false);
                    isFullScreen = false;

                    break;
            }
        }

        public override void Close()
        {
            Time.timeScale = 1;
            base.Close();
        }

        #endregion
    }
}
