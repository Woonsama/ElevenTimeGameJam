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


        public override async Task InitView()
        {
            await uiTitleContentView.InitView();
        }

        public override async Task UpdateView()
        {

        }
    }
}

