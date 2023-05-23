using UnityStruckdTest.Actors;
using UnityStruckdTest.Behaviours;

namespace UnityStruckdTest.Activators
{
    public class InputFinishedActivator : BaseActivator
    {
        private readonly IInput _input;
        public InputFinishedActivator(bool activate, IInput input) : base(activate)
        {
            _input = input;
        }

        public override void TryActivate(BaseAction action, BaseActor actor)
        {
            if (_input.InputCount != 1)
            {
                action.Active = _activate;
            }
        }
    }
}
