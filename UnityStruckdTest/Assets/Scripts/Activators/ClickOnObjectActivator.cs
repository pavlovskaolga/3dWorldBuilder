using UnityEngine;
using UnityEngine.EventSystems;
using UnityStruckdTest.Actors;
using UnityStruckdTest.Behaviours;

namespace UnityStruckdTest.Activators
{
    public class ClickOnObjectActivator : BaseActivator
    {
        private readonly IInput _input;
        private readonly GameObject _gameObject;
        private readonly bool _clickedOnModel;

        public ClickOnObjectActivator(bool activate, bool clickedOnModel, GameObject gameObject, IInput input) : base(activate)
        {
            _input = input;
            _gameObject = gameObject;
            _clickedOnModel = clickedOnModel;
        }

        public override void TryActivate(BaseAction action, BaseActor actor)
        {
            if (_input.InputCount == 1 && !EventSystem.current.IsPointerOverGameObject(_input.GetPointerFingerID) && _input.GetCurrentInputState(0) == TouchPhase.Began
                && (_input.GetPressedGameObject() == _gameObject) == _clickedOnModel)
            {
                action.Active = _activate;
            }
        }
    }
}
