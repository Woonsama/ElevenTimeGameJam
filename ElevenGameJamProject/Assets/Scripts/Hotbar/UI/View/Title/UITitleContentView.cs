using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Hotbar.UI.View.Title
{
    public class UITitleContentView : UIViewBase
    {
        public Image titleIcon;
        public Button startButton;
        public Button rankingButton;
        public Button settingButton;
        public Button creditButton;
        public Button exitButton;

        public override async Task InitView()
        {
            startButton.onClick?.RemoveAllListeners();
            startButton.onClick?.AddListener(OnClickStartButton);

            rankingButton.onClick?.RemoveAllListeners();
            rankingButton.onClick?.AddListener(OnClickRankingButton);

            settingButton.onClick?.RemoveAllListeners();
            settingButton.onClick?.AddListener(OnClickSettingButton);

            creditButton.onClick?.RemoveAllListeners();
            creditButton.onClick?.AddListener(OnClickCreditButton);

            exitButton.onClick?.RemoveAllListeners();
            exitButton.onClick?.AddListener(OnClickExitButton);
        }
    
        public override async Task UpdateView()
        {
    
        }

        #region Event
        
        private async void OnClickStartButton() => await TitleManager.Instance.uiTitleView.uiTitleSynopsisView.InitView();

        private void OnClickRankingButton()
        {

        }

        private void OnClickSettingButton()
        {

        }

        private void OnClickCreditButton()
        {

        }

        private void OnClickExitButton() => Application.Quit();

        #endregion
    }

}
