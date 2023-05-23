using UnityEngine;
using UnityStruckdTest.Actors;

namespace UnityStruckdTest.Behaviours
{
    public class RotateWithInputBehaviour : IUpdateBehaviour, IRotate
    {
        private readonly IInput _input;

        public Vector3 Angle { get; private set;  }

        public RotateWithInputBehaviour(IInput input)
        {
            _input = input;
        }

        public void Update(ActorWorld actorWorld, float deltaTime)
        {
            Angle = Vector3.zero;

            if (_input.InputCount != 2)
            {
                return;
            }
            
            var currentInput0 = _input.GetCurrentInputAt(0);
            var currentInput1 = _input.GetCurrentInputAt(1);

            var lastInput0 = _input.GetLastInputAt(0);
            var lastInput1 = _input.GetLastInputAt(1);

            if (lastInput0 != _input.DefaultInput && lastInput1 != _input.DefaultInput)
            {
                var currentVector = (currentInput0 - currentInput1).normalized;
                var previousVector = (lastInput0 - lastInput1).normalized;

                Angle = new Vector3(0, Vector3.SignedAngle(currentVector, previousVector, Vector3.back), 0);
            }
        }
    }
}