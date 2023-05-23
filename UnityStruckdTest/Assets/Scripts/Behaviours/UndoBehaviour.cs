using UnityStruckdTest.Actors;
using UnityStruckdTest.Data;

namespace UnityStruckdTest.Behaviours
{
    public class UndoBehaviour : IUpdateBehaviour
    {
        private readonly UndoBuffer _undoBuffer;

        public UndoBehaviour(UndoBuffer undoBuffer)
        {
            _undoBuffer = undoBuffer;
        }

        public void Update(ActorWorld actorWorld, float deltaTime)
        {
            _undoBuffer.Dequeue(actorWorld);
        }
    }
}