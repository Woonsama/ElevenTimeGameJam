using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Hotbar.UI.View.Title
{
    public class UITitleSynopsisView : UIViewBase
    {
        public Text sysnopsisText;
        public Transform cloudImage;
        public Button skipButton;

        public override async Task InitView()
        {
            skipButton.onClick?.RemoveAllListeners();
            skipButton.onClick?.AddListener(OnClickSkipButton);
        }

        public override async Task UpdateView()
        {

        }

        private void OnClickSkipButton()
        {

        }

        private void OnEnable()
        {
            sysnopsisText.transform.position = new Vector3(0, 25, 0);
            sysnopsisText.transform.DOMoveY(0, 10.0f);
        }
    }

}
