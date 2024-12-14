using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragandDropImage : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public enum DragType
    {
        SingleImage,    
        MultipleImage   
    }

    public static bool canDrag = true;
    public DragType dragType;

    private RectTransform dragRectTransform;
    private Transform originalParent;
    private Vector3 originalPosition;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    public string allowedSlotName;
    private GameObject clone;
    private Image imageComponent;
    private bool isPlaced = false; // Para rastrear si la imagen está en un slot

    void Start()
    {
        dragRectTransform = GetComponent<RectTransform>();
        originalParent = transform.parent;
        originalPosition = transform.localPosition;
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        imageComponent = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canDrag && !isPlaced) // Solo permitir arrastrar si no está colocada
        {
            clone = Instantiate(gameObject, transform.position, transform.rotation, originalParent);
            
            CanvasGroup cloneCanvasGroup = clone.GetComponent<CanvasGroup>();
            if (cloneCanvasGroup != null)
            {
                cloneCanvasGroup.alpha = 0.7f;
                cloneCanvasGroup.blocksRaycasts = true;
            }

            dragRectTransform = clone.GetComponent<RectTransform>();
            dragRectTransform.anchoredPosition += new Vector2(0, 60f);
            
            // Asegurarse de que el clon tenga este script y configurarlo correctamente
            DragandDropImage cloneScript = clone.GetComponent<DragandDropImage>();
            if (cloneScript != null)
            {
                cloneScript.allowedSlotName = this.allowedSlotName;
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canDrag && !isPlaced)
        {
            dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            if (clone != null)
            {
                clone.GetComponent<CanvasGroup>().blocksRaycasts = true;
                clone.GetComponent<CanvasGroup>().alpha = 1f;

                if (eventData.pointerEnter != null)
                {
                    Debug.Log("Se detectó un objeto al soltar: " + eventData.pointerEnter.name);

                    if (eventData.pointerEnter.CompareTag("Slot") && eventData.pointerEnter.name == allowedSlotName)
                    {
                        Debug.Log("Soltado sobre el slot permitido: " + allowedSlotName);

                        if (dragType == DragType.SingleImage)
                        {
                            Image[] existingImages = eventData.pointerEnter.GetComponentsInChildren<Image>();
                            foreach (Image img in existingImages)
                            {
                                if (img.transform != eventData.pointerEnter.transform)
                                {
                                    Destroy(img.gameObject);
                                }
                            }
                        }

                        clone.transform.SetParent(eventData.pointerEnter.transform);
                        
                        RectTransform slotRect = eventData.pointerEnter.GetComponent<RectTransform>();
                        RectTransform imageRect = clone.GetComponent<RectTransform>();
                        
                        imageRect.anchorMin = new Vector2(0.5f, 0.5f);
                        imageRect.anchorMax = new Vector2(0.5f, 0.5f);
                        imageRect.pivot = new Vector2(0.5f, 0.5f);
                        imageRect.anchoredPosition = Vector2.zero;
                        imageRect.sizeDelta = slotRect.sizeDelta * 0.9f;

                        // Marcar el clon como colocado
                        DragandDropImage cloneScript = clone.GetComponent<DragandDropImage>();
                        if (cloneScript != null)
                        {
                            cloneScript.isPlaced = true;
                        }
    
                    }
                    else
                    {
                        Debug.Log("El objeto soltado no es el slot permitido.");
                        Destroy(clone);
                    }
                }
                else
                {
                    Debug.Log("No se detectó ningún objeto al soltar");
                    Destroy(clone);
                }
            }
        }
    }

    // Nuevo método para manejar los clicks
    public void OnPointerClick(PointerEventData eventData)
    {
        if (isPlaced)
        {
            Debug.Log("Imagen clickeada - destruyendo");
            Destroy(gameObject);
        }
    }
}