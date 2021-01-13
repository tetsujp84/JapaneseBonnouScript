using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public static class MathUtility
    {
        public static Vector2 PointOnCircle(float angle, float radius = 1)
        {
            float radian = angle * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(radian) * radius, Mathf.Sin(radian) * radius);
        }

    }
}