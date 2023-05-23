using UnityEngine;
using UnityStruckdTest.Configs;
using UnityStruckdTest.UI;

namespace UnityStruckdTest.Data
{
    public class SharedData 
    {
        public Camera Camera { get; set; }
        public BottomCanvasReferences BottomCanvasReferences { get; set; }
        public BuildButtonReferences BuildButtonReferences { get; set; }
        public BuildablesConfig BuildablesConfig { get; set; }
        public CameraMoveConfig CameraMoveConfig { get; set; }
        public UndoBuffer UndoBuffer { get; set; }
        public ModelsContainer ModelsContainer { get; set; }

    }
}
