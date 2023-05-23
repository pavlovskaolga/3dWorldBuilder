using UnityStruckdTest.Actors;

namespace UnityStruckdTest.Activators
{
    public abstract class BaseActivator
    {
        protected readonly bool _activate;
        public BaseActivator(bool activate)
        {
            _activate = activate;
        }

        public virtual void TryActivate(BaseAction action, BaseActor actor) { }
        public virtual void Reset(ActorWorld actorWorld) { }
    }
}