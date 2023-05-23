using UnityEngine;
using UnityStruckdTest.Actors;

namespace UnityStruckdTest.Behaviours
{
    public class MoveBehaviour : IUpdateBehaviour
    {
        private readonly Transform _transform;
        private readonly IMovement _moveWith;
        private readonly int _sign = 1;

        public MoveBehaviour(Transform transform, IMovement moveWith, int sign)
        {
            _transform = transform;
            _moveWith = moveWith;
            _sign = sign;
        }

        public void Update(ActorWorld actorWorld, float deltaTime)
        {
            _transform.position += new Vector3(_moveWith.MoveVector.x, 0, _moveWith.MoveVector.z) * _sign; 
        }
    }
}