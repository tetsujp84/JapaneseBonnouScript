using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Main;
using TMPro;
using DG.Tweening;
using Extensions;
using Cysharp.Threading.Tasks;
using System;

namespace Title
{
    public class TitlePerformer : MonoBehaviour
    {
        [SerializeField] private BonnouRepository bonnouRepository;

        [SerializeField] private TextMeshPro bonnouPrefab;
        [SerializeField] private Transform bonnouParent;
        [SerializeField] private Gradient bonnouGradient;
        [SerializeField] private Gradient daibutsuGradient;
        [SerializeField] private Gradient namuamidabutsuGradient;

        [SerializeField] private Transform daibutsuTransform;
        [SerializeField] private SpriteRenderer daibutsuRenderer;

        [SerializeField] private Image logo, logoEffect;
        [SerializeField] private TextMeshProUGUI start;
        [SerializeField] private TextMeshProUGUI startEffect;
        [SerializeField] private Button startButton;
        
        [SerializeField] private TextMeshProUGUI userStart;
        [SerializeField] private TextMeshProUGUI userStartEffect;
        [SerializeField] private Button userStartButton;

        [SerializeField] private Transform bellTransform;
        [SerializeField] private Renderer bellRenderer;

        [SerializeField] private Transform hammer;

        [SerializeField] private RectTransform[] namuamidabutsuTransforms;
        [SerializeField] private Graphic[] namuamidabutsuGraphics;

        [SerializeField] private AnimationCurve namuamidabutsuRotationCurve;

        private Sequence startButtonSequence;
        private Tween bellRotationTween;


        public struct BonnouData
        {
            public GameObject gameObject;
            public Transform transform;
            public TextMeshPro textMesh;
        }

        private BonnouData[] bonnouData;

        private static readonly int bonnouCount = 40;

        // Start is called before the first frame update
        private void Start()
        {
            Initialize();
            Run().Forget();
        }

        public void Initialize()
        {
            bonnouData = new BonnouData[bonnouCount];
            for (int i = 0; i < bonnouCount; i++)
            {
                TextMeshPro bonnou = Instantiate(bonnouPrefab, bonnouParent);
                bonnouData[i] = new BonnouData
                {
                    gameObject = bonnou.gameObject,
                    transform = bonnou.transform,
                    textMesh = bonnou
                };
                bonnouData[i].textMesh.SetAlpha(0);
            }

            logo.gameObject.SetActive(false);
            start.gameObject.SetActive(false);

            bellRenderer.material.color = new Color(1, 1, 1, 0);
        }


        public async UniTaskVoid Run()
        {
            float time;

            for (int i = 0; i < bonnouCount; i++)
            {
                time = UnityEngine.Random.Range(2f, 3.5f);
                float delay = UnityEngine.Random.Range(0f, 2f);
                float angle = UnityEngine.Random.Range(0, 360);
                float scale = UnityEngine.Random.Range(0.7f, 1.3f);

                bonnouData[i].textMesh.text = bonnouRepository.bonnouEntities[i].Theme;

                bonnouData[i].transform
                    .SetLocalPosition(MathUtility.PointOnCircle(angle, scale * 0.7f))
                    .DOLocalMove(MathUtility.PointOnCircle(angle, scale * 15f), time)
                    .SetDelay(delay);

                bonnouData[i].transform
                    .SetLocalScale(0.5f)
                    .DOScale(2, time)
                    .SetDelay(delay);

                bonnouData[i].textMesh
                    .DOGradientColor(bonnouGradient, time)
                    .SetDelay(delay);
            }

            RotateNamuamidabutsu();

            await UniTask.Delay(TimeSpan.FromSeconds(0.7f));

            time = 2;

            daibutsuTransform
                .SetLocalPositionY(-8)
                .DOLocalMoveY(-4.5f, time);

            daibutsuRenderer
                .DOGradientColorByTween(daibutsuGradient, time);

            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));

