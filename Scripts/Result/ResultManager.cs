using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Main;
using SceneManager;
using Title;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace Result
{
    public class ResultManager : MonoBehaviour
    {
        private const string GameId = "japanese_bonnou";
        
        [SerializeField] private ScoreConfig scoreConfig;
        [SerializeField] private GameObject buttons;

        [SerializeField] private Button startButton;

        [SerializeField] private GameObject[] clearStatus;

        [SerializeField] private Button openRegisterBonnouWindowButton;
        [SerializeField] private Button openRankingWindowButton;
        [SerializeField] private Button tweetButton;
        [SerializeField] private string[] tweetTexts;

        [SerializeField] private TMP_Text txt_deletedBonnouCount;
        [SerializeField] private TMP_Text txt_deletedBonnouCountBonus;

        [SerializeField] private TMP_Text txt_clearBonus;

        [SerializeField] private TMP_Text txt_remainTime;
        [SerializeField] private TMP_Text txt_remainTimeBonus;

        [SerializeField] private TMP_Text txt_finalScore;

        private enum ClearStatus
        {
            Success,
            Fail
        }

        public void Initialize(ResultSceneParameter resultSceneParameter)
        {
            // 一度非表示にして対象のみ再表示
            foreach (var s in clearStatus)
            {
                s.SetActive(false);
            }
            var status = resultSceneParameter.IsSuccess ? ClearStatus.Success : ClearStatus.Fail;
            clearStatus[(int)status].SetActive(true);

            int acquireScore = CalcScoreAndSetView(resultSceneParameter);
            
            WindowManager.Inst.SetParam<int>("score", acquireScore);
            WindowManager.Inst.SetParam<bool>("registeredBonnou", false);
            openRankingWindowButton.onClick.AddListener(() =>
            {
                //OnClickButton_OpenRankingWindow();
                ShowNifRankingWindow(acquireScore);
            });

            startButton.OnClickAsObservable().Subscribe(_ => {
                //WindowManager.Inst.Close(WindowManager.WindowName.RANKING).Forget();
                RootSceneManager.LoadSceneAsync<TitleSceneManager>().Forget();
            });
            tweetButton.OnClickAsObservable().Subscribe(_ =>
            {
                var text = resultSceneParameter.IsSuccess ? tweetTexts[0] : tweetTexts[1];
                text += $" スコア {acquireScore}点 ";
                text += $"unityroom.com/games/japanese_bonnou";
                var escText = UnityWebRequest.EscapeURL(text);
                StartCoroutine(TweetWithScreenShot.TweetManager.TweetWithScreenShot(escText));
            });
            openRegisterBonnouWindowButton.OnClickAsObservable().Subscribe(_ =>
            {
                WindowManager.Inst.Open(WindowManager.WindowName.BONNOU_REGISTER).Forget();
            });
            buttons.SetActive(false);
        }

        int CalcScoreAndSetView(ResultSceneParameter resultSceneParameter)
        {
            int deletedBonnouCount = resultSceneParameter.DeletedBonnouCount;
            int remainTime = resultSceneParameter.RemainTime;

            int clearBonus = resultSceneParameter.IsSuccess ? scoreConfig.CLEAR_BONUS : 0;
            int deleteBonnnouCountBonus = scoreConfig.SCORE_UNIT_DELETE_BONNOU_COUNT * deletedBonnouCount;
            int remainTimeBonus = scoreConfig.SCORE_UNIT_REMAIN_TIME * remainTime;

            int finalScore = clearBonus + deleteBonnnouCountBonus + remainTimeBonus;

            txt_deletedBonnouCount.SetText(deletedBonnouCount.ToString());
            txt_remainTime.SetText(remainTime.ToString());

            txt_clearBonus.SetText(clearBonus.ToString());
            txt_deletedBonnouCountBonus.SetText(deleteBonnnouCountBonus.ToString());
            txt_remainTimeBonus.SetText(remainTimeBonus.ToString());
            txt_finalScore.SetText(finalScore.ToString());

            return finalScore;
        }

        private void ShowNifRankingWindow(int score)
        {
            naichilab.RankingLoader.Instance.SendScoreAndShowRanking (score);
        }

        void OnClickButton_OpenRankingWindow()
        {
            WindowManager.Inst.Open(WindowManager.WindowName.RANKING).Forget();
        }

        private void Update()
        {
            if (WindowManager.Inst.GetParam<bool>("registeredBonnou"))
            {
                openRegisterBonnouWindowButton.gameObject.SetActive(false);
            }
        }

        public void ShowButtons()
        {
            openRegisterBonnouWindowButton.onClick.Invoke();
            buttons.SetActive(true);
        }
    }
}