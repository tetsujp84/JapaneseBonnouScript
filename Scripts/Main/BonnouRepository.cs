using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Main
{
    public interface IBonnouRepository
    {
        IReadOnlyList<BonnouEntity> GetOrderedAll();
    }
    
    [CreateAssetMenu(menuName = "Create ScriptableObject/BonnouRepository")]
    public class BonnouRepository : ScriptableObject, IBonnouRepository
    {
        public BonnouEntity[] bonnouEntities;

        public IReadOnlyList<BonnouEntity> GetAll() => bonnouEntities;

        // Rankで取得→Scoreで並び替え
        public IReadOnlyList<BonnouEntity> GetOrderedAll() => bonnouEntities.OrderBy(r => r.Rank).ThenBy(r => r.Score).ThenBy(_ => Guid.NewGuid()).ToList();
    }
}