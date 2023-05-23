using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityStruckdTest.UI
{
    public class BuildButtonReferences : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IScrollHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        private Image _icon = default;

        public Image Icon => _icon;
        
        public Action OnPointerDownHandler;
        public Action OnPointerDragHandler;
        public Action OnPointerUpHandler;
        public bool CancelDrag = false;
        private ScrollRect _scrollRect;

        public void Init(Sprite icon, ScrollRect scrollRect)
        {
            _icon.sprite = icon;
            _scrollRect = scrollRect;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _scrollRect.OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (CancelDrag)
            {
                eventData.pointerDrag = null;
                CancelDrag = false;
                return;
            }
            OnPointerDragHandler?.Invoke();
            _scrollRect.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _scrollRect.OnEndDrag(eventData);
        }

        public void OnScroll(PointerEventData data)
        {
            _scrollRect.OnScroll(data);
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownHandler?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnPointerUpHandler?.Invoke();
        }
    }
}