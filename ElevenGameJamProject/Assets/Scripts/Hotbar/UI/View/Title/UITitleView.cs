using Hotbar.UI;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Hotbar.UI.View.Title
{
    public class UITitleView : UIViewBase
    {
        [Header("UITitleContentView")] public UITitleContentView uiTitleContentView;
        [Header("UITitleContentView")] public UITitleSynopsisView uiTitleSynopsisView;
        [Header("UITitleContentView")] public UITitleSynopsisView uiTitleRankingView;
        [Header("UITitleContentView")] public UITitleSynopsisView uiTitleSettingView;
        [Header("UITitleContentView")] public UITitleSynopsisView uiTitleCreditView;


        public override async Task InitView()
        {
            await uiTitleContentView.InitView();
        }

        public override async Task UpdateView()
        {

        }
    }
}

