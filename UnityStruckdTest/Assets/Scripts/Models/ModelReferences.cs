using UnityEngine;
using UnityEngine.UI;

namespace UnityStruckdTest.Models
{
    public class ModelReferences : MonoBehaviour
    {
        [SerializeField]
        private Button _destroyButton = default;
        public Button  DestroyButton => _destroyButton;
        [SerializeField]
        private Canvas _canvas = default;
        [SerializeField]
        private RectTransform _outline = default;
        public RectTransform Outline => _outline;
        [SerializeField]
        private BoxCollider _collider = default;
        public BoxCollider Collider => _collider;

        public void Activate(bool activate)
        {
            _canvas.gameObject.SetActive(activate);
        }
    }
}