using UnityEngine;
using UnityStruckdTest.Actors;
using UnityStruckdTest.Behaviours;
using UnityStruckdTest.Configs;
using UnityStruckdTest.Data;
using UnityStruckdTest.UI;

namespace UnityStruckdTest.Main
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField]
        private BottomCanvasReferences _bottomCanvasReferences = default;
        [SerializeField]
        private BuildButtonReferences _buildButtonReferences = default;
        [SerializeField]
        private BuildablesConfig _buildablesConfig = default;
        [SerializeField]
        private CameraMoveConfig _cameraConfigs = default;

        private ActorWorld _actorWorld;
        private ReadWriteWorld _readWriteWorld;
       
        
        public void Start()
        {
            SharedData sharedData = new SharedData();
            sharedData.Camera = Camera.main;
            sharedData.BottomCanvasReferences = _bottomCanvasReferences;
            sharedData.BuildButtonReferences = _buildButtonReferences;
            sharedData.BuildablesConfig = _buildablesConfig;
            sharedData.CameraMoveConfig = _cameraConfigs;
            sharedData.UndoBuffer = new UndoBuffer();
            sharedData.ModelsContainer = new ModelsContainer();
            _actorWorld = new ActorWorld(sharedData);
            var input =
#if UNITY_EDITOR || UNITY_STANDALONE
                new MouseInput(Camera.main);
#else
                new TouchScreenInput(Camera.main);
#endif
            var moveWithInputBehaviour = new MoveWithInputBehaviour(input);

            var inputAndMoveActor = new BaseActor();
            var navigateAction = new BaseAction { Active = true };
            navigateAction.AddLateUpdate(input);
            navigateAction.AddUpdate(moveWithInputBehaviour);
            inputAndMoveActor.AddAction(navigateAction);
            _actorWorld.AddActor(inputAndMoveActor);

            var createBuildUIActor = new BaseActor();
            var buildAndDeleteAction = new BaseAction { Active = true };
            var createBuildUIBehaviour = new CreateBuildUIBehaviour(moveWithInputBehaviour, input);
            buildAndDeleteAction.AddInit(createBuildUIBehaviour);
            buildAndDeleteAction.AddUpdate(new DestroyActorBehaviour(createBuildUIActor));
            createBuildUIActor.AddAction(buildAndDeleteAction);
            _actorWorld.AddActor(createBuildUIActor);

            var sceneNavigationActor = new BaseActor();
            var buildNavigationAndDeleteAction = new BaseAction { Active = true };
            var sceneNavigationBuild = new BuildSceneNavigationBehaviour(input, moveWithInputBehaviour);
            buildNavigationAndDeleteAction.AddInit(sceneNavigationBuild);
            buildNavigationAndDeleteAction.AddUpdate(new DestroyActorBehaviour(createBuildUIActor));
            sceneNavigationActor.AddAction(buildNavigationAndDeleteAction);
            _actorWorld.AddActor(sceneNavigationActor);

            _readWriteWorld = new ReadWriteWorld(input, moveWithInputBehaviour, sharedData);
            _readWriteWorld.BuildTheWorld(_actorWorld);
        }

        public void Update()
        {
            _actorWorld.UpdateActors();
        }

        public void LateUpdate()
        {
            _actorWorld.LateUpdateActors();
        }

        public void OnApplicationQuit()
        {
            _readWriteWorld.SaveTheWorld(_actorWorld);
        }

        public void OnApplicationPause()
        {
#if !UNITY_EDITOR
            _readWriteWorld.SaveTheWorld(_actorWorld);
#endif
        }

        public void OnApplicationFocus(bool focus)
        {
            if (!focus)
            {
#if !UNITY_EDITOR
            _readWriteWorld.SaveTheWorld(_actorWorld);
#endif
            }
        }
    }
}