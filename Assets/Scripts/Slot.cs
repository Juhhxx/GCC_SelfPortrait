using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    int id;
    public int Id
    {
        get => id;

        set
        {
            id = value;
        }
    }
    private int pieceId = 0;
    
    private float pieceRotation;
    private bool isPieceCorrect = false;
    public bool IsPieceCorrect => isPieceCorrect;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Vector4(0f,0f,0f,0.5f);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = new Vector4(0f,0f,0f,0f);
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;
            eventData.pointerDrag.GetComponent<RectTransform>().localScale = rectTransform.localScale;
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
