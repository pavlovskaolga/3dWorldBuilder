using System;
using UnityEngine;
using UnityStruckdTest.Actors;

namespace UnityStruckdTest.Data
{
    public class MoveUndo : IUndoAction
    {
        private readonly ModelsContainer _modelsContainer;
        private readonly Guid _guid;
        private readonly Vector3 _position;

        public MoveUndo(ModelsContainer modelsContainer, Guid guid)
        {
            _modelsContainer = modelsContainer;
            _guid = guid;
            var model = _modelsContainer.GetModel(_guid);
            _position = model.transform.position;
        }

        public void Undo(ActorWorld actorWorld)
        {
            var model = _modelsContainer.GetModel(_guid);
            model.transform.position = _position;
        }
    }
}