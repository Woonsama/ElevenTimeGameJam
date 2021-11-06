using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace Hotbar.UI.View
{
    public class UIHeaderView : UIViewBase
    {
        public Text remainPlayTimeText;
        public Text scoreText;
        public Button settingButton;

        public override async Task InitView()
        {
            
        }

        public void SetRemainPlayTime() => remainPlayTimeText.text = string.Format("{0}", TimeManager.Instance.currentTime.ToString("n2"));

        public void RefreshRemainPlayTime() => remainPlayTimeText.text = string.Format("{0}", TimeManager.Instance.currentTime.ToString("n2"));

        public void SetScore(int score) => scoreText.text = string.Format("{0}", score);

        public void StartScoreUpEffect()
        {
            scoreText.DOKill();
            scoreText.transform.DOScale(1.2f, 0.2f).SetLoops(2, LoopType.Yoyo);
        }
    }
}