            hammer
                .DOLocalMoveX(-20, 1.5f)
                .SetEase(Ease.Linear);

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));

            bellRotationTween = bellTransform
                .DORotate(new Vector3(0, 360, 0), 5, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1);
            bellTransform
                .DOLocalMoveZ(-2f, 1.2f);
            bellRenderer.material
                .DOFade(1, 1.2f);

            await UniTask.Delay(TimeSpan.FromSeconds(0.8f));

            time = 0.7f;

            logo.gameObject.SetActive(true);

            logo.transform
                .SetLocalScale(2)
                .DOScale(1, time)
                .SetEase(Ease.OutSine);
            logo
                .SetAlpha(0)
                .DOFade(1, time);

            start.gameObject.SetActive(true);
            start.transform
                .DOLocalMoveX(-350, 1.3f)
                .SetEase(Ease.Linear);
            start
                .SetAlpha(0)
                .DOFade(1, 1.3f)
                .SetEase(Ease.InSine);
            
            userStart.gameObject.SetActive(true);
            userStart.transform
                .DOLocalMoveX(-200, 1.3f)
                .SetEase(Ease.Linear);
            userStart
                .SetAlpha(0)
                .DOFade(1, 1.3f)
                .SetEase(Ease.InSine);

            await UniTask.Delay(TimeSpan.FromSeconds(time));

            time = 1;

            logoEffect.gameObject.SetActive(true);

            logoEffect.transform
                .DOScale(2, time);
            logoEffect
                .SetAlpha(1)
                .DOFade(0, time);

            await UniTask.Delay(TimeSpan.FromSeconds(0.6f));

            startButton.enabled = true;
            userStartButton.enabled = true;

            logo.transform.DOLocalMove(new Vector3(334, 60), 0.5f);
            logo.transform.DOScale(0.8f, 0.5f);

            startButtonSequence = DOTween.Sequence()
                .Append(startEffect.transform
                    .SetLocalScale(1)
                    .DOScale(1.4f, 0.7f))
                .Join(startEffect
                       .SetAlpha(1)
                    .DOFade(0, 0.7f))
                .Append(userStartEffect.transform
                    .SetLocalScale(1)
                    .DOScale(1.4f, 0.7f))
                .Join(userStartEffect
                    .SetAlpha(1)
                    .DOFade(0, 0.7f))
                .AppendInterval(0.5f)
                .SetLoops(-1);
        }

        private void RotateNamuamidabutsu()
        {

            int namuamidabutsuCount = namuamidabutsuTransforms.Length;

            for (int i = 0; i < namuamidabutsuCount; i++)
            {
                int index = i;
                DOVirtual.Float(0, 1, 3, value =>
                {
                    namuamidabutsuGraphics[index].color = namuamidabutsuGradient.Evaluate(value);
                    float angle = value * 360f + (360f / namuamidabutsuCount) * (float)index;
                    float radius = 60 + value * 70;
                    namuamidabutsuTransforms[index].localPosition = MathUtility.PointOnCircle(angle, radius);
                })
                    .SetEase(namuamidabutsuRotationCurve);
            }
        }

        public void OnStartButtonPushed(bool userBonnouMode)
        {
            float fadeTime = 0.4f;

            startButtonSequence.Kill();
            startButton.enabled = false;
            userStartButton.enabled = false;

            var disableTarget = userBonnouMode ? startButton : userStartButton;
            disableTarget.gameObject.SetActive(false);
            
            startEffect.gameObject.SetActive(false);
            start
                .SetColor(Color.red)
                .DOFade(0, fadeTime);
            start.rectTransform
                .DOLocalMoveY(40, fadeTime)
                .SetRelative(true)
                .SetEase(Ease.Linear);
            
            userStartEffect.gameObject.SetActive(false);
            userStart
                .SetColor(Color.red)
                .DOFade(0, fadeTime);
            userStart.rectTransform
                .DOLocalMoveY(40, fadeTime)
                .SetRelative(true)
                .SetEase(Ease.Linear);

            bellRotationTween.Kill();
        }
    }
}