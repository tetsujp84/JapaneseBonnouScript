using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Main;
using SceneManager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Title
{
    public class TitleSceneManager : MonoBehaviour, IScene
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button userStartButton;
        [SerializeField] private TitlePerformer titlePerformer;
        [SerializeField] private TransitionPerformer transitionPerformer;

        public UniTask InitializeAsync()
        {
            UserData.Init();
            var userBonnouMode = false;
            startButton.OnClickAsObservable().Subscribe(_ =>
            {
                titlePerformer.OnStartButtonPushed(userBonnouMode);
                transitionPerformer.FadeOut();
            });

            userStartButton.OnClickAsObservable().Subscribe(_ =>
            {
                userBonnouMode = true;
                titlePerformer.OnStartButtonPushed(userBonnouMode);
                transitionPerformer.FadeOut();
            });

            transitionPerformer
                .OnComplete
                .Subscribe(_ => RootSceneManager.LoadSceneAsync<MainSceneManager>(async s => await s.InitializeAsync(userBonnouMode)).Forget())
                .AddTo(this);

            return UniTask.CompletedTask;
        }

        public UniTask ShowAsync()
        {
            return UniTask.CompletedTask;
        }

        public UniTask HideAsync()
        {
            return UniTask.CompletedTask;
        }
    }
}