using UnityEngine;
using UnityEngine.EventSystems;

public class CronoTrigger : EventTrigger
{
    public override void OnPointerClick(PointerEventData data)
    {
        Debug.Log("OnPointerClick called.");
    }
}
