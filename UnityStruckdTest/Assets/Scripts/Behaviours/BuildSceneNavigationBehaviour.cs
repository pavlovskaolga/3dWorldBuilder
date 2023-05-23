using UnityStruckdTest.Actors;

namespace UnityStruckdTest.Behaviours
{
    public class BuildSceneNavigationBehaviour : IInitBehaviour
    {
        private readonly IInput _input;
        private readonly IMovement _moveWith;

        public BuildSceneNavigationBehaviour(IInput input, IMovement moveWith)
        {
            _input = input;
            _moveWith = moveWith;
        }

        public void Init(ActorWorld actorWorld)
        {
            var navigateWorldActor = new BaseActor();
            var navigateActions = new BaseAction { Active = true };
            var zoom = new ZoomWithInputBehaviour(_input);
            navigateActions.AddUpdate(zoom);
            navigateActions.AddUpdate(new CameraZoomBehaviour(zoom));
            navigateActions.AddUpdate(new MoveNotSelectedBehaviour(actorWorld.SharedData.Camera.transform, _moveWith, 1));
            var rotateWith = new RotateWithInputBehaviour(_input);
            navigateActions.AddUpdate(rotateWith);
            navigateActions.AddUpdate(new RotateAroundScreenCenterBehaviour(actorWorld.SharedData.Camera.transform, rotateWith));
            navigateWorldActor.AddAction(navigateActions);
            actorWorld.AddActor(navigateWorldActor);
        }
    }
}