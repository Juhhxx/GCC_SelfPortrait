using UnityEngine;
using UnityEngine.EventSystems;


public class Drag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] [Range (0,1)] private float alphaValue = 0.5f;
    private int id;
    public int Id
    {
        get => id;

        set
        {
            id = value;
        }
    }
    private CanvasGroup gCanvas;
    private RectTransform rectTrans;

    private void Awake()
    {
        rectTrans = GetComponent<RectTransform>();
        gCanvas = GetComponent<CanvasGroup>();
    }
    private void Start()
    {
        float rotation = Mathf.Floor(Random.Range(0,4)) * 90;
        rectTrans.rotation = Quaternion.Euler(0f,0f,rotation);
    }
    private void Update()
    {
        if (!gCanvas.blocksRaycasts)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector3 newRotation = rectTrans.rotation.eulerAngles;
                newRotation.z -= 90f;
                rectTrans.rotation = Quaternion.Euler(newRotation.x,newRotation.y,newRotation.z);
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
    public void OnBeginDrag(PointerEventData eventDrag)
    {
        gCanvas.alpha = alphaValue;
        if (rectTrans.localScale.x != 2)
            rectTrans.localScale = new Vector3(2.5f,2.5f,2.5f);
        gCanvas.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTrans.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        gCanvas.alpha = 1f;
        gCanvas.blocksRaycasts = true;
    }
    
}
