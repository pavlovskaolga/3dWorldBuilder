using UnityEngine;

namespace UnityStruckdTest.Behaviours
{
    public class MouseInput : ILateUpdateBehaviour, IInput
    {
        private Vector2 _lastMousePosition;
        private Vector3 _mouseWorldPosition;
        private Vector3 _lastMouseWorldPosition;
        private GameObject _pressedGameObject;
        public Vector2 DefaultInput => _defaultPosition;

        private Vector2 _defaultPosition = new Vector2(-1, -1);

        private readonly Plane _plane = new Plane(Vector3.up, 0);
        private readonly Camera _camera;

        public MouseInput(Camera camera)
        {
            _camera = camera;
        }

        public int InputCount
        {
            get
            {
                if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
                {
                    return 1;
                }
                return 0;
            }
        }

        public int GetPointerFingerID => -1;

        public Vector2 GetCurrentInputAt(int index)
        {
            if (index == 0 && (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0)))
            {
                return Input.mousePosition;
            }

            return DefaultInput;
        }

        public Vector2 GetLastInputAt(int index)
        {
            if (index == 0)
            {
                return _lastMousePosition;
            }

            return DefaultInput;
        }

        public void LateUpdate()
        {
            _pressedGameObject = null;
            if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
            {
                _lastMousePosition = Input.mousePosition;
                _lastMouseWorldPosition = DefaultInput;
                _mouseWorldPosition = DefaultInput;
                return;
            }

            _lastMousePosition = DefaultInput;
            _lastMouseWorldPosition = DefaultInput;
            _mouseWorldPosition = DefaultInput;
        }
        
        public Vector3 GetLastInputWorld(int index)
        {
            if (index == 0 && _lastMouseWorldPosition.x == DefaultInput.x && _lastMouseWorldPosition.y == DefaultInput.y)
            {
                if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
                {
                    var currentInput = _lastMousePosition;
                    var newPosRay = _camera.ScreenPointToRay(currentInput);
                    if (_plane.Raycast(newPosRay, out var distance))
                    {
                        _lastMouseWorldPosition = newPosRay.GetPoint(distance);
                    }
                }
            }
            return _lastMouseWorldPosition;
        }

        public Vector3 GetCurrentInputWorld(int index)
        {
            if (index == 0 && _mouseWorldPosition.x == DefaultInput.x && _mouseWorldPosition.y == DefaultInput.y)
            {
                if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
                {
                    var currentInput = Input.mousePosition;
                    var newPosRay = _camera.ScreenPointToRay(currentInput);
                    if (_plane.Raycast(newPosRay, out var distance))
                    {
                        _mouseWorldPosition = newPosRay.GetPoint(distance);
                    }
                }
            }
            return _mouseWorldPosition;
        }
 
        public GameObject GetPressedGameObject()
        {
            if (_pressedGameObject == null)
            {
                if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
                {
                    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
            if (Input.GetMouseButtonDown(0))
            {
                return TouchPhase.Began;
            }

            if (Input.GetMouseButton(0))
            {
                return TouchPhase.Stationary;
            }

            if (Input.GetMouseButtonUp(0))
            {
                return TouchPhase.Ended;
            }

            return TouchPhase.Canceled;
        }
    }
}