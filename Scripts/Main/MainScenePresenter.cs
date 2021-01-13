using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Main;
using Result;
using SceneManager;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UserBonnou;

namespace Main
{
    public class MainScenePresenter : MonoBehaviour
    {
        [SerializeField] private MainSceneConfig mainSceneConfig;
        [SerializeField] private BonnouRepository bonnouRepository;

        [SerializeField] private TimerView timerView;
        [SerializeField] private BonnouScoreView bonnouScoreView;

        [SerializeField] private HammerMover hammerMover;
        [SerializeField] private TargetCircleView targetCircleView;
        [SerializeField] private Cinemachine.CinemachineImpulseSource impulseSource;

        [SerializeField] private ClearedBonnouViewController clearedBonnouViewController;
        [SerializeField] private ModelBell modelBell;

        [SerializeField] private InputRangeView inputRangeView;

        [SerializeField] private EnviroHourChanger enviroHourChanger;

        [SerializeField] private ClearTimeline clearTimeline;
        
        [SerializeField] private ResultManager resultManager;

        private TimerModel timerModel;
        private TargetCircleModel targetCircleModel;

        public async UniTask InitializeAsync(bool userBonnouMode)
        {
            timerModel = new TimerModel(mainSceneConfig.LimitTime);
            targetCircleModel = new TargetCircleModel(mainSceneConfig.TargetTime);
            IBonnouRepository repository;
            if (userBonnouMode)
            {
                var r = new UserBonnouRepository();
                await r.InitializeAsync(bonnouRepository);
                repository = r;
            } else
            {
                repository = bonnouRepository;
            }
            var bonnouScoreModel = new BonnouScoreModel(repository.GetOrderedAll());

            targetCircleView.Initialize(mainSceneConfig.TargetTime.WaitCount);
            bonnouScoreView.SetRemainBonnou(bonnouScoreModel.RemainCount);
            hammerMover.Initialize(mainSceneConfig.OneTime);

            timerModel.TimerCount.Subscribe(t =>
            {
                timerView.SetTimer(t);
                if (timerModel.IsTimeLimit)
                {
                    EndGame(false, bonnouScoreModel.DeletedCount).Forget();
                }
            });
            targetCircleModel.WaitCount.Subscribe(v => { targetCircleView.SetActiveTarget(v); });

            inputRangeView.OnClick.Subscribe(async _ =>
            {
                // 失敗でカウントをリセットする
                if (!hammerMover.IsInSuccessRange || !targetCircleModel.IsFullCount)
                {
                    targetCircleModel.RestartWait();
                    impulseSource.GenerateImpulse();
                    return;
                }
                // 成功判定
                var (clearedBonnou, nextBonnou) = bonnouScoreModel.DoSuccess();
                if (clearedBonnou != null)
                {
                    clearedBonnouViewController.Show(clearedBonnou);
                    targetCircleView.ShowSuccess(clearedBonnou.Score > 1);
                    // 片道で判定は1回
                    hammerMover.DisableInRange();

                    // 速度変更
                    if (nextBonnou != null) hammerMover.SetSpeedRate(nextBonnou);

                    // 鐘のアニメーション
                    await modelBell.DoAttackAsync();
                    bonnouScoreView.SetRemainBonnou(bonnouScoreModel.RemainCount);
                }
                // クリア判定
                if (bonnouScoreModel.IsCleared)
                {
                    EndGame(true, bonnouScoreModel.DeletedCount).Forget();
                }
            });
            resultManager.gameObject.SetActive(false);
            // デバッグ用追加
            this.UpdateAsObservable().Where(_ => Input.GetKeyDown(KeyCode.Q)).First().Subscribe(_ => { timerModel.ForceStop(); });
        }

        public void StartGame()
        {
            inputRangeView.IsEnable = true;
            targetCircleModel.SetEnable(true);
            hammerMover.StartMove();
            timerModel.StartTimer();
            enviroHourChanger.StartTime(timerModel.TimerCount.Value);
        }

        [SerializeField] private float gameEndWaitTime = 1f;
        [SerializeField] private float nextSceneWaitTime = 1f;
        [SerializeField] private GameObject[] resultHideObjects;

        private async UniTaskVoid EndGame(bool isSuccess, int deletedBonnouCount)
        {
            inputRangeView.IsEnable = false;
            timerModel.Stop();
            hammerMover.Stop();
            
            // クリア時ちょっと遅くする演出
            if (isSuccess)
            {
                Time.timeScale = 0.5f;
                await UniTask.Delay(TimeSpan.FromSeconds(gameEndWaitTime));
            }
            
            foreach (var hideObject in resultHideObjects)
            {
                hideObject.SetActive(false);
            }
            Time.timeScale = 1f;
            
            if (isSuccess)
            {
                await clearTimeline.PlayAsync();
            }
            await UniTask.Delay(TimeSpan.FromSeconds(nextSceneWaitTime));
            resultManager.Initialize(new ResultSceneParameter()
            {
                IsSuccess = isSuccess,
                DeletedBonnouCount = deletedBonnouCount,
                RemainTime = timerModel.TimerCount.Value,
            });
            resultManager.gameObject.SetActive(true);
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            resultManager.ShowButtons();
        }
    }
}