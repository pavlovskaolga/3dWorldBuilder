using System;
using UnityEngine;
using UnityStruckdTest.Actors;
using UnityStruckdTest.Builders;
using UnityStruckdTest.Data;

namespace UnityStruckdTest.Behaviours
{
    public class SpawnModelBehaviour : IUpdateBehaviour
    {
        private readonly GameObject _prefab;
        private readonly IInput _input;
        private readonly RectTransform _spawnFrom;
        private readonly IBuilder<GameModelBuilder.ModelBuildContext> _gameModelBuilder;

        public SpawnModelBehaviour(GameObject prefab, IInput input, RectTransform spawnFrom, MoveWithInputBehaviour moveWithInputBehaviour, SharedData sharedData)
        {
            _prefab = prefab;
            _input = input;
            _spawnFrom = spawnFrom;
            _gameModelBuilder = new GameModelBuilder(input, moveWithInputBehaviour, sharedData);
        }

        public void Update(ActorWorld actorWorld, float deltaTime)
        {
            if (!RectTransformUtility.RectangleContainsScreenPoint(_spawnFrom, _input.GetCurrentInputAt(0)))
            {
                var position = _input.GetCurrentInputWorld(0);

                var modelBuilderContext = new GameModelBuilder.ModelBuildContext() { Position = position, Prefab = _prefab, Spawned = true };
                var actor = _gameModelBuilder.Build(modelBuilderContext);
                actorWorld.AddActor(actor);
            }
        }
    }
}