using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Main
{
    public class TimerModel
    {
        private ReactiveProperty<int> timerCount = new ReactiveProperty<int>();
        public IReadOnlyReactiveProperty<int> TimerCount => timerCount;

        public bool IsTimeLimit => timerCount.Value <= 0;

        private IDisposable timerDisposable; 

        public TimerModel(int limitTime)
        {
            timerCount = new ReactiveProperty<int>(limitTime);
        }

        public void StartTimer()
        {
            timerDisposable = Observable.Interval(TimeSpan.FromSeconds(1)).Skip(1).Subscribe(t =>
            {
                if (timerCount.Value == 0)
                {
                    Dispose();
                }
                timerCount.Value--;
            });
        }

        private void Dispose()
        {
            timerDisposable?.Dispose();
        }

        public void Stop()
        {
            Dispose();
        }

        public void ForceStop()
        {
            timerCount.Value = 0;
            Dispose();
        }
    }
}