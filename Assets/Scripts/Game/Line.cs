using UnityEngine;

namespace Game
{
    public class Line : MonoBehaviour
    {
        public LineRenderer LineRenderer;
        
        public void SetPosition(Vector2 position)
        {
            if (!CanAppend(position)) return;

            LineRenderer.positionCount++;
            LineRenderer.SetPosition(LineRenderer.positionCount-1, position);
        }

        public void ClearLine()
        {
            LineRenderer.positionCount = 0;
        }

        private bool CanAppend(Vector2 position)
        {
            if (LineRenderer.positionCount == 0) return true;

            return Vector2.Distance(LineRenderer.GetPosition(LineRenderer.positionCount - 1),
                position) > DrawManager.Resolution;
        }

        public Vector3 GetLastPosition()
        {
            if (LineRenderer.positionCount < 0) return new Vector3();
            
            return LineRenderer.GetPosition(LineRenderer.positionCount - 1);
        }
    }
}