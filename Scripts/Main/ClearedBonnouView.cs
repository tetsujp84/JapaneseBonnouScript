using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UserRanking;
using Utility;

namespace Main
{
    public class ClearedBonnouView : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        [SerializeField] private TextMeshProUGUI themeText;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private Vector2[] fromRandomPositionsDistance;
        [SerializeField] private Vector2[] toRandomPositions;
        [SerializeField] private Vector2 toRangeDuration = new Vector2(1.3f, 2.8f);
        [SerializeField] private Ease[] easeTypes;
        [SerializeField] private Vector2 delay;

        public void Show(BonnouEntity clearedEntity)
        {
            themeText.SetText(clearedEntity.Theme);
            descriptionText.SetText($"【{clearedEntity.Description}】");
            var randFrom = RandomUtility.GetRandom(fromRandomPositionsDistance) * RandomUtility.GetRandom(-1, 1);
            rectTransform.anchoredPosition += randFrom;

            gameObject.SetActive(true);
            var randTo = RandomUtility.GetRandom(toRandomPositions);
            var diffPosition = randTo - randFrom;
            float distance = diffPosition.magnitude;
            float durationRatio = distance / 400f;
            rectTransform.DOAnchorPos(randTo, UnityEngine.Random.Range(toRangeDuration.x, toRangeDuration.y) * durationRatio)
                .SetEase(RandomUtility.GetRandom(easeTypes))
                .SetDelay(RandomUtility.GetRandom(delay.x, delay.y))
                .OnComplete(OnFinishDestroy);
        }

        private void OnFinishDestroy()
        {
            Destroy(gameObject);
        }
    }
}