using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    [Header("Hide and Show")]
    [SerializeField] float hideTtime = 0.8f;
    [SerializeField] float showTime = 0.4f;

    Image backgroundImg;
    Image handleImg;

    Vector2 defaultPosition = Vector2.zero;

    protected override void Start()
    {
        base.Start();
        //background.gameObject.SetActive(false);
        
        backgroundImg = background.GetComponent<Image>();
        handleImg = handle.GetComponent<Image>();

        defaultPosition = background.anchoredPosition;

        setJoystickAlpha(0.1f, hideTtime);

    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        setJoystickAlpha(1, showTime);
       // parentBG.rectTransform.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);

        //background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        //background.gameObject.SetActive(false);
        background.anchoredPosition = defaultPosition;
        //parentBG.rectTransform.anchoredPosition = defaultPosition;
        setJoystickAlpha(0.1f, hideTtime);
        base.OnPointerUp(eventData);
    }

    Coroutine coroutine = null;

    void setJoystickAlpha(float value, float time) {

        if (coroutine != null) {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(IEJoystickAlpha(value, time));

    }

    IEnumerator IEJoystickAlpha(float value , float time) {
        float actualTime = 0;
        float progress = 0;
        float alpha = backgroundImg.color.a;
        while (time != actualTime) {

            actualTime = Mathf.Min(actualTime + 0.05f, time);

            progress = actualTime / time;

            alpha = Mathf.Lerp(alpha, value, progress);

            //Debug.Log(alpha);

            Color a = backgroundImg.color;
            Color b = handleImg.color;

            a.a = alpha;
            b.a = alpha;

            backgroundImg.color = a;
            handleImg.color = b;
            

            yield return new WaitForSeconds(0.05f);

        }
    }

}