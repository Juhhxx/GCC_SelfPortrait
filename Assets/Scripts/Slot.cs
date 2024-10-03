using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler
{
    private RectTransform rectTransform;
    [SerializeField] int id;
    private int pieceId = 0;
    private float pieceRotation;
    private bool isPieceCorrect = false;
    public bool IsPieceCorrect => isPieceCorrect;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;
            pieceRotation = eventData.pointerDrag.GetComponent<RectTransform>().rotation.z;
            pieceId = eventData.pointerDrag.GetComponent<Drag>().Id;

            if (pieceId == id)
                if (pieceRotation == 0)
                    isPieceCorrect = true;
        }
    }
    private void Update()
    {
        
    }
}
