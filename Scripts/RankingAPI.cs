using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace UserRanking
{
    public class RankingAPI
    {

        public static async UniTask<string> GetTopRanking(int count)
        {
            Hashtable post = new Hashtable {
            { "count", count },
        };
            return await NetManager.Connect("ranking/getTop", post);

        }

        public static async UniTask<string> GetRank(string userId = "")
        {
            Hashtable post = new Hashtable
        {
            { "user_id", userId },
        };
            return await NetManager.Connect("ranking/get", post);
        }

        public static async UniTask<string> RegisterRank(int score, string userId = "", string userName = "noname")
        {
            Hashtable post = new Hashtable {
            { "user_id",userId },
            { "user_name", userName },
            { "score", score },
        };
            return await NetManager.Connect("ranking/register", post);
        }
    }
}
