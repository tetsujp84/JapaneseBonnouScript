using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UniRx;
using System;
using Extensions;

public class TransitionPerformer : MonoBehaviour
{
    [SerializeField] private RectTransform[] maskTransforms;
    [SerializeField] private Image[] maskImages;

    private Subject<Unit> onComplete = new Subject<Unit>();
    public IObservable<Unit> OnComplete => onComplete;

    public void Initialize()
    {
        for (int i = 0; i < maskTransforms.Length; i++)
        {
            maskTransforms[i].SetLocalPositionX(0);
            maskImages[i].color = Color.black;
        }
        gameObject.SetActive(true);
    }


    public void FadeOut()
    {
        gameObject.SetActive(true);
        for (int i = 0; i < maskTransforms.Length; i++)
        {
            maskTransforms[i]
                .DOLocalMoveX(0, 0.6f)
                .SetEase(Ease.InSine)
                .SetDelay(i * 0.2f);
            maskImages[i]
                .DOColor(Color.black, 0.4f)
                .SetDelay(i * 0.2f + 0.6f);
        }
        DOVirtual.DelayedCall(1.3f, () => onComplete.OnNext(Unit.Default));
    }

    public void FadeIn()
    {
        for (int i = 0; i < maskTransforms.Length; i++)
        {
            maskImages[i]
                .DOColor(Color.white, 0.2f)
                .SetDelay(i * 0.1f);

            maskTransforms[i]
                .DOLocalMoveX(-980, 0.4f)
                .SetEase(Ease.InSine)
                .SetDelay(i * 0.1f + 0.1f);
        }
        DOVirtual.DelayedCall(2f, () =>
        {
            gameObject.SetActive(false);
            onComplete.OnNext(Unit.Default);
        });
    }



}
