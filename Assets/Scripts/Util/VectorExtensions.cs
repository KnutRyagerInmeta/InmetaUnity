using UnityEngine;

namespace VectorExtensions
{
    public static class VectorExtensions
    {
        public static Vector2 Sign(this Vector2 v) => new Vector3(Mathf.Sign(v.x), Mathf.Sign(v.y));

        public static Vector2 Abs(this Vector2 v) => new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y));

        public static Vector2 Min(this Vector2 v, Vector2 min) => new Vector2(Mathf.Min(v.x, min.x), Mathf.Min(v.y, min.y));

        public static Vector2 Max(this Vector2 v, Vector2 max) => new Vector2(Mathf.Max(v.x, max.x), Mathf.Max(v.y, max.y));

        public static Vector2 Clamp(this Vector2 v, Vector2 min, Vector2 max) => v.Max(min).Min(max);

        public static Vector2 Clamp(this Vector2 v, Vector2 minMax) => v.Clamp(-minMax, minMax);

        public static Vector3 Sign(this Vector3 v) => new Vector3(Mathf.Sign(v.x), Mathf.Sign(v.y), Mathf.Sign(v.z));

        public static Vector3 Abs(this Vector3 v) => new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));

        public static Vector3 Min(this Vector3 v, Vector3 min) => new Vector3(Mathf.Min(v.x, min.x), Mathf.Min(v.y, min.y), Mathf.Min(v.z, min.z));

        public static Vector3 Max(this Vector3 v, Vector3 max) => new Vector3(Mathf.Max(v.x, max.x), Mathf.Max(v.y, max.y), Mathf.Max(v.z, max.z));

        public static Vector3 Clamp(this Vector3 v, Vector3 min, Vector3 max) => v.Max(min).Min(max);

        public static Vector3 Clamp(this Vector3 v, Vector3 minMax) => v.Clamp(-minMax, minMax);

        public static Vector3 Mult(this Vector3 v, Vector3 other) => new Vector3(v.x * other.x, v.y * other.y, v.z * other.z);
    }
}
