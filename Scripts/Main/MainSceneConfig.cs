using System;
using UnityEngine;

namespace Main
{
    [CreateAssetMenu(menuName = "Create ScriptableObject/MainSceneConfig")]
    public class MainSceneConfig : ScriptableObject
    {
        [Header("制限時間")]
        public int LimitTime = 60;
        
        [Header("片道の時間倍率")]
        public float OneTime = 1f;

        [Serializable]
        public struct TargetCircleTime
        {
            [Header("アイコンの個数")]
            public int WaitCount;
            [Header("1つのアイコンが有効になる時間")]
            public float OneWaitTime;
        }

        [Header("アイコンによる待ち時間")]
        public TargetCircleTime TargetTime;
    }
}