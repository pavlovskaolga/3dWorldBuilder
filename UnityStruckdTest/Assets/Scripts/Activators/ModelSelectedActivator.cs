using UnityStruckdTest.Actors;
using UnityStruckdTest.Data;

namespace UnityStruckdTest.Activators
{
    public class ModelSelectedActivator : BaseActivator
    {
        private readonly ModelSelected _modelSelected;

        public ModelSelectedActivator(bool activate, ModelSelected modelSelected) : base(activate)
        {
            _modelSelected = modelSelected;
        }

        public override void TryActivate(BaseAction action, BaseActor actor)
        {
            if (_modelSelected.Selected)
            {
                action.Active = _activate;
            }
        }

        public override void Reset(ActorWorld actorWorld)
        {
            base.Reset(actorWorld);
            actorWorld.SharedData.ModelsContainer.ModelSelected = false;
        }
    }
}
