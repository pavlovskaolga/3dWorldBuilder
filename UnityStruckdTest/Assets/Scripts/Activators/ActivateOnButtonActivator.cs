using UnityEngine.UI;
using UnityStruckdTest.Actors;

namespace UnityStruckdTest.Activators
{
    public class ActivateOnButtonActivator : BaseActivator
    {
        private bool _clicked;

        public ActivateOnButtonActivator(bool activate, Button button) : base(activate)
        {
            button.onClick.AddListener(() => _clicked = true);
        }

        public override void TryActivate(BaseAction action, BaseActor actor)
        {
            if (_clicked)
            {
                _clicked = false;
                action.Active = _activate;
            }
        }
    }
}
