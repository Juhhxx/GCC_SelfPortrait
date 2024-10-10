using TMPro;
using UnityEngine;

public class IdSetter : MonoBehaviour
{
    private Slot[] slotList;
    private RectTransform rectTrans;
    private Vector2 midPoint;
    private TextMeshProUGUI idText;

    void Start()
    {
        slotList = FindObjectsByType<Slot>(0);
        rectTrans = GetComponent<RectTransform>();
        midPoint = rectTrans.anchoredPosition;
        SetAllIds();
    }

    private void SetAllIds()
    {
        foreach (Slot slot in slotList)
        {
            idText = slot.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            Vector2 slotTrans = slot.gameObject.GetComponent<RectTransform>().anchoredPosition;
            float slotX = slotTrans.x;
            float slotY = slotTrans.y;

            if (slotX < midPoint.x)
                if (slotY < midPoint.y)
                    slot.Id = 7;
                else if (slotY > midPoint.y)
                    slot.Id = 1;
                else
                    slot.Id = 4;
            else if (slotX > midPoint.x)
                if (slotY < midPoint.y)
                    slot.Id = 9;
                else if (slotY > midPoint.y)
                    slot.Id = 3;
                else
                    slot.Id = 6;
            else
                if (slotY < midPoint.y)
                    slot.Id = 8;
                else if (slotY > midPoint.y)
                    slot.Id = 2;
                else
                    slot.Id = 5;
            
            Debug.Log($"MidPoint at ({midPoint.x},{midPoint.y})");
            Debug.Log($"{slot.gameObject} at ({slotX},{slotY}) set as id : {slot.Id}");
            idText.text = $"{slot.Id}";
        }
    }
}
