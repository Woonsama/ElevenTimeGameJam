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

        public override async Task InitView()
        {
            Time.timeScale = 0;
            closeButton.onClick?.AddListener(Close);
        }

        #region Event

        public override void Close()
        {
            Time.timeScale = 1;
            base.Close();
        }

        #endregion
    }
}
