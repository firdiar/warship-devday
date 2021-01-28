using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedJoystick : Joystick, IPointerUpHandler
{
    public bool resetOnTouchUp = true;
    public override void OnPointerUp(PointerEventData eventData)
    {
        if (resetOnTouchUp)
            base.OnPointerUp(eventData);
    }
}