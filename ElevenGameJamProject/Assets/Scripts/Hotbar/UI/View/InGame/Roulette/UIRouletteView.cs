using DG.Tweening;
using Hotbar.Enum;
using Hotbar.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Hotbar.UI.View.Roulette
{
    public class UIRouletteView : UIViewBase
    {
        #region Member

        [Header("Roulette Pattern")]
        public List<UniTuple<float, float, float>> rouletteFillPattern = new List<UniTuple<float, float, float>>();

        [Header("Roulette Start Transform")]
        public RectTransform rouletteStartTransform;

        [Header("Roulette")]
        public Transform roulette;

        [Header("Roulette Group")]
        public Transform rouletteGroup;

        [Header("BackgroundImage")]
        public Image backgroundImage;

        [Header("Fill")]
        public Image hardFill;
        public Image luckyFill;
        public Image normalFill;

        [Header("Roulette Result Image")]
        public Image resultImage;

        [Header("Roulette Result Sprite")]
        public Sprite hardSprite;
        public Sprite luckySprite;
        public Sprite normalSprite;

        [Header("Roulette Loop Count")]
        public int loopCount;

        private readonly int angle = 360;
        private UniTuple<float, float, float> currentPattern;

        #endregion

        public override async Task InitView() => await StartSpin();

        public async Task<RouletteType> StartSpin() => await StartSpinAnimation();

        private UniTuple<float, float, float> ChoosePattern() => rouletteFillPattern[Random.Range(0, rouletteFillPattern.Count)];

        private async Task<RouletteType> StartSpinAnimation()
        {
            SetFill();

            var randomFill = Random.Range(0f, 1.0f);
            RouletteType result = GetRouletteResult(randomFill);
            Debug.Log(result);

            var tasks = new List<Task>();
            tasks.Add(backgroundImage.DOFade(0.5f, 0.7f).AsyncWaitForCompletion());
            await Task.WhenAll(tasks);
            tasks.Clear();

            tasks.Add(roulette.DOMove(new Vector3(Screen.width / 2, Screen.height / 2), 1.0f).AsyncWaitForCompletion());
            await Task.WhenAll(tasks);
            tasks.Clear();

            var addAngle = randomFill * 360;
            var rouletteGroupVec = new Vector3(rouletteGroup.rotation.x, rouletteGroup.rotation.y, -angle - addAngle);
            tasks.Add(rouletteGroup.DORotate(rouletteGroupVec, 0.1f, RotateMode.LocalAxisAdd).SetLoops(loopCount, LoopType.Incremental).SetEase(Ease.OutQuad, 2).AsyncWaitForCompletion());
            await Task.WhenAll(tasks);
            tasks.Clear();

            await Task.Delay(500);

            tasks.Add(resultImage.transform.DOScale(1.7f, 0.8f).AsyncWaitForCompletion());
            await Task.WhenAll(tasks);
            tasks.Clear();

            await Task.Delay(300);

            tasks.Add(resultImage.transform.DOScale(1.5f, 0.5f).SetLoops(6, LoopType.Yoyo).AsyncWaitForCompletion());
            await Task.WhenAll(tasks);
            tasks.Clear();


            tasks.Add(roulette.DOMove(rouletteStartTransform.position, 1.0f).AsyncWaitForCompletion());
            await Task.WhenAll(tasks);
            tasks.Clear();

            ResetInfo();

            return result;
        }

        private void SetFill()
        {
            currentPattern = ChoosePattern();
            hardFill.fillAmount = currentPattern.Item1;
            luckyFill.fillAmount = currentPattern.Item2;
            normalFill.fillAmount = currentPattern.Item3;

            luckyFill.transform.rotation = Quaternion.Euler(new Vector3(luckyFill.transform.rotation.x, luckyFill.transform.rotation.y, angle * luckyFill.fillAmount));
            normalFill.transform.rotation = Quaternion.Euler(new Vector3(normalFill.transform.rotation.x, normalFill.transform.rotation.y, angle * luckyFill.fillAmount + angle * normalFill.fillAmount));
        }

        private void ResetInfo()
        {
            roulette.transform.position = rouletteStartTransform.position;
            backgroundImage.DOFade(0f, 0.7f);
            resultImage.transform.DOScale(0, 0);
        }

        private RouletteType GetRouletteResult(float fill)
        {
            Debug.Log(fill);
            Debug.Log(currentPattern.Item1);
            Debug.Log(currentPattern.Item1 + currentPattern.Item2);


            if (fill <= currentPattern.Item1 && fill >= 0)
            {
                resultImage.sprite = hardSprite;
                resultImage.SetNativeSize();
                return RouletteType.Hard;
            }

            else if (fill > currentPattern.Item1 && fill <= currentPattern.Item1 + currentPattern.Item2)
            {
                resultImage.sprite = luckySprite;
                resultImage.SetNativeSize();
                return RouletteType.Lucky;
            }

            else
            {
                resultImage.sprite = normalSprite;
                resultImage.SetNativeSize();
                return RouletteType.Normal;
            }
        }
    }
}

