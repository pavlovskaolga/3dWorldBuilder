using UnityEngine;

namespace UnityStruckdTest.Behaviours
{
    public class TouchScreenInput : ILateUpdateBehaviour, IInput
    {
        private Vector3[] _worldPosition = new Vector3[2];
        private Vector3[] _lastWorldPosition = new Vector3[2];

        private GameObject _pressedGameObject;
        public Vector2 DefaultInput => _defaultPosition;

        private Vector2 _defaultPosition = new Vector2(-1, -1);

        private readonly Plane _plane = new Plane(Vector3.up, 0);
        private readonly Camera _camera;

        public TouchScreenInput(Camera camera)
        {
            _camera = camera;
        }

        public int InputCount =>  Input.touchCount;

        public int GetPointerFingerID => 0;

        public Vector2 GetCurrentInputAt(int index)
        {
            if ((index == 0 && Input.touchCount > 0) || (index == 1 && Input.touchCount > 1))
            {
                return Input.touches[index].position;
            }

            return DefaultInput;
        }

        public Vector2 GetLastInputAt(int index)
        {
            if ((index == 0 && Input.touchCount > 0) || (index == 1 && Input.touchCount > 1))
            {
                return Input.touches[index].position - Input.touches[index].deltaPosition;
            }

            return DefaultInput;
        }

        public void LateUpdate()
        {
            _pressedGameObject = null;

            for (int i = 0; i < 2; i++)
            {
                _lastWorldPosition[i] = DefaultInput;
                _worldPosition[i] = DefaultInput;
            }
        }

        public Vector3 GetLastInputWorld(int index)
        {
            var position = _lastWorldPosition[index];
            if ((index == 0 && Input.touchCount > 0) || (index == 1 && Input.touchCount > 1))
            {
                 position = CaclculateWorldPosition(index, Input.touches[index].position - Input.touches[index].deltaPosition, _lastWorldPosition[index]);
                _lastWorldPosition[index] = position;   
            }
            return position;
        }

        public Vector3 GetCurrentInputWorld(int index)
        {
            var position = _worldPosition[index];
            if ((index == 0 && Input.touchCount > 0) || (index == 1 && Input.touchCount > 1))
            {
                position = CaclculateWorldPosition(index, Input.touches[index].position, _worldPosition[index]);
                _worldPosition[index] = position;
            }
            return position;
        }

        private Vector3 CaclculateWorldPosition(int index, Vector2 positon, Vector3 lastWorldPosition)
        {
            if (lastWorldPosition.x == DefaultInput.x && lastWorldPosition.y == DefaultInput.y)
            {
                if (Input.touches[index].phase != TouchPhase.Canceled && Input.touches[index].phase != TouchPhase.Ended)
                {
                    var newPosRay = _camera.ScreenPointToRay(positon);
                    if (_plane.Raycast(newPosRay, out var distance))
                    {
                        return newPosRay.GetPoint(distance);
                    }
                }
            }
            return lastWorldPosition;
        }

        public GameObject GetPressedGameObject()
        {
            if (_pressedGameObject == null)
            {
                if (Input.touchCount == 1)
                {
                    var ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
                    if (Physics.Raycast(ray, out var hit))
                    {
                        _pressedGameObject = hit.collider.gameObject;
                    }
                }
            }
            return _pressedGameObject;
        }

        public TouchPhase GetCurrentInputState(int index)
        {
            if ((index == 0 && Input.touchCount > 0) || (index == 1 && Input.touchCount > 1))
            {
                return Input.touches[index].phase;
            }
            return TouchPhase.Canceled;
        }
    }
}