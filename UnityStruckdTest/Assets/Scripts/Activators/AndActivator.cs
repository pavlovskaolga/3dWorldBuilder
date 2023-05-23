using UnityStruckdTest.Actors;

namespace UnityStruckdTest.Activators
{
    public class AndActivator : BaseActivator
    {
        private readonly BaseActivator[] _activators;
        public AndActivator(bool activate, params BaseActivator[] activators) : base(activate)
        {
            _activators = activators;
        }

        public override void TryActivate(BaseAction action, BaseActor actor)
        {
            foreach (var activator in _activators)
            {
                action.Active = !_activate;
                activator.TryActivate(action, actor);
                if (action.Active != _activate)
                {
                    return;
                }
            }
        }

        public override void Reset(ActorWorld actorWorld)
        {
            base.Reset(actorWorld);
            foreach (var activator in _activators)
            {
                activator.Reset(actorWorld);
            }
        }
    }
}
