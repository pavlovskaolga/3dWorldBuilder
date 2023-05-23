using UnityStruckdTest.Actors;

namespace UnityStruckdTest.Behaviours
{
    public class ZoomWithInputBehaviour : IUpdateBehaviour, IZoom
    {
        private readonly IInput _input;
        public float Value { get; private set;  }

        public ZoomWithInputBehaviour(IInput input)
        {
            _input = input;
        }

        public void Update(ActorWorld actorWorld, float deltaTime)
        {
            Value = 0;

            if (_input.InputCount != 2)
            {
                return;
            }
            
            var currentInput0 = _input.GetCurrentInputAt(0);
            var currentInput1 = _input.GetCurrentInputAt(1);

            var lastInput0 =  _input.GetLastInputAt(0);
            var lastInput1 = _input.GetLastInputAt(1);

            if (lastInput0 != _input.DefaultInput && lastInput1 != _input.DefaultInput)
            {
                var currentSqrMagnitude = (currentInput0 - currentInput1).sqrMagnitude;
                var previousSqrMagnitude = (lastInput0 - lastInput1).sqrMagnitude;

                Value = previousSqrMagnitude - currentSqrMagnitude;
            }
        }
    }
}