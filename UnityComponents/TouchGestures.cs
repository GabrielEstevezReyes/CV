using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class TouchGestures : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler,  IDragHandler, IEndDragHandler, IScrollHandler
{
	public ScrollRect scrollRect;
	[SerializeField]
	public bool pointerDown;
	private float pointerDownTimer;

	[SerializeField]
	private float requiredHoldTime;

	public UnityEvent onLongClick;
	public UnityEvent onShortClick;

	public void OnBeginDrag(PointerEventData eventData)
    {
        scrollRect.OnBeginDrag(eventData);
    }
 
 
    public void OnDrag(PointerEventData eventData)
    {
        scrollRect.OnDrag(eventData);
    }
 
    public void OnEndDrag(PointerEventData eventData)
    {
        scrollRect.OnEndDrag(eventData);
    } 
 
    public void OnScroll(PointerEventData data)
    {
        scrollRect.OnScroll(data);
    }

	public void OnPointerDown(PointerEventData eventData)
	{
		pointerDown = true;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if(pointerDown && scrollRect.velocity.y == 0)
		{
			if (pointerDownTimer < requiredHoldTime)
			{
				if (onShortClick != null)
				{
					onShortClick.Invoke();
				}
				Reset();
			}
		}
		Reset();
	}

	private void Update()
	{
		if(scrollRect != null && scrollRect.velocity.x != 0)
		{
			pointerDown = false;
		}
		if (pointerDown)
		{
			pointerDownTimer += Time.deltaTime;
			if (pointerDownTimer >= requiredHoldTime)
			{
				if (onLongClick != null)
                {
					onLongClick.Invoke();
                }
				Reset();
			}
		}
	}

	private void Reset()
	{
		pointerDown = false;
		pointerDownTimer = 0;
	}
}