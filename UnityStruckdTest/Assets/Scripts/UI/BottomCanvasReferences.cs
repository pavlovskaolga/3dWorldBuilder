using UnityEngine;
using UnityEngine.UI;

namespace UnityStruckdTest.UI
{
    public class BottomCanvasReferences : MonoBehaviour
    {
        [SerializeField]
        private Transform _buildableRoot = default;
        public Transform BuildableRoot => _buildableRoot;
        [SerializeField]
        private ScrollRect _scrollRect = default;
        public ScrollRect ScrollRect => _scrollRect;
        [SerializeField]
        private RectTransform _bottomPanel = default;
        public RectTransform BottomPanel => _bottomPanel;
        [SerializeField]
        private Button _undo = default;
        public Button Undo => _undo;
    }
}