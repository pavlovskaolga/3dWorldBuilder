using UnityEngine;
using UnityStruckdTest.Actors;

namespace UnityStruckdTest.Behaviours
{
    public class RotateAroundScreenCenterBehaviour : IUpdateBehaviour
    {
        private readonly Transform _transform;
        private readonly IRotate _rotateWith;

        public RotateAroundScreenCenterBehaviour(Transform transform, IRotate rotateWith)
        {
            _transform = transform;
            _rotateWith = rotateWith;
        }

        public void Update(ActorWorld actorWorld, float deltaTime)
        {
            var center = Vector3.zero;
            var ray = actorWorld.SharedData.Camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(ray, out var hit))
            {
                center = hit.point;
            }
            _transform.RotateAround(center, Vector3.up, _rotateWith.Angle.y * 0.45f); 
        }
    }
}