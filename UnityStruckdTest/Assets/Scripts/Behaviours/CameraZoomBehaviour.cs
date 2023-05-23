using UnityEngine;
using UnityStruckdTest.Actors;

namespace UnityStruckdTest.Behaviours
{
    public class CameraZoomBehaviour : IUpdateBehaviour
    {
        private readonly IZoom _zoomWith;

        public CameraZoomBehaviour(IZoom zoomWith)
        {
            _zoomWith = zoomWith;
        }

        public void Update(ActorWorld actorWorld, float deltaTime)
        {
            actorWorld.SharedData.Camera.orthographicSize += _zoomWith.Value / 200000;
            var config = actorWorld.SharedData.CameraMoveConfig;
            actorWorld.SharedData.Camera.orthographicSize = Mathf.Clamp(actorWorld.SharedData.Camera.orthographicSize, config.SizeMin, config.SizeMax);
        }
    }
}