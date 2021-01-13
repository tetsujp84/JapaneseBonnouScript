using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class BaseWindow : MonoBehaviour
{
    const float F_DURATION_TIME = 0.5f;

    WindowManager.WindowName _windowName;
    public WindowManager.WindowName WindowName
    {
        get
        {
            return _windowName;
        }
    }

    [SerializeField]
    protected Canvas _canvas;

    [SerializeField]
    protected CanvasGroup _canvasGroup;
    
    public virtual async UniTask Open(WindowManager.WindowName windowName)
    {
        _windowName = windowName;
        /*DOVirtual.Float(0, 1, F_DURATION_TIME, value => {
            _canvasGroup.alpha = value;
        });*/
        _canvasGroup.alpha = 1;

        await UniTask.Yield();
    }

    public virtual async UniTask Close()
    {
        /*DOVirtual.Float(1, 0, F_DURATION_TIME, value => {
            _canvasGroup.alpha = value;
        });*/
        _canvasGroup.alpha = 0;

        await UniTask.Yield();
    }

    public void SetSortingOrder(int sortOrder)
    {
        _canvas.sortingOrder = sortOrder;
    }
}
