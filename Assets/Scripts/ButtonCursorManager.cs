using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonCursorManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!CursorManager.Instance.changable) return;
        CursorManager.Instance.SetCursorLink();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!CursorManager.Instance.changable) return;
        CursorManager.Instance.SetCursorNormal();
    }
}
