using UnityStruckdTest.Actors;

namespace UnityStruckdTest.Data
{
    public interface IUndoAction
    {
        void Undo(ActorWorld actorWorld);
    }
}
