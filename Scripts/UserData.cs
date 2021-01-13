using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UserRanking;
using UserBonnou;

public static class UserData
{
    public static UserRankingManager UserRankingManager;
    public static UserBonnouManager UserBonnouManager;

    public static string userId;

    public static void Init()
    {
        userId = PlayerPrefs.GetString("user_id", "none");
        UserRankingManager = new UserRankingManager();
        UserBonnouManager = new UserBonnouManager();
    }

    public static async UniTask GetRanking(int count = 30)
    {
        JsonUtility.FromJsonOverwrite(await RankingAPI.GetTopRanking(count), UserRankingManager);
        JsonUtility.FromJsonOverwrite(await RankingAPI.GetRank(userId), UserRankingManager);
    }

    public static async UniTask GetUserBonnouListFromServer(RequestBonnouList requestBonnouList)
    {
        JsonUtility.FromJsonOverwrite(await BonnouAPI.GetBonnou(requestBonnouList), UserBonnouManager);
    }

}
