using System.Collections.Generic;
using System;

namespace UnityStruckdTest.Actors
{
    public class BaseActor
    {
        private List<BaseAction> _actions = new List<BaseAction>();
        public Guid ID;

        public BaseActor()
        {
            ID = Guid.NewGuid();
        }

        public BaseActor(Guid id)
        {
            ID = id;
        }

        public void Init(ActorWorld actorWorld)
        {
            foreach (var action in _actions)
            {
                action.Init(actorWorld);
            }
        }

        public void Update(ActorWorld actorWorld, float deltaTime)
        {
            foreach (var action in _actions)
            {
                action.Update(actorWorld, this, deltaTime);
            }
        }

        public void LateUpdate()
        {
            foreach (var action in _actions)
            {
                action.LateUpdate(this);
            }
        }

        public void AddAction(BaseAction action)
        {
            _actions.Add(action);
        }

        public void RemoveAction(BaseAction action)
        {
            _actions.Remove(action);
        }
    }
}