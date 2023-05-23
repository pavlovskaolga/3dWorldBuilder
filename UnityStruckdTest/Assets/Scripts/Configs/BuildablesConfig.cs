using System;
using UnityEngine;

namespace UnityStruckdTest.Configs
{
    [CreateAssetMenu(menuName = "Configs/BuildablesConfig", fileName = "BuildablesConfig")]
    public class BuildablesConfig : ScriptableObject
    {
        [SerializeField]
        private BuildableData[] _buildables = null;
        public BuildableData[] Buildables => _buildables;

        public GameObject GetBuildableByID(int iD)
        {
            foreach (var buildable in _buildables)
            {
                if (iD == buildable.Prefab.GetInstanceID())
                {
                    return buildable.Prefab;
                }
            }
            return null;
        }
    }

    [Serializable]
    public class BuildableData
    {
        [SerializeField]
        private GameObject _prefab = default;
        public GameObject Prefab => _prefab;
        [SerializeField]
        private Sprite _icon = default;
        public Sprite Icon => _icon;
    }
}
