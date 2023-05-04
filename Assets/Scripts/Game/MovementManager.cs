
using DG.Tweening;
using UnityEngine;

namespace Game
{
    public class MovementManager
    {
        public static void OnMoveObjectOnLineRenderer(GameObject moveableObject, LineRenderer line)
        {
            moveableObject.transform.DOMove(GetPointOnLineRenderer(0, line), 0);
            moveableObject.transform.DOPath(GetPointsOnLineRenderer(line), 4f, PathType.Linear, PathMode.Ignore, 
                100, Color.red);
        }
        
        private static Vector3[] GetPointsOnLineRenderer(LineRenderer line)
        {
            var points = new Vector3[line.positionCount];
            line.GetPositions(points);
            return points;
        }
        
        private static Vector3 GetPointOnLineRenderer(float percent, LineRenderer line)
        {
            var points = GetPointsOnLineRenderer(line);
            var index = percent * (points.Length - 1);
            return points[Mathf.RoundToInt(index)];
        }
    }
}