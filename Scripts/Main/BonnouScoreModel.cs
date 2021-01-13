using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Main
{
    public class BonnouScoreModel
    {
        private ReactiveCollection<BonnouEntity> bonnouList;
        public bool IsCleared => bonnouList.Count == 0;

        public int InitialCount { get; private set; }
        public int RemainCount => bonnouList.Sum(b => b.Score);
        public int DeletedCount => InitialCount - RemainCount;

        public BonnouScoreModel(IReadOnlyList<BonnouEntity> bonnouList)
        {
            this.bonnouList = new ReactiveCollection<BonnouEntity>(bonnouList);

            InitialCount = RemainCount;
        }

        public (BonnouEntity, BonnouEntity) DoSuccess()
        {
            if (bonnouList.Count <= 0) return (null, null);
            // 現在の取得
            var current = bonnouList[0];
            bonnouList.RemoveAt(0);
            // 次の取得
            var next = bonnouList.Count > 0 ? bonnouList[0] : null;
            return (current, next);
        }
    }
}
