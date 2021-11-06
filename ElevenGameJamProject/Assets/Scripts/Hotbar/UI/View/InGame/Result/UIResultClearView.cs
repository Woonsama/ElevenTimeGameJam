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
        public Image gameClearIcon;
        public InputField nameInputField;
        public Image scoreImage;
        public Text scoreText;
        public Image penguinImage;
        public Image contentsImage;
        public Button replayButton;
        public Button backButton;

        /// <summary>
        /// ���� Ŭ���� ���â�� �����
        /// </summary>
        /// <param name="replayAction">�ٽ� ���� ��ư Ŭ�� �� �߻��ϴ� �̺�Ʈ</param>
        /// <returns></returns>
        public async Task Show(UnityAction<string> replayAction, UnityAction backAction)
        {
            replayButton.onClick?.AddListener(delegate 
            {
                replayAction?.Invoke(nameInputField.text);
                Close();
            });

            backButton.onClick?.AddListener(delegate
            {
                backAction?.Invoke();
            });

            //var tasks = new List<Task>();
            //tasks.Add(backgroundImage.DOFade(0.5f, 1.0f).AsyncWaitForCompletion());
            //await Task.WhenAll(tasks);
            //tasks.Clear();

            //tasks.Add(gameClaerIcon.transform.DOMove(gameClearInitialPos, 1.2f).AsyncWaitForCompletion());
            //tasks.Add(scoreText.transform.DOMove(scoreInitilPos, 1.2f).AsyncWaitForCompletion());
            //await Task.WhenAll(tasks);
            //tasks.Clear();

            //tasks.Add(contentsText.DOFade(1, 1.2f).AsyncWaitForCompletion());
            //await Task.WhenAll(tasks);

            //nameInputField.gameObject.SetActive(true);
            //replayButton.gameObject.SetActive(true);
        }
    }
}


