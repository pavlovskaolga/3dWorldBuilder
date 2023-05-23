using System;
using UnityStruckdTest.Actors;
using UnityStruckdTest.Data;

namespace UnityStruckdTest.Activators
{
    public class WriteUndoPlaceActivator : BaseActivator
    {
        private readonly Guid _id;

        public WriteUndoPlaceActivator(bool activate, Guid id) : base(activate)
        {
            _id = id;
        }

        public override void Reset(ActorWorld actorWorld)
        {
            actorWorld.SharedData.UndoBuffer.Enqueue(new PlaceUndo(_id, actorWorld.SharedData.ModelsContainer));
        }
    }
}
