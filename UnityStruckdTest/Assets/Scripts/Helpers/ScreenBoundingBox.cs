using System.Linq;
using UnityEngine;

namespace UnityStruckdTest.Helpers
{
    public class ScreenBoundingBox
    {
        public static Vector3[] _screenCorners = new Vector3[8];

        public static void SetBoundingBox(Camera camera, Bounds bounds, RectTransform rectTransform)
        {
            var c = bounds.center;
            var e = bounds.extents;

            _screenCorners[0] = camera.WorldToScreenPoint(new Vector3(c.x + e.x, c.y + e.y, c.z + e.z));
            _screenCorners[1] = camera.WorldToScreenPoint(new Vector3(c.x + e.x, c.y + e.y, c.z - e.z));
            _screenCorners[2] = camera.WorldToScreenPoint(new Vector3(c.x + e.x, c.y - e.y, c.z + e.z));
            _screenCorners[3] = camera.WorldToScreenPoint(new Vector3(c.x + e.x, c.y - e.y, c.z - e.z));
            _screenCorners[4] = camera.WorldToScreenPoint(new Vector3(c.x - e.x, c.y + e.y, c.z + e.z));
            _screenCorners[5] = camera.WorldToScreenPoint(new Vector3(c.x - e.x, c.y + e.y, c.z - e.z));
            _screenCorners[6] = camera.WorldToScreenPoint(new Vector3(c.x - e.x, c.y - e.y, c.z + e.z));
            _screenCorners[7] = camera.WorldToScreenPoint(new Vector3(c.x - e.x, c.y - e.y, c.z - e.z));

            var maxX = _screenCorners.Max(corner => corner.x);
            var minX = _screenCorners.Min(corner => corner.x);
            var maxY = _screenCorners.Max(corner => corner.y);
            var minY = _screenCorners.Min(corner => corner.y);

            rectTransform.position = new Vector3((minX + maxX) * 0.5f, (minY + maxY) * 0.5f);
            rectTransform.sizeDelta = new Vector2(maxX - minX, maxY - minY);
        }
    }
}