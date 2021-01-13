using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserRanking
{

    [System.Serializable]
    public class UserRankingManager
    {
        public UserRanking Ranking;
    }

    [System.Serializable]
    public class UserRanking
    {
        public List<Rank> Top = new List<Rank>();

        public Rank MyRank;

    }

    [System.Serializable]
    public class Rank
    {
        public string user_name;
        public string user_id;
        public int rank;
        public int score;
    }
}