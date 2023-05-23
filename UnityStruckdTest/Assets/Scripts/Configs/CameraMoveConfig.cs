using System;
using UnityEngine;

namespace UnityStruckdTest.Configs
{
    [CreateAssetMenu(menuName = "Configs/CameraMoveConfig", fileName = "CameraMoveConfig")]
    public class CameraMoveConfig : ScriptableObject
    {
        [SerializeField]
        private int _sizeMax = 60;
        public int SizeMax => _sizeMax;
        [SerializeField]
        private int _sizeMin = 20;
        public int SizeMin => _sizeMin;
    }
}
