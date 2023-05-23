using UnityStruckdTest.Actors;

namespace UnityStruckdTest.Behaviours
{
    public class SelectModelBehaviour : IUpdateBehaviour
    {
        public void Update(ActorWorld actorWorld, float deltaTime)
        {
            actorWorld.SharedData.ModelsContainer.ModelSelected = true;
        }
    }
}