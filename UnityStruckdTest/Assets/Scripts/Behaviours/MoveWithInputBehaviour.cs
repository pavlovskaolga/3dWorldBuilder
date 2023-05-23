using UnityEngine;
using UnityEngine.EventSystems;
using UnityStruckdTest.Actors;

namespace UnityStruckdTest.Behaviours
{
    public class MoveWithInputBehaviour : IUpdateBehaviour, IMovement
    {
        private readonly IInput _input;
        public Vector3 MoveVector { get; private set; }

        public MoveWithInputBehaviour(IInput input)
        {
            _input = input;
        }

        public void Update(ActorWorld actorWorld, float deltaTime)
        {
            MoveVector = Vector3.zero;

            if (_input.InputCount != 1 || EventSystem.current.IsPointerOverGameObject(_input.GetPointerFingerID))
            {
                return;
            }
            
            var currentInput = _input.GetCurrentInputAt(0);
            var lastInput = _input.GetLastInputAt(0);
            if (lastInput != _input.DefaultInput && lastInput != currentInput)
            {
                var currentWorldPos = _input.GetCurrentInputWorld(0);
                var previousWorldPos = _input.GetLastInputWorld(0);
                MoveVector = new Vector3(previousWorldPos.x - currentWorldPos.x, 0, previousWorldPos.z - currentWorldPos.z);
            }
        }
    }
}