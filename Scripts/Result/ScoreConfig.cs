using UnityEngine;

namespace Result
{
    [CreateAssetMenu(menuName = "Create ScriptableObject/ScoreConfig")]
    public class ScoreConfig : ScriptableObject
    {
        [Header("クリアしたら加算されるボーナス")]
        public int CLEAR_BONUS = 5000;
        [Header("煩悩を消した数により加算されるボーナスの単価")]
        public int SCORE_UNIT_DELETE_BONNOU_COUNT = 50;
        [Header("残り時間により加算されるボーナスの単価")]
        public int SCORE_UNIT_REMAIN_TIME = 50;
    }
}