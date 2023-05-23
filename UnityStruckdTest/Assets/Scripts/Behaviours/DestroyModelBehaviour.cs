using System;
using UnityEngine;
using UnityStruckdTest.Actors;

namespace UnityStruckdTest.Behaviours
{
    public class DestroyModelBehaviour : IUpdateBehaviour
    {
        private readonly GameObject _gameObject;
        private readonly BaseActor _baseActor;
        private readonly Guid _id;

        public DestroyModelBehaviour(GameObject gameObject, BaseActor baseActor, Guid id)
        {
            _baseActor = baseActor;
            _gameObject = gameObject;
            _id = id;
        }

        public void Update(ActorWorld actorWorld, float deltaTime)
        {
            actorWorld.SharedData.ModelsContainer.RemoveModel(_id);
            GameObject.Destroy(_gameObject);
            actorWorld.DeleteActor(_baseActor);
        }
    }
}