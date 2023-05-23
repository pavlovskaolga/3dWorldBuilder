using System;
using UnityEngine;
using UnityStruckdTest.Actors;
using UnityStruckdTest.Builders;
using UnityStruckdTest.Data;

namespace UnityStruckdTest.Activators
{
    public class WriteUndoDeleteActivator : BaseActivator
    {
        private readonly Guid _id;
        private readonly GameObject _prefab;
        private readonly Transform _transform;
        private readonly IBuilder<GameModelBuilder.ModelBuildContext> _builder;

        public WriteUndoDeleteActivator(bool activate, Transform transform, GameObject prefab, Guid id, IBuilder<GameModelBuilder.ModelBuildContext> builder) : base(activate)
        {
            _transform = transform;
            _prefab = prefab;
            _id = id;
            _builder = builder;
        }

        public override void Reset(ActorWorld actorWorld)
        {
            actorWorld.SharedData.UndoBuffer.Enqueue(new DeleteUndo(_transform.position, _prefab, _id, _builder));
        }
    }
}
