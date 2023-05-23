using UnityStruckdTest.Actors;

namespace UnityStruckdTest.Activators
{
    public class ModelSelectedResetActivator : BaseActivator
    {
        public ModelSelectedResetActivator(bool activate) : base(activate)
        {
        }

        public override void Reset(ActorWorld actorWorld)
        {
            base.Reset(actorWorld);
            actorWorld.SharedData.ModelsContainer.ModelSelected = false;
        }
    }
}
