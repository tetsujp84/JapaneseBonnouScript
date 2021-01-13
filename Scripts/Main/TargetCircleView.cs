using TMPro;
using UnityEngine;
using Extensions;
using DG.Tweening;
using UnityEngine.UI;

namespace Main
{

    public class TargetCircleView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI[] effectForFailure;
        [SerializeField] private TextMeshProUGUI[] effectForCount;
        [SerializeField] private TextMeshProUGUI[] effectForSuccess;
        [SerializeField] private Color32 activeColor;

        [SerializeField] private Animator successAnimator;
        [SerializeField] private RectTransform successRectTransform;

        [SerializeField] private RectTransform daibutsuTransform;
        [SerializeField] private Image daibutsuImage;
        
        private int maxWaitCount;
        private static readonly int Success = Animator.StringToHash("Success");

        private bool[] isVisibles;
        private Tween[] showTweens;
        private Tween daibutsuMoveTween, daibutsuFadeTween;


        private int prevCount = 0;

        /// <summary>
        /// 設定ミスがなければtargetTimeWaitCount=targetCharacters.Lengthは一致する
        /// </summary>
        /// <param name="configWaitCount"></param>
        public void Initialize(int configWaitCount)
        {
            maxWaitCount = Mathf.Min(configWaitCount, effectForCount.Length);
            isVisibles = new bool[maxWaitCount];

            showTweens = new Tween[maxWaitCount];
        }

        // count=targetCharactersで判定が有効状態
        public void SetActiveTarget(int count)
        {
            if (prevCount < count)
            {
                for (int i = 0; i < count - prevCount; i++)
                {
                    ShowCharacter(count - i - 1);
                }
            }
            else if (count < prevCount)
            {
                for (int i = 0; i < prevCount - count; i++)
                {
                    FadeOutCharacters(prevCount - i - 1);
                }
            }
            prevCount = count;
        }

        private void ShowCharacter(int index)
        {
            float fadeTime = 0.4f;

            effectForCount[index].rectTransform
                .SetLocalScale(2f)
                .DOScale(1, fadeTime);

            showTweens[index] = effectForCount[index]
                .SetColor(activeColor, 0)
                .DOFade(1, fadeTime);
        }

        private void FadeOutCharacters(int index)
        {
            float fadeTime = 0.6f;

            showTweens[index].Kill();
            effectForCount[index].SetAlpha(0);

            effectForFailure[index].rectTransform.DOShakePosition(fadeTime, 10);

            effectForFailure[index]
                .SetColor(Color.red)
                .DOFade(0, fadeTime)
                .SetEase(Ease.InSine);
        }


        /// <summary>
        /// 成功時の表現。
        /// </summary>
        /// <param name="showsDaibitsu">画面右下にうっすらと大仏を表示する。</param>
        public void ShowSuccess(bool showsDaibitsu = false)
        {
            successRectTransform.transform.localPosition = transform.localPosition;
            successAnimator.SetTrigger(Success);

            float fadeTime = 0.4f;

            for (int i = 0; i < maxWaitCount; i++)
            {
                effectForSuccess[i].rectTransform
                    .SetLocalScale(1)
                    .DOScale(1.5f, fadeTime);
                effectForSuccess[i].rectTransform
                    .SetLocalPosition(Vector3.zero)
                    .DOLocalMoveY(10, fadeTime);
                effectForSuccess[i]
                    .SetAlpha(1)
                    .DOFade(0, fadeTime)
                    .SetEase(Ease.OutSine);
            }

            if (showsDaibitsu)
            {
                float daibutsuFadeTime = 1.5f;

                daibutsuMoveTween = daibutsuTransform
                    .SetLocalPositionY(-200)
                    .DOLocalMoveY(-150, daibutsuFadeTime);

                daibutsuFadeTween = daibutsuImage
                    .SetAlpha(0.3f)
                    .DOFade(0, daibutsuFadeTime);
            }
        }
    }
}
