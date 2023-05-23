using UnityStruckdTest.Actors;

namespace UnityStruckdTest.Behaviours
{
    public interface IUpdateBehaviour
    {
        void Update(ActorWorld actorWorld, float deltaTime);   
    }
}