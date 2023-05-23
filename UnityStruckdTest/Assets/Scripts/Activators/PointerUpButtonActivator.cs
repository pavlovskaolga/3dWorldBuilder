using UnityStruckdTest.Actors;
using UnityStruckdTest.UI;

namespace UnityStruckdTest.Activators
{
    public class PointerUpButtonActivator : BaseActivator
    {
        private readonly BuildButtonReferences _buttonReferences;
        private bool _pointerUp;
        public PointerUpButtonActivator(bool activate, BuildButtonReferences buildButtonReferences) : base(activate)
        {
            _buttonReferences = buildButtonReferences;
            _buttonReferences.OnPointerUpHandler += () =>
            {
                _pointerUp = true;
            };
        }

        public override void TryActivate(BaseAction action, BaseActor actor)
        {
            if (_pointerUp)
            {
                action.Active = _activate;
            }
            _pointerUp = false;
        }

        public override void Reset(ActorWorld actorWorld)
        {
            base.Reset(actorWorld);
            _pointerUp = false;
        }
    }
}
