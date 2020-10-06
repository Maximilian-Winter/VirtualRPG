using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlotDragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private GameObject draggedItem;
    private bool isDraggedItemThere;

    public GameObject DraggedItem { get => draggedItem; set => draggedItem = value; }

    private void Awake()
    {
        canvas = GameObject.Find("UI").GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        isDraggedItemThere = false;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(gameObject.GetComponent<EquipmentSlot>().GetItem() != null)
        {
            DraggedItem = Instantiate(gameObject, transform.position, transform.rotation, canvas.transform);
            DraggedItem.GetComponent<EquipmentSlot>().SetItem(gameObject.GetComponent<EquipmentSlot>().GetItem());
            DraggedItem.GetComponent<CanvasGroup>().alpha = .6f;
            DraggedItem.GetComponent<CanvasGroup>().blocksRaycasts = false;

            gameObject.GetComponent<EquipmentSlot>().RemoveItem();
            isDraggedItemThere = true;
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        if (isDraggedItemThere)
        {
            DraggedItem.GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDraggedItemThere)
        {
            DraggedItem.GetComponent<CanvasGroup>().alpha = 1f;
            DraggedItem.GetComponent<CanvasGroup>().blocksRaycasts = true;
            Destroy(DraggedItem);
            isDraggedItemThere = false;
        }  
    }
}
