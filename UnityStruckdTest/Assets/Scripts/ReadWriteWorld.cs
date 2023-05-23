using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityStruckdTest.Actors;
using UnityStruckdTest.Behaviours;
using UnityStruckdTest.Builders;
using UnityStruckdTest.Data;

namespace UnityStruckdTest
{
    public class ReadWriteWorld
    {
        private readonly string _filePath = Path.Combine(Application.persistentDataPath, "world");
        private readonly IBuilder<GameModelBuilder.ModelBuildContext> _gameModelBuilder;
        public ReadWriteWorld(IInput input, MoveWithInputBehaviour moveWithInputBehaviour, SharedData sharedData)
        {
            _gameModelBuilder = new GameModelBuilder(input, moveWithInputBehaviour, sharedData);
        }

        public void BuildTheWorld(ActorWorld actorWorld)
        {
            var world = GetTheWorld();
            if (world == null)
            {
                return;
            }
            foreach (var model in world.Objects)
            {
                var prefab = actorWorld.SharedData.BuildablesConfig.GetBuildableByID(model.ID);
                var modelBuilderContext = new GameModelBuilder.ModelBuildContext() { Position = model.Position, Prefab = prefab };
                var actor = _gameModelBuilder.Build(modelBuilderContext);
                actorWorld.AddActor(actor);
            }
        }

        public void SaveTheWorld(ActorWorld actorWorld)
        {
            using (var stream = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
            {
                stream.SetLength(0);
                new BinaryFormatter().Serialize(stream, actorWorld.SharedData.ModelsContainer.GetSerialized());
            }
        }

        private SerializableWorld GetTheWorld()
        {
            var output = default(SerializableWorld);
            if (File.Exists(_filePath))
            {
                using (var stream = new FileStream(_filePath, FileMode.Open))
                {
                    output = new BinaryFormatter().Deserialize(stream) as SerializableWorld;
                }
            }
            return output;
        }
    }
}
