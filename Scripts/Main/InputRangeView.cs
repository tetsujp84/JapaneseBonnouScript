using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Main
{
    public class InputRangeView : MonoBehaviour, IPointerDownHandler
    {
        public bool IsEnable { get; set; }
        private readonly Subject<Unit> subClick = new Subject<Unit>();
        public IObservable<Unit> OnClick => subClick;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (IsEnable)
            {
                subClick.OnNext(Unit.Default);
            }
        }
    }
}