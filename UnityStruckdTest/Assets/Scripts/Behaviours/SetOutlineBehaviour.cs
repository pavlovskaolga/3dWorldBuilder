using UnityEngine;
using UnityStruckdTest.Actors;
using UnityStruckdTest.Helpers;

namespace UnityStruckdTest.Behaviours
{
    public class SetOutlineBehaviour : IUpdateBehaviour
    {
        private readonly RectTransform _outline;
        private readonly BoxCollider _collider;

        public SetOutlineBehaviour(RectTransform outline, BoxCollider collider)
        {
            _outline = outline;
            _collider = collider;
        }

        public void Update(ActorWorld actorWorld, float deltaTime)
        {
            ScreenBoundingBox.SetBoundingBox(actorWorld.SharedData.Camera, _collider.bounds, _outline);
        }
    }
}