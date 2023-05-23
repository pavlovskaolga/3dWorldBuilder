using System;
using UnityStruckdTest.Actors;
using UnityStruckdTest.Data;

namespace UnityStruckdTest.Activators
{
    public class WriteUndoMoveActivator : BaseActivator
    {
        private readonly Guid _guid;

        public WriteUndoMoveActivator(bool activate, Guid guid) : base(activate)
        {
            _guid = guid;
        }

        public override void Reset(ActorWorld actorWorld)
        {
            actorWorld.SharedData.UndoBuffer.Enqueue(new MoveUndo(actorWorld.SharedData.ModelsContainer, _guid));
        }
    }
}
