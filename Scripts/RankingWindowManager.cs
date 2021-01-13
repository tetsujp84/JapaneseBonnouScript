using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UserRanking;
using TMPro;
using UnityEngine.UI;

public class RankingWindowManager : BaseWindow
{
    [SerializeField]
    GameObject _rankingScrollBase;

    [SerializeField]
    GameObject _rankingScrollContentPrefab;

    [SerializeField]
    TMP_Text _txt_score;

    [SerializeField]
    TMP_Text _txt_score_prevMax;

    [SerializeField]
    TMP_InputField _inputField_userName;

    [SerializeField]
    Button _btn_register;

    [SerializeField]
    Button _btn_closeWindow;

    int _score;

    bool isRegistering = false;

    public async UniTask SetTopRankingView()
    {

        foreach (Transform scrollContentTransform in _rankingScrollBase.transform)
        {
            Destroy(scrollContentTransform.gameObject);
        }

        await UserData.GetRanking();
        _txt_score_prevMax.SetText(UserData.UserRankingManager.Ranking.MyRank.score.ToString());

        _inputField_userName.text = UserData.UserRankingManager.Ranking.MyRank.user_name;
        var topRankings = UserData.UserRankingManager.Ranking.Top;
        foreach (Rank rank in topRankings)
        {
            var rankingObject = Instantiate(_rankingScrollContentPrefab, _rankingScrollBase.transform);
            var rankingScrollContent = rankingObject.GetComponent<RankingScrollContent>();
            rankingScrollContent.Set(rank);
        }
    }

    async UniTaskVoid registerRank(int score,string userName)
    {
        isRegistering = true;

        if (string.IsNullOrEmpty(userName))
        {
            userName = "noname";
        }
        string myRankJson = await RankingAPI.RegisterRank(score, UserData.userId, userName);
        JsonUtility.FromJsonOverwrite(myRankJson, UserData.UserRankingManager);
        UserData.userId = UserData.UserRankingManager.Ranking.MyRank.user_id;
        PlayerPrefs.SetString("user_id", UserData.userId);

        await SetTopRankingView();

        isRegistering = false;
    }

    void OnClickButton_Register()
    {
        if (!isRegistering)
        {
            registerRank(_score, _inputField_userName.text).Forget();
        }
    }

    public void SetScore(int score)
    {
        _score = score;
        _txt_score.text = score.ToString();
    }

    public override async UniTask Open(WindowManager.WindowName windowName)
    {
        _canvasGroup.alpha = 0;
        _txt_score_prevMax.SetText("");
        int score = WindowManager.Inst.GetParam<int>("score");
        SetScore(score);

        _btn_closeWindow.onClick.AddListener(() => WindowManager.Inst.Close(windowName).Forget());
        _btn_register.onClick.AddListener(OnClickButton_Register);
        await base.Open(windowName);

        await SetTopRankingView();
    }
}
