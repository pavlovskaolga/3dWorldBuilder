using System;
using UnityEngine;
using UnityStruckdTest.Actors;

namespace UnityStruckdTest.Data
{
    public class PlaceUndo : IUndoAction
    {
        private readonly Guid _id;
        private readonly ModelsContainer _modelsContainer;

        public PlaceUndo(Guid id, ModelsContainer modelsContainer)
        {
            _id = id;
            _modelsContainer = modelsContainer;
        }

        public void Undo(ActorWorld actorWorld)
        {
            var model = _modelsContainer.GetModel(_id);
            GameObject.Destroy(model);
            
            _modelsContainer.RemoveModel(_id);

            actorWorld.DeleteActor(_id);
        }
    }
}