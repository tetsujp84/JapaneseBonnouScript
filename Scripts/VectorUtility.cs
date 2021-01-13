using UnityEngine;

namespace Utility
{
    public static class RandomUtility
    {
        internal static T GetRandom<T> (params T [] param)
        {
            return param [Random.Range (0, param.Length)];
        }
    }
}