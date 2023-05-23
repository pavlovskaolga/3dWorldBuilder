using UnityEngine;
using UnityStruckdTest.Actors;
using UnityStruckdTest.Behaviours;

namespace UnityStruckdTest.Activators
{
    public class PointerOutOfRectActivator : BaseActivator
    {
        private readonly RectTransform _rect;
        private readonly IInput _input;

        public PointerOutOfRectActivator(bool activate, IInput input, RectTransform rect) : base(activate)
        {
            _rect = rect;
            _input = input;
        }

        public override void TryActivate(BaseAction action, BaseActor actor)
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(_rect, _input.GetCurrentInputAt(0)))
            {
                action.Active = _activate;
            }
        }
    }
}
