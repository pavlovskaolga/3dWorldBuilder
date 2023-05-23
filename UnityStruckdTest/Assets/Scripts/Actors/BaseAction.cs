using System.Collections.Generic;
using UnityStruckdTest.Activators;
using UnityStruckdTest.Behaviours;

namespace UnityStruckdTest.Actors
{
    public class BaseAction
    {
        private List<BaseActivator> _activators = new List<BaseActivator>();
        private List<BaseActivator> _deactivators = new List<BaseActivator>();
        private List<IInitBehaviour> _inits = new List<IInitBehaviour>();
        private List<IUpdateBehaviour> _updates = new List<IUpdateBehaviour>();
        private List<ILateUpdateBehaviour> _lateUpdates = new List<ILateUpdateBehaviour>();

        public bool Active { get; set; }

        public void AddActivator(BaseActivator activator) => _activators.Add(activator);
        public void AddDeactivator(BaseActivator deactivator) => _deactivators.Add(deactivator);
        public void AddInit(IInitBehaviour init) => _inits.Add(init);
        public void AddUpdate(IUpdateBehaviour update) => _updates.Add(update);
        public void AddLateUpdate(ILateUpdateBehaviour lateUpdate) => _lateUpdates.Add(lateUpdate);

        public void Init(ActorWorld actorWorld)
        {
            foreach (var init in _inits)
            {
                init.Init(actorWorld);
            }
        }

        public void Update(ActorWorld actorWorld, BaseActor baseActor, float deltaTime)
        {
            if (!Active)
            {
                foreach (var activator in _activators)
                {
                    activator.TryActivate(this, baseActor);
                }
                if (Active)
                {
                    foreach (var deactivator in _deactivators)
                    {
                        deactivator.Reset(actorWorld);
                    }
                }
            }

            if (Active)
            {
                foreach (var update in _updates)
                {
                    update.Update(actorWorld, deltaTime);
                }
            }

            if (Active)
            {
                foreach (var deactivator in _deactivators)
                {
                    deactivator.TryActivate(this, baseActor);
                }

                if (!Active)
                {
                    foreach (var activator in _activators)
                    {
                        activator.Reset(actorWorld);
                    }
                }
            }
        }

        public void LateUpdate(BaseActor baseActor)
        {
            if (Active)
            {
                foreach (var lateUpdate in _lateUpdates)
                {
                    lateUpdate.LateUpdate();
                }
            }
        }
    }
}