using UnityEngine.EventSystems;
using UnityStruckdTest.Actors;
using UnityStruckdTest.UI;

namespace UnityStruckdTest.Activators
{
    public class PressButtonActivator : BaseActivator
    {
        private readonly BuildButtonReferences _buttonReferences;
        private bool _pointerDown;
        public PressButtonActivator(bool activate, BuildButtonReferences buildButtonReferences) : base(activate)
        {
            _buttonReferences = buildButtonReferences;
            _buttonReferences.OnPointerDownHandler += () =>
            {
                _pointerDown = true;
            };
            _buttonReferences.OnPointerDragHandler += () =>
            {
                _pointerDown = true;
            };
        }

        public override void TryActivate(BaseAction action, BaseActor actor)
        {
            if (_pointerDown)
            {
                action.Active = _activate;
            }
            _pointerDown = false;
        }

        public override void Reset(ActorWorld actorWorld)
        {
            base.Reset(actorWorld);
            _buttonReferences.CancelDrag = true;
            _pointerDown = false;
        }
    }
}
