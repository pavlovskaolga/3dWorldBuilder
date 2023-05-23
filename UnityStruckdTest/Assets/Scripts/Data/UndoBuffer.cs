using UnityStruckdTest.Actors;

namespace UnityStruckdTest.Data
{
    public class UndoBuffer
    {
        private const int BufferSize = 5;
        private CircularBuffer<IUndoAction> _buffer = new CircularBuffer<IUndoAction>(BufferSize);

        public int Size => _buffer.Size;

        public void Enqueue(IUndoAction undoAction)
        {
            _buffer.Enqueue(undoAction);
        }

        public void Dequeue(ActorWorld actorWorld)
        {
            if (_buffer.Size > 0)
            {
                _buffer.Dequeue().Undo(actorWorld);
            }
        }
    }
}