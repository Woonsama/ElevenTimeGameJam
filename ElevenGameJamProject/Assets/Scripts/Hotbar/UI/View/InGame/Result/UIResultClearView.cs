using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace Hotbar.UI.View.Result
{
    public class UIResultClearView : UIViewBase
    {
        public Image lightImage;
        public Image backgroundImage;
        public Image gameClearIcon;
        public InputField nameInputField;
        public Image scoreImage;
        public Text scoreText;
        public Image penguinImage;
        public Image contentsImage;
        public Button replayButton;
        public Button backButton;

        /// <summary>
        /// 게임 클리어 결과창을 띄워줌
        /// </summary>
        /// <param name="replayAction">다시 시작 버튼 클릭 시 발생하는 이벤트</param>
        /// <returns></returns>
        public async Task Show(int score, UnityAction<string> replayAction, UnityAction backAction)
        {
            scoreText.text = $"Score : {score}";

            replayButton.onClick?.AddListener(delegate 
            {
                SavePlayerInfo(score);
                replayAction?.Invoke(nameInputField.text);
                Close();
            });

            backButton.onClick?.AddListener(delegate
            {
                backAction?.Invoke();
                Close();
            });

            var tasks = new List<Task>();
            tasks.Add(backgroundImage.DOFade(0.5f, 1.0f).AsyncWaitForCompletion());
            await Task.WhenAll(tasks);
            tasks.Clear();

            tasks.Add(lightImage.DOFade(1, 0.7f).AsyncWaitForCompletion());
            tasks.Add(gameClearIcon.DOFade(1, 0.7f).AsyncWaitForCompletion());
            tasks.Add(contentsImage.DOFade(1, 0.7f).AsyncWaitForCompletion());
            tasks.Add(penguinImage.DOFade(1, 0.7f).AsyncWaitForCompletion());
            tasks.Add(scoreImage.DOFade(1, 0.7f).AsyncWaitForCompletion());
            tasks.Add(scoreText.DOFade(1, 0.7f).AsyncWaitForCompletion());
            tasks.Add(replayButton.image.DOFade(1, 0.7f).AsyncWaitForCompletion());
            tasks.Add(backButton.image.DOFade(1, 0.7f).AsyncWaitForCompletion());
            tasks.Add(nameInputField.image.DOFade(1, 0.7f).AsyncWaitForCompletion());
            tasks.Add(nameInputField.placeholder.DOFade(1, 0.7f).AsyncWaitForCompletion());


            await Task.WhenAll(tasks);
            tasks.Clear();
            tasks.Add(lightImage.transform.DOScale(2.2f, 0.3f).SetLoops(2, LoopType.Yoyo).AsyncWaitForCompletion());
            tasks.Add(gameClearIcon.transform.DOScale(1.1f, 0.3f).SetLoops(2, LoopType.Yoyo).AsyncWaitForCompletion());
            tasks.Add(contentsImage.transform.DOScale(1.1f, 0.3f).SetLoops(2, LoopType.Yoyo).AsyncWaitForCompletion());
            tasks.Add(replayButton.transform.DOScale(1.1f, 0.3f).SetLoops(2, LoopType.Yoyo).AsyncWaitForCompletion());
            tasks.Add(backButton.transform.DOScale(1.1f, 0.3f).SetLoops(2, LoopType.Yoyo).AsyncWaitForCompletion());
            tasks.Add(scoreImage.transform.DOScale(1.1f, 0.3f).SetLoops(2, LoopType.Yoyo).AsyncWaitForCompletion());
            tasks.Add(scoreText.transform.DOScale(1.1f, 0.3f).SetLoops(2, LoopType.Yoyo).AsyncWaitForCompletion());
            tasks.Add(nameInputField.transform.DOScale(1.1f, 0.3f).SetLoops(2, LoopType.Yoyo).AsyncWaitForCompletion());
            tasks.Add(nameInputField.placeholder.transform.DOScale(1.1f, 0.3f).SetLoops(2, LoopType.Yoyo).AsyncWaitForCompletion());

            await Task.WhenAll(tasks);
            tasks.Clear();
        }

        private void SavePlayerInfo(int score)
        {
            var playerNameInfoArray = PlayerPrefsX.GetStringArray("PlayerNameInfo");
            var playerScoreInfoArray = PlayerPrefsX.GetIntArray("PlayerScoreInfo");

            
            //Add Name
            var playerNameInfoList = new List<string>();
            for(int i = 0; i < playerNameInfoArray.Length; i++)
            {
                playerNameInfoList.Add(playerNameInfoArray[i]);
            }

            if(nameInputField.text == string.Empty)
            {
                playerNameInfoList.Add("Guest" + Random.Range(0, 100000));
            }
            else
            {
                playerNameInfoList.Add(nameInputField.text);
            }

            playerNameInfoArray = playerNameInfoList.ToArray();
            PlayerPrefsX.SetStringArray("PlayerNameInfo", playerNameInfoArray);

            //Add Score
            var playerScoreInfoList = new List<int>();
            for (int i = 0; i < playerScoreInfoArray.Length; i++)
            {
                playerScoreInfoList.Add(playerScoreInfoArray[i]);
            }

            playerScoreInfoList.Add(score);

            playerScoreInfoArray = playerScoreInfoList.ToArray();
            PlayerPrefsX.SetIntArray("PlayerScoreInfo", playerScoreInfoArray);
        }
    }
}


