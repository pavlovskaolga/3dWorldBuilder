using System;
using UnityEngine;
using UnityStruckdTest.Actors;
using UnityStruckdTest.Builders;

namespace UnityStruckdTest.Data
{
    public class DeleteUndo : IUndoAction
    {
        private readonly Guid _id;
        private readonly GameObject _prefab;
        private readonly Vector3 _position;
        private readonly IBuilder<GameModelBuilder.ModelBuildContext> _builder;

        public DeleteUndo(Vector3 position, GameObject prefab, Guid id, IBuilder<GameModelBuilder.ModelBuildContext> builder)
        {
            _position = position;
            _prefab = prefab;
            _id = id;
            _builder = builder;
        }

        public void Undo(ActorWorld actorWorld)
        {
            var modelBuilderContext = new GameModelBuilder.ModelBuildContext() { Position = _position, Prefab = _prefab, ID = _id };
            var actor = _builder.Build(modelBuilderContext);
            actorWorld.AddActor(actor);
        }
    }
}