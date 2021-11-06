using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Hotbar.UI.View.Title
{
    public class UITitleSynopsisView : UIViewBase
    {
        public Text sysnopsisText;
        public Transform cloudImage;
        public Button skipButton;

        private void Update()
        {
            cloudImage.position += Vector3.left * Time.deltaTime * 10;
            sysnopsisText.transform.position += Vector3.up * Time.deltaTime * 25;
        }

        public override async Task InitView()
        {
            skipButton.onClick?.RemoveAllListeners();
            skipButton.onClick?.AddListener(OnClickSkipButton);

            gameObject.SetActive(true);
        }

        public override async Task UpdateView()
        {

        }

        private void OnClickSkipButton()
        {
            Debug.Log("T");
            SceneManager.LoadScene(1);
        }
    }

}
