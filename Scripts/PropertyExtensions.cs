using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Extensions
{
    /// <summary>
    /// 主にDOTweenと組み合わせてメソッドチェーン的に使う。
    /// Transform.positionとかのプロパティーを一つだけ変えたいときにも使えるものがある。
    /// </summary>
    public static partial class PropertyExtensions
    {
        public static Transform SetLocalPosition(this Transform transform, Vector3 localPosition)
        {
            transform.localPosition = localPosition;
            return transform;
        }

        public static Transform SetLocalPositionX(this Transform transform, float x)
        {
            Vector3 localPosition = transform.localPosition;
            localPosition.x = x;
            transform.localPosition = localPosition;
            return transform;
        }

        public static Transform SetLocalPositionY(this Transform transform, float y)
        {
            Vector3 localPosition = transform.localPosition;
            localPosition.y = y;
            transform.localPosition = localPosition;
            return transform;
        }

        public static Transform SetLocalPositionZ(this Transform transform, float z)
        {
            Vector3 localPosition = transform.localPosition;
            localPosition.z = z;
            transform.localPosition = localPosition;
            return transform;
        }

        public static Transform SetLocalScale(this Transform transform, float scale)
        {
            transform.localScale = new Vector3(scale, scale, scale);
            return transform;
        }

        public static RectTransform SetLocalPosition(this RectTransform rectTransform, Vector3 localPosition)
        {
            rectTransform.localPosition = localPosition;
            return rectTransform;
        }

        public static RectTransform SetLocalPositionX(this RectTransform rectTransform, float x)
        {
            Vector3 localPosition = rectTransform.localPosition;
            localPosition.x = x;
            rectTransform.localPosition = localPosition;
            return rectTransform;
        }

        public static RectTransform SetLocalPositionY(this RectTransform rectTransform, float y)
        {
            Vector3 localPosition = rectTransform.localPosition;
            localPosition.y = y;
            rectTransform.localPosition = localPosition;
            return rectTransform;
        }

        public static RectTransform SetLocalPositionZ(this RectTransform rectTransform, float z)
        {
            Vector3 localPosition = rectTransform.localPosition;
            localPosition.z = z;
            rectTransform.localPosition = localPosition;
            return rectTransform;
        }

        public static RectTransform SetLocalRotationZ(this RectTransform rectTransform, float z)
        {
            Vector3 localRotation = rectTransform.localRotation.eulerAngles;
            localRotation.z = z;
            rectTransform.localRotation = Quaternion.Euler(localRotation);
            return rectTransform;
        }

        public static RectTransform SetLocalScale(this RectTransform rectTransform, float scale)
        {
            rectTransform.localScale = new Vector3(scale, scale, scale);
            return rectTransform;
        }


        public static Graphic SetAlpha(this Graphic graphic, float alpha)
        {
            Color color = graphic.color;
            color.a = alpha;
            graphic.color = color;
            return graphic;
        }

        public static Graphic SetColor(this Graphic graphic, Color color)
        {
            graphic.color = color;
            return graphic;
        }

        public static Graphic SetColor(this Graphic graphic, Color color, float alpha)
        {
            color.a = alpha;
            graphic.color = color;
            return graphic;
        }

        public static Tween DOGradientColor(this Graphic graphic, Gradient gradient, float duration)
            => DOVirtual.Float(0, 1, duration, value => graphic.color = gradient.Evaluate(value));

        public static Tween DOGradientColorByTween(this SpriteRenderer renderer, Gradient gradient, float duration)
            => DOVirtual.Float(0, 1, duration, value => renderer.color = gradient.Evaluate(value));
    }
}