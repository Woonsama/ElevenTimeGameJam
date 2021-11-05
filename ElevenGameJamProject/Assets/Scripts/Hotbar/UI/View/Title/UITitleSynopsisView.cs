using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Hotbar.UI.View.Title
{
    public class UITitleSynopsisView : UIViewBase
    {
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
    }

}
