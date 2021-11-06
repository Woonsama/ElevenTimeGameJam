using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Hotbar.UI.View.Title
{
    public class UITitleRankingView : UIViewBase
    {
        public Button closeButton;

        public override async Task InitView()
        {
            closeButton.onClick?.RemoveAllListeners();
            closeButton.onClick?.AddListener(OnClickCloseButton);

            gameObject.SetActive(true);
            GetComponent<CanvasGroup>().DOKill();
            GetComponent<CanvasGroup>().alpha = 1.0f;
        }

        public override async Task UpdateView()
        {

        }

        private void OnClickCloseButton()
        {
            GetComponent<CanvasGroup>().DOFade(0.0f, 0.4f)
                .OnComplete(() => gameObject.SetActive(false));
        }
    }

}
