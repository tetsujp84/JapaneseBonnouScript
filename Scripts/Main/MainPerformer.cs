using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Extensions;
using Cysharp.Threading.Tasks;
using System;

namespace Main
{
    public class MainPerformer : MonoBehaviour
    {
        [SerializeField] private RectTransform nenTransform;
        [SerializeField] private CanvasGroup nenCanvasGroup;

        [SerializeField] private CanvasGroup rightUiCanvasGroup;
        [SerializeField] private CanvasGroup leftUiCanvasGroup;
        [SerializeField] private RectTransform rightUiTransform;
        [SerializeField] private RectTransform leftUiTransform;
        [SerializeField] private RectTransform targetCircle;

        private static readonly Vector3 rightUiPositionOffset = new Vector3(70, 70);
        private static readonly Vector3 leftUiPositionOffset = new Vector3(-70, 70);

        public void Initialize()
        {
            nenCanvasGroup.alpha = 0;
            rightUiCanvasGroup.alpha = 0;
            leftUiCanvasGroup.alpha = 0;
            rightUiTransform.localPosition += rightUiPositionOffset;
            leftUiTransform.localPosition += leftUiPositionOffset;
            nenTransform.SetLocalScale(1.6f);
        }

        public async UniTaskVoid Run()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));

            targetCircle
                .DORotate(new Vector3(0, 0, 360), 1.6f, RotateMode.FastBeyond360)
                .SetEase(Ease.InOutCubic);

            await UniTask.Delay(TimeSpan.FromSeconds(0.7f));

            float time = 0.8f;

            rightUiCanvasGroup.DOFade(1, time);
            leftUiCanvasGroup.DOFade(1, time);
            rightUiTransform.DOLocalMove(-rightUiPositionOffset, time).SetRelative(true);
            leftUiTransform.DOLocalMove(-leftUiPositionOffset, time).SetRelative(true);

            nenCanvasGroup.DOFade(1, 1);
            nenTransform.DOScale(1, 1);

        }
    }

}
