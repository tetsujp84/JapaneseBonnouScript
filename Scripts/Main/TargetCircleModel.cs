using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Main
{
    public class TargetCircleModel
    {
        private readonly int fullWaitCount;
        private readonly float oneWaitTime;

        // 有効なアイコン数
        public readonly ReactiveProperty<int> WaitCount = new ReactiveProperty<int>();
        public bool IsFullCount => WaitCount.Value == fullWaitCount;
        
        public TargetCircleModel(MainSceneConfig.TargetCircleTime targetCircleTime)
        {
            fullWaitCount = targetCircleTime.WaitCount;
            oneWaitTime = targetCircleTime.OneWaitTime;
        }

        // 強制入力を変更
        public void SetEnable(bool isEnable)
        {
            waitTimeDisposable?.Dispose();
            WaitCount.Value = isEnable ? fullWaitCount : 0;
        }

        private IDisposable waitTimeDisposable;
        // 入力受付を待つ
        public void RestartWait()
        {
            WaitCount.Value = 0;
            waitTimeDisposable?.Dispose();
            waitTimeDisposable = Observable.Interval(TimeSpan.FromSeconds(oneWaitTime)).Subscribe(_ =>
            {
                WaitCount.Value++;
                if (WaitCount.Value == fullWaitCount)
                {
                    waitTimeDisposable?.Dispose();
                }
            });
        }
    }
}