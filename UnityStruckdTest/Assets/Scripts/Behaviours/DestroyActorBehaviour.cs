using UnityStruckdTest.Actors;

namespace UnityStruckdTest.Behaviours
{
    public class DestroyActorBehaviour : IUpdateBehaviour
    {
        private readonly BaseActor _actor;

        public DestroyActorBehaviour(BaseActor actor)
        {
            _actor = actor;
        }

        public void Update(ActorWorld actorWorld, float deltaTime)
        {
            actorWorld.DeleteActor(_actor);
        }
    }
}