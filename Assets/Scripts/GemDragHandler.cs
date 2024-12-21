using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Transform originalParent;
    private GameObject draggedItem;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originalPosition = rectTransform.anchoredPosition;
        originalParent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        draggedItem = Instantiate(gameObject, transform.position, Quaternion.identity, canvas.transform);
        draggedItem.GetComponent<CanvasGroup>().alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggedItem != null)
        {
            draggedItem.GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter != null)
        {
            GemSlot gemSlot = eventData.pointerEnter.GetComponent<GemSlot>();

            if (gemSlot != null )
            {
                // Colocar la gema en el slot
               // gemSlot.AddGem(gameObject);

                // Ajustar posición en el slot
                RectTransform slotRect = gemSlot.GetComponent<RectTransform>();
                draggedItem.GetComponent<RectTransform>().position = slotRect.position;
            }
            else
            {
                Debug.Log("Slot no válido o lleno");
                Destroy(draggedItem);
            }
        }
        else
        {
            Destroy(draggedItem);
        }
    }
}