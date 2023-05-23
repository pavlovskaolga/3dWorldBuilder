using UnityStruckdTest.Actors;

namespace UnityStruckdTest.Activators
{
    public class InstantActivationActivator : BaseActivator
    {
        public InstantActivationActivator(bool activate) : base(activate)
        {
        }

        public override void TryActivate(BaseAction action, BaseActor actor)
        {
            action.Active = _activate;
        }
    }
}
