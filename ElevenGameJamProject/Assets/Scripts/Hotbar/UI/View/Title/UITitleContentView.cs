using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;

namespace Hotbar.UI.View.Title
{
    public class UITitleContentView : UIViewBase
    {
        public Image titleIcon;
        public GameObject rightButtonGroup;
        public Button startButton;
        public Button rankingButton;
        public Button settingButton;
        public Button creditButton;
        public Button exitButton;

        private float tick;

        private bool animationInitEnded = false;

        private async void Start()
        {
            await InitView();
        }

        private void Update()
        {
            if(animationInitEnded)
            {
                tick -= Time.deltaTime;

                if (tick < 0)
                {
                    tick = 3.0f;
                    titleIcon.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.0f), 1.0f, 5);
                }
            }
        }

        private void OnEnable()
        {
            tick = 1.0f;
            titleIcon.transform.localScale = new Vector3(0.75f, 0.75f, 1.0f);
        }

        private void OnDisable()
        {
            titleIcon.DOKill();
        }

        public override async Task InitView()
        {
            await UniTask.NextFrame();
            AddButtonEvent();

            var tasks = new List<Task>();
            tasks.Add(titleIcon.DOFade(0, 0).AsyncWaitForCompletion());
            tasks.Add(startButton.image.DOFade(0, 0).AsyncWaitForCompletion());
            tasks.Add(rankingButton.image.DOFade(0, 0).AsyncWaitForCompletion());
            tasks.Add(settingButton.image.DOFade(0, 0).AsyncWaitForCompletion());
            tasks.Add(creditButton.image.DOFade(0, 0).AsyncWaitForCompletion());
            tasks.Add(exitButton.image.DOFade(0, 0).AsyncWaitForCompletion());
            await Task.WhenAll(tasks);
            tasks.Clear();

            tasks.Add(titleIcon.DOFade(1, 1.2f).AsyncWaitForCompletion());
            tasks.Add(startButton.image.DOFade(1, 1.2f).AsyncWaitForCompletion());
            tasks.Add(rankingButton.image.DOFade(1, 1.2f).AsyncWaitForCompletion());
            tasks.Add(settingButton.image.DOFade(1, 1.2f).AsyncWaitForCompletion());
            tasks.Add(creditButton.image.DOFade(1, 1.2f).AsyncWaitForCompletion());
            tasks.Add(exitButton.image.DOFade(1, 1.2f).AsyncWaitForCompletion());
            tasks.Add(titleIcon.transform.DOLocalMoveY(280, 1.2f).AsyncWaitForCompletion());
            tasks.Add(exitButton.transform.DOLocalMoveX(-900, 1.2f).AsyncWaitForCompletion());
            tasks.Add(rightButtonGroup.transform.DOLocalMoveX(650, 1.2f).AsyncWaitForCompletion());
            await Task.WhenAll(tasks);
            tasks.Clear();

            animationInitEnded = true;
        }

        public override async Task UpdateView()
        {
    
        }

        #region Event
        
        private async void OnClickStartButton() => await TitleManager.Instance.uiTitleView.uiTitleSynopsisView.InitView();

        private async void OnClickRankingButton() => await TitleManager.Instance.uiTitleView.uiTitleRankingView.InitView();

        private async void OnClickSettingButton()             
        {
            UISettingView SettingView = Instantiate(TitleManager.Instance.uiTitleView.uiTitleSettingViewPrefab, transform);

            await SettingView.InitView();
        }


        private async void OnClickCreditButton() => await TitleManager.Instance.uiTitleView.uiTitleCreditView.InitView();

        private void OnClickExitButton() => Application.Quit();

        #endregion

        private void AddButtonEvent()
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
    }

}
