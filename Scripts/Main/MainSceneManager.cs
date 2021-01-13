using Cysharp.Threading.Tasks;
using SceneManager;
using UnityEngine;
using UniRx;
using DG.Tweening;

namespace Main
{
    public class MainSceneManager : MonoBehaviour, IScene
    {
        [SerializeField] private GameObject fadeCanvas;
        [SerializeField] MainScenePresenter mainScenePresenter;
        [SerializeField] private TransitionPerformer transitionPerformer;
        [SerializeField] private MainPerformer mainPerformer;

        private void Awake()
        {
            fadeCanvas.SetActive(true);
        }
        
        public async UniTask InitializeAsync(bool userBonnouMode)
        {
            await mainScenePresenter.InitializeAsync(userBonnouMode);
            transitionPerformer.Initialize();
            mainPerformer.Initialize();
        }

        public UniTask InitializeAsync() => UniTask.CompletedTask;

        public UniTask ShowAsync()
        {
            fadeCanvas.SetActive(false);
            transitionPerformer.FadeIn();

            mainPerformer.Run().Forget();

            DOVirtual.DelayedCall(2.7f, () => mainScenePresenter.StartGame());

            return UniTask.CompletedTask;
        }

        public UniTask HideAsync()
        {
            return UniTask.CompletedTask;
        }
    }
}