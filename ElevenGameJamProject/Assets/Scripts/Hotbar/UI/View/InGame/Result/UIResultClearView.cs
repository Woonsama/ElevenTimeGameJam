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
        public Image backgroundImage;
        public Image gameClaerIcon;
        public Text scoreText;
        public Text contentsText;
        public InputField nameInputField;
        public Button replayButton;

        Vector3 gameClearInitialPos;
        Vector3 scoreInitilPos;

        /// <summary>
        /// 게임 클리어 결과창을 띄워줌
        /// </summary>
        /// <param name="replayAction">다시 시작 버튼 클릭 시 발생하는 이벤트</param>
        /// <returns></returns>
        public async Task Show(UnityAction<string> replayAction)
        {
            await backgroundImage.DOFade(0, 0).AsyncWaitForCompletion();
            await contentsText.DOFade(0, 0).AsyncWaitForCompletion();

            gameClearInitialPos = gameClaerIcon.transform.position;
            gameClaerIcon.transform.position = new Vector2(Screen.width * 1.5f, gameClearInitialPos.y);

            scoreInitilPos = scoreText.transform.position;
            scoreText.transform.position = new Vector2(-Screen.width * 1.5f, gameClearInitialPos.y);

            nameInputField.gameObject.SetActive(false);

            replayButton.onClick?.RemoveAllListeners();
            replayButton.onClick?.AddListener(delegate 
            {
                replayAction?.Invoke(nameInputField.text);
                Close();
            });
            replayButton.gameObject.SetActive(false);

            var tasks = new List<Task>();
            tasks.Add(backgroundImage.DOFade(0.5f, 1.0f).AsyncWaitForCompletion());
            await Task.WhenAll(tasks);
            tasks.Clear();

            tasks.Add(gameClaerIcon.transform.DOMove(gameClearInitialPos, 1.2f).AsyncWaitForCompletion());
            tasks.Add(scoreText.transform.DOMove(scoreInitilPos, 1.2f).AsyncWaitForCompletion());
            await Task.WhenAll(tasks);
            tasks.Clear();

            tasks.Add(contentsText.DOFade(1, 1.2f).AsyncWaitForCompletion());
            await Task.WhenAll(tasks);

            nameInputField.gameObject.SetActive(true);
            replayButton.gameObject.SetActive(true);
        }
    }
}


