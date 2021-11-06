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
        public Transform content;
        public Button closeButton;

        private Text[] names = new Text[20];
        private Text[] scores = new Text[20];

        public override async Task InitView()
        {
            closeButton.onClick?.RemoveAllListeners();
            closeButton.onClick?.AddListener(OnClickCloseButton);

            gameObject.SetActive(true);
            GetComponent<CanvasGroup>().DOKill();
            GetComponent<CanvasGroup>().alpha = 1.0f;

            for (int i = 0; i < 20; i++)
            {
                names[i] = content.GetChild(i).transform.GetChild(1).GetComponent<Text>();
                scores[i] = content.GetChild(i).transform.GetChild(2).GetComponent<Text>();
            }

            SetScores();
        }

        private void SetScores()
        {
            // TEST CODE
            {
                int[] testArray = new int[21];
                string[] testArray2 = new string[21];
                for (int i = 0; i < 21; i++)
                {
                    testArray[i] = 0;
                    //testArray[i] = Random.Range(0, 100);
                    testArray2[i] = "name";
                }
                //testArray2[0] = "AAA";
                //PlayerPrefsX.SetStringArray("PlayerNameInfo", testArray2);
                //PlayerPrefsX.SetIntArray("PlayerScoreInfo", testArray);
            }

            var playerNameInfoArray = PlayerPrefsX.GetStringArray("PlayerNameInfo");
            var playerScoreInfoArray = PlayerPrefsX.GetIntArray("PlayerScoreInfo");
            int temp1;
            string temp2;

            for (int i = playerScoreInfoArray.Length - 1; i > 0; i--)
            {
                for (int j = 0; j < i; j++)
                {
                    if (playerScoreInfoArray[j] < playerScoreInfoArray[j + 1])
                    {
                        temp1 = playerScoreInfoArray[j];
                        playerScoreInfoArray[j] = playerScoreInfoArray[j + 1];
                        playerScoreInfoArray[j + 1] = temp1;

                        temp2 = playerNameInfoArray[j];
                        playerNameInfoArray[j] = playerNameInfoArray[j + 1];
                        playerNameInfoArray[j + 1] = temp2;
                    }
                }
            }

            PlayerPrefsX.SetStringArray("PlayerNameInfo", playerNameInfoArray);
            PlayerPrefsX.SetIntArray("PlayerScoreInfo", playerScoreInfoArray);

            for (int i = 0; i < 20; i++)
            {
                names[i].text = playerNameInfoArray[i];
                scores[i].text = playerScoreInfoArray[i].ToString();
            }
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
