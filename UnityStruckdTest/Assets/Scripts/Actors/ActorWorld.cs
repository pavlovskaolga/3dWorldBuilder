using System;
using System.Collections.Generic;
using UnityEngine;
using UnityStruckdTest.Data;

namespace UnityStruckdTest.Actors
{
    public class ActorWorld
    {
        private List<BaseActor> _actorsToAdd = new List<BaseActor>();
        private List<BaseActor> _actorsToDelete = new List<BaseActor>();
        private List<BaseActor> _actors = new List<BaseActor>();
        
        private readonly SharedData _sharedData;
        public SharedData SharedData => _sharedData;
        
        public ActorWorld(SharedData sharedData)
        {
            _sharedData = sharedData;
        }

        public void AddActor(BaseActor actor)
        {
            _actorsToAdd.Add(actor);
        }

        public void DeleteActor(BaseActor actor)
        {
            _actorsToDelete.Add(actor);
        }

        public void DeleteActor(Guid id)
        {
            var actor = GetActor(id);
            if (actor != null)
            {
                _actorsToDelete.Add(actor);
            }
        }

        public void UpdateActors()
        {
            foreach (var actor in _actors)
            {
                actor.Update(this, Time.deltaTime);
            }
        }
        
        private BaseActor GetActor(Guid id)
        {
            foreach (var actor in _actors)
            {
                if (actor.ID == id)
                {
                    return actor;
                }
            }
            return null;
        }

        public void LateUpdateActors()
        {
            foreach (var actor in _actors)
            {
                actor.LateUpdate();
            }

            for (var i = 0; i < _actorsToAdd.Count; i++)
            {
                _actorsToAdd[i].Init(this);
            }

            foreach (var actor in _actorsToAdd)
            {
                _actors.Add(actor);
            }
            _actorsToAdd.Clear();

            foreach (var actor in _actorsToDelete)
            {
                _actors.Remove(actor);
            }
            _actorsToDelete.Clear();
        }
    }
}
