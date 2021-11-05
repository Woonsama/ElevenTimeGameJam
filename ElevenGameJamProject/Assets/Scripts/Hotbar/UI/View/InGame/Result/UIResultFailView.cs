using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.Events;

namespace Hotbar.UI.View.Result
{
    public class UIResultFailView : UIViewBase
    {
        public Image gameOverIcon;
        public Text scoreText;
        public Text contentsText;
        public Button replayButton;

        Vector3 gameOverInitialPos;
        Vector3 scoreInitilPos;

        public async Task InitView(UnityAction replayAction)
        {
            gameOverInitialPos = gameOverIcon.transform.localPosition;
            gameOverIcon.transform.localPosition = new Vector2(Screen.width, gameOverInitialPos.y);

            scoreInitilPos = scoreText.transform.localPosition;
            scoreText.transform.localPosition = new Vector2(-Screen.width, gameOverInitialPos.y);

            contentsText.color = new Color(contentsText.color.r, contentsText.color.g, contentsText.color.b, 0);

            replayButton.onClick?.RemoveAllListeners();
            replayButton.onClick?.AddListener(replayAction);
            replayButton.enabled = false;
        }

        /// <summary>
        /// 실패 결과창을 띄워줌
        /// </summary>
        /// <param name="replayAction">다시 시작 버튼 클릭 시 발생하는 이벤트</param>
        /// <returns></returns>
        public async Task Show()
        {
            var tasks = new List<Task>();
            tasks.Add(gameOverIcon.transform.DOMove(gameOverInitialPos, 0.7f).AsyncWaitForCompletion());
            tasks.Add(scoreText.transform.DOMove(scoreInitilPos, 0.7f).AsyncWaitForCompletion());
            await Task.WhenAll(tasks);
            tasks.Clear();

            tasks.Add(contentsText.DOFade(1, 0.7f).AsyncWaitForCompletion());
            await Task.WhenAll(tasks);

            replayButton.enabled = true;
        }
    }
}
