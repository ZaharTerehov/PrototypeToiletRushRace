
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Game
{
    public class DrawManager : MonoBehaviour
    {
        [SerializeField] private Line _linePrefab;
        [SerializeField] private Collider2D _start;
        [SerializeField] private RectTransform _end;

        public static event Action<GameObject, LineRenderer> GoalAchieved;
        public static float Resolution = 0.1f;

        private Camera _camera;
        private Line _currentLine;
        private bool _isDraws = false;

        private bool _goalAchieved = false;

        private void Start()
        {
            _camera = Camera.main;
            GoalAchieved += MovementManager.OnMoveObjectOnLineRenderer;
        }

        private void Update()
        {
            if (Input.touchCount == 1 && !_goalAchieved)
            {
                var worldPoint = _camera.ScreenToWorldPoint(Input.touches[0].position);
                var position2D = new Vector2(worldPoint.x, worldPoint.y);
                var hit = Physics2D.Raycast(position2D, Vector2.zero);

                if (hit.collider == _start)
                {
                    _isDraws = true;
                    if (_currentLine == null)
                        _currentLine = Instantiate(_linePrefab, worldPoint, Quaternion.identity);
                    _currentLine.SetPosition(worldPoint);
                }
                else if (_isDraws)
                {
                    _currentLine.SetPosition(worldPoint);

                    if (RectTransformUtility.RectangleContainsScreenPoint(_end, _currentLine.GetLastPosition()))
                    {
                        _goalAchieved = true;
                        GoalAchieved?.Invoke(_start.GameObject(), _currentLine.LineRenderer);
                    }
                }
            }
            else
            {
                if(_currentLine == null || _goalAchieved) return;
                
                // if(RectTransformUtility.RectangleContainsScreenPoint(_end, _currentLine.GetLastPosition()))
                //     Debug.Log(true);

                _isDraws = false;
                _currentLine.ClearLine();
            }
        }
    }
}
