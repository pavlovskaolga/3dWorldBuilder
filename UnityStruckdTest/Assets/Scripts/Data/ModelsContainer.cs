using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStruckdTest.Data
{
    public class ModelsContainer
    {
        private readonly Dictionary<Guid, Model> _container = new Dictionary<Guid, Model>();
        public bool ModelSelected { get; set; }

        public void AddModel(Guid guid, GameObject model, int id)
        {
            if (!_container.ContainsKey(guid))
            {
                _container.Add(guid, new Model { Object = model, Id = id });
            }
            else
            {
                _container[guid] = new Model { Object = model, Id = id };
            }
        }

        public void RemoveModel(Guid guid)
        {
            if (_container.ContainsKey(guid))
            {
                _container.Remove(guid);
            }
        }

        public GameObject GetModel(Guid guid)
        {
            if (_container.ContainsKey(guid))
            {
                return _container[guid].Object;
            }
            return null;
        }

        public SerializableWorld GetSerialized()
        {
            var serializableWorld = new SerializableWorld() { Objects = new SerializableObject[_container.Count] };
            var index = 0;
            foreach (var model in _container)
            {
                var serializableObject = new SerializableObject() { ID = model.Value.Id, Position = model.Value.Object.transform.position };
                serializableWorld.Objects[index] = serializableObject;
                index++;
            }
            return serializableWorld;
        }

        class Model
        {
            public GameObject Object;
            public int Id;
        }
    }
}