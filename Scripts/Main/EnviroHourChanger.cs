using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Main
{
    public class EnviroHourChanger : MonoBehaviour
    {
        [SerializeField] private EnviroSkyLite enviroSkyLite;
        [SerializeField] private float endHours = 5;
        [SerializeField] private float duration = 1;

        [SerializeField] private float maxEndTime = 22;

        private Sequence sequence;
        
        public void StartTime(int limitTime)
        {
            sequence?.Kill();
            sequence = DOTween.Sequence();
            sequence.Append(DOTween.To(() => enviroSkyLite.internalHour, value => enviroSkyLite.SetInternalTimeOfDay(value), endValue: maxEndTime, limitTime));
        }

        // Timelineから呼ばれる
        public void Play()
        {
            sequence?.Kill();
            sequence = DOTween.Sequence();
            var initialHours = enviroSkyLite.GameTime.Hours;
            enviroSkyLite.SetInternalTimeOfDay(0);
            sequence.Append(DOTween.To(() => 0f, value => enviroSkyLite.SetInternalTimeOfDay(value), endValue: endHours, duration));
        }

        private void OnDestroy()
        {
            sequence?.Kill();
        }
    }
}