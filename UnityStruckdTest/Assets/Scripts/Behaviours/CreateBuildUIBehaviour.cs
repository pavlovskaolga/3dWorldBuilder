using UnityEngine;
using UnityStruckdTest.Activators;
using UnityStruckdTest.Actors;

namespace UnityStruckdTest.Behaviours
{
    public class CreateBuildUIBehaviour : IInitBehaviour
    {
        private readonly MoveWithInputBehaviour _moveWithInputBehaviour;
        private readonly IInput _input;

        public CreateBuildUIBehaviour(MoveWithInputBehaviour moveWithInputBehaviour, IInput input)
        {
            _moveWithInputBehaviour = moveWithInputBehaviour;
            _input = input;
        }

        public void Init(ActorWorld actorWorld)
        {
            var bottomCanvas = GameObject.Instantiate(actorWorld.SharedData.BottomCanvasReferences);

            var uiActor = new BaseActor();

            //action to drag out the models from the UI into the 3d world
            var undoAction = new BaseAction { Active = false };
            undoAction.AddActivator(new ActivateOnButtonActivator(true, bottomCanvas.Undo));
            undoAction.AddUpdate(new UndoBehaviour(actorWorld.SharedData.UndoBuffer));
            undoAction.AddDeactivator(new InstantActivationActivator(false));
            uiActor.AddAction(undoAction);

            actorWorld.AddActor(uiActor);

            foreach (var buildable in actorWorld.SharedData.BuildablesConfig.Buildables)
            {
                var buildableUI = GameObject.Instantiate(actorWorld.SharedData.BuildButtonReferences, bottomCanvas.BuildableRoot);
                buildableUI.Init(buildable.Icon, bottomCanvas.ScrollRect);
                var actor = new BaseActor();

                //action to drag out the models from the UI into the 3d world
                var action = new BaseAction { Active = false };
                var pressButtonActivator = new PressButtonActivator(true, buildableUI);
                var pointerOutOfRectActivator = new PointerOutOfRectActivator(true, _input, bottomCanvas.BottomPanel);

                var andActivator = new AndActivator(true, pressButtonActivator, pointerOutOfRectActivator);
                action.AddActivator(andActivator);
                action.AddUpdate(new SpawnModelBehaviour(buildable.Prefab, _input, bottomCanvas.BottomPanel, _moveWithInputBehaviour, actorWorld.SharedData));
                action.AddDeactivator(new PointerUpButtonActivator(false, buildableUI));
                action.AddDeactivator(new PointerOutOfRectActivator(false, _input, bottomCanvas.BottomPanel));
                actor.AddAction(action);

                actorWorld.AddActor(actor);
            }
        }
    }
}