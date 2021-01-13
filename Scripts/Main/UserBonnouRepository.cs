using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UserBonnou;

namespace Main
{
    public class UserBonnouRepository : IBonnouRepository
    {
        private List<BonnouEntity> bonnouEntities = new List<BonnouEntity>();

        public async UniTask InitializeAsync(IBonnouRepository repository)
        {
            var requestBonnouList = new RequestBonnouList();
            requestBonnouList.request.Add(new RequestBonnou(1, 108));
            await UserData.GetUserBonnouListFromServer(requestBonnouList);
            var userList = UserData.UserBonnouManager.BonnouList.list;
            foreach (var bonnouEntity in repository.GetOrderedAll())
            {
                var bonnou = userList.FirstOrDefault(b => b.id == bonnouEntity.Id);
                if (bonnou != null)
                {
                    bonnouEntities.Add(new BonnouEntity()
                    {
                        Id = bonnou.id,
                        Theme = bonnou.title,
                        Description = bonnou.description,
                        Rank = bonnouEntity.Rank,
                        SpeedRate = bonnouEntity.SpeedRate,
                        Score = bonnouEntity.Score
                    });
                } else
                {
                    bonnouEntities.Add(bonnouEntity);
                }
            }
        }

        public IReadOnlyList<BonnouEntity> GetOrderedAll() => bonnouEntities.OrderBy(r => r.Rank).ThenBy(_ => Guid.NewGuid()).ToList();
    }
}