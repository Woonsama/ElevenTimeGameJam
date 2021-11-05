using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Hotbar.UI.View.Roulette
{
    public class UIRouletteView : UIViewBase
    {
        [Header("Contents Generate Transform")]
        public Transform contentsGenerateTransform;

        [Header("Contents")]
        public GameObject rouletteContents;

        [Header("Contents Container")]
        public List<UIRouletteContents> rouletteContainer = new List<UIRouletteContents>();

        private void SetRouletteInfo(int ruletteCount)
        {
            var contents = GenerateContents();
        }

        public async Task StartSpin()
        {

        }

        private UIRouletteContents GenerateContents() => Instantiate(rouletteContents, contentsGenerateTransform).GetComponent<UIRouletteContents>();
    }
}

