using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UserBonnou
{
    [System.Serializable]
    public class UserBonnouManager
    {
        public UserBonnouList BonnouList;
    }

    [System.Serializable]
    public class UserBonnouList
    {
        public List<UserBonnou> list = new List<UserBonnou>();
    }


    [System.Serializable]
    public class UserBonnou
    {
        public int id;
        public string title;
        public string description;
        public int rank;
        public int is_show;

    }
}
