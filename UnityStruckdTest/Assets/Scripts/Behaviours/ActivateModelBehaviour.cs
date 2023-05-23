using UnityStruckdTest.Actors;
using UnityStruckdTest.Data;
using UnityStruckdTest.Models;

namespace UnityStruckdTest.Behaviours
{
    public class ActivateModelBehaviour : IUpdateBehaviour
    {
        private readonly ModelSelected _modelSelected;
        private readonly ModelReferences _modelReferences;
        private readonly bool _activate;

        public ActivateModelBehaviour(ModelSelected modelSelected, ModelReferences modelReferences, bool activate)
        {
            _modelSelected = modelSelected;
            _modelReferences = modelReferences;
            _activate = activate;
        }

        public void Update(ActorWorld actorWorld, float deltaTime)
        {
            _modelSelected.Selected = _activate;
            _modelReferences.Activate(_activate);
        }
    }
}