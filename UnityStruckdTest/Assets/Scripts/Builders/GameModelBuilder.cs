using System;
using UnityEngine;
using UnityStruckdTest.Activators;
using UnityStruckdTest.Actors;
using UnityStruckdTest.Behaviours;
using UnityStruckdTest.Data;
using UnityStruckdTest.Models;

namespace UnityStruckdTest.Builders
{
    public class GameModelBuilder : IBuilder<GameModelBuilder.ModelBuildContext>
    {
        private readonly IInput _input;
        private readonly MoveWithInputBehaviour _moveWithInputBehaviour;
        private readonly SharedData _sharedData;

        public GameModelBuilder(IInput input, MoveWithInputBehaviour moveWithInputBehaviour, SharedData sharedData)
        {
            _input = input;
            _moveWithInputBehaviour = moveWithInputBehaviour;
            _sharedData = sharedData;
        }

        public BaseActor Build(ModelBuildContext modelBuildContext)
        {
            var model = GameObject.Instantiate(modelBuildContext.Prefab);
            model.transform.position = modelBuildContext.Position;

            var actor = modelBuildContext.ID == Guid.Empty ? new BaseActor() : new BaseActor(modelBuildContext.ID);
            var modelSelected = new ModelSelected();

            _sharedData.ModelsContainer.AddModel(actor.ID, model, modelBuildContext.Prefab.GetInstanceID());

            //action to move the model to the ground first time
            var moveAction = new BaseAction() { Active = modelBuildContext.Spawned };
            moveAction.AddUpdate(new MoveBehaviour(model.transform, _moveWithInputBehaviour, -1));
            moveAction.AddUpdate(new SelectModelBehaviour());
            moveAction.AddActivator(new ModelSelectedResetActivator(false));
            moveAction.AddDeactivator(new InputFinishedActivator(false, _input));
            if (modelBuildContext.ID == Guid.Empty)
            {
                moveAction.AddActivator(new WriteUndoPlaceActivator(false, actor.ID));
            }
            actor.AddAction(moveAction);
            var modelRefs = model.GetComponent<ModelReferences>();

            //action to destroy the model
            var destroyModelAction = new BaseAction() { Active = false };
            destroyModelAction.AddActivator(new ActivateOnButtonActivator(true, modelRefs.DestroyButton));
            destroyModelAction.AddUpdate(new DestroyModelBehaviour(model, actor, actor.ID));
            destroyModelAction.AddDeactivator(new WriteUndoDeleteActivator(false, model.transform, modelBuildContext.Prefab, actor.ID, this));
            actor.AddAction(destroyModelAction);

            //action to move the model around after activating it
            var moveOnPressAction = new BaseAction() { Active = false };
            var onClickActivator = new ClickOnObjectActivator(true, true, model, _input);
            var modelSelectedActivator = new ModelSelectedActivator(true, modelSelected);
            moveOnPressAction.AddActivator(new AndActivator(true, onClickActivator, modelSelectedActivator));
            moveOnPressAction.AddUpdate(new MoveBehaviour(model.transform, _moveWithInputBehaviour, -1));
            moveOnPressAction.AddUpdate(new SelectModelBehaviour());
            moveOnPressAction.AddDeactivator(new InputFinishedActivator(false, _input));
            moveOnPressAction.AddDeactivator(new WriteUndoMoveActivator(false, actor.ID));
            actor.AddAction(moveOnPressAction);

            //action to activate the model
            var activateAction = new BaseAction() { Active = false };
            activateAction.AddActivator(new ClickOnObjectActivator(true, true, model, _input));
            activateAction.AddUpdate(new ActivateModelBehaviour(modelSelected, modelRefs, true));
            activateAction.AddDeactivator(new InstantActivationActivator(false));
            actor.AddAction(activateAction);

            //action to move the outline for selected model
            var outlineAction = new BaseAction { Active = false };
            outlineAction.AddActivator(modelSelectedActivator);
            outlineAction.AddUpdate(new SetOutlineBehaviour(modelRefs.Outline, modelRefs.Collider));
            outlineAction.AddDeactivator(new InputFinishedActivator(false, _input));
            actor.AddAction(outlineAction);

            //action to deactivate the model
            var deactivateAction = new BaseAction() { Active = false };
            deactivateAction.AddActivator(new ClickOnObjectActivator(true, false, model, _input));
            deactivateAction.AddUpdate(new ActivateModelBehaviour(modelSelected, modelRefs, false));
            deactivateAction.AddDeactivator(new InstantActivationActivator(false));
            actor.AddAction(deactivateAction);

            return actor;
        }

        public class ModelBuildContext
        {
            public Vector3 Position;
            public GameObject Prefab;
            public Guid ID;
            public bool Spawned;
        }
    }
}