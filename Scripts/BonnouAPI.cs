using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace UserBonnou
{
    public class BonnouAPI
    {
        public static async UniTask<string> RegisterBonnou(string title, string description, int rank = 1)
        {
            Hashtable post = new Hashtable
            {
                { "title", title },
                { "description", description },
                { "rank", rank },
            };
            return await NetManager.Connect("bonnou/register", post);
        }

        public static async UniTask<string> GetBonnou(RequestBonnouList requestBonnouList)
        {
            Hashtable post = new Hashtable
            {
                { "bonnou_request", JsonUtility.ToJson(requestBonnouList) },
            };
            return await NetManager.Connect("bonnou/get", post);
        }
    }


    [System.Serializable]
    public class RequestBonnouList
    {
        public List<RequestBonnou> request = new List<RequestBonnou>();
    }

    [System.Serializable]
    public class RequestBonnou
    {
        public int rank;
        public int count;

        public RequestBonnou()
        {

        }

        public RequestBonnou(int rank, int count)
        {
            this.rank = rank;
            this.count = count;
        }
    }
}
