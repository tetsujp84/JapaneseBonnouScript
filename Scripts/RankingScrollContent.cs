using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UserRanking;

public class RankingScrollContent : MonoBehaviour
{
    [SerializeField]
    TMP_Text _txt_rank;

    [SerializeField]
    TMP_Text _txt_userName;

    [SerializeField]
    TMP_Text _txt_score;


    public void Set(Rank rank)
    {
        _txt_rank.SetText(rank.rank.ToString());
        _txt_userName.SetText(rank.user_name);
        _txt_score.SetText(rank.score.ToString());
        if(rank.user_id == UserData.userId)
        {
            _txt_rank.color = Color.yellow;
            _txt_userName.color = Color.yellow;
            _txt_score.color = Color.yellow;
        }
    }
}
