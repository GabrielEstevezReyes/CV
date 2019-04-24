using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResizableComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool touchDetected;
    public Camera searchCam;
    public bool hasMinSize;
    public float minSize;
    public RectTransform objectToResize;
    protected Vector2 centerXPos = Vector2.zero;
    public ControlDot topLeft;
    public ControlDot topRight;
    public ControlDot bottomLeft;
    public ControlDot bottomRight;
    public ControlDot centerRight;
    public ControlDot centerDown;
    public ControlDot centerLeft;
    public ControlDot centerTop;
    protected float aspectRatioX = 9;
    protected float aspectRatioY = 16;
    protected float xAnchorSize;
    protected float yAnchorSize;
    protected Vector3 centerOfRectTopLeft;
    protected Vector3 centerOfRectTopRight;
    protected Vector3 centerOfRectbottomLeft;
    protected Vector3 centerOfRectbottomRight;
    protected Vector3 centerOfRectbottomCenter;
    protected Vector3 centerOfRectTopCenter;
    protected Vector3 centerOfRectRightCenter;
    protected Vector3 centerOfRectLeftCenter;
    protected bool inside = false;
    protected Vector3 newSizeDelta;
    protected Vector3 newCenter;

    protected float yPercentInverse;
    protected float xPercentInverse;

    protected Vector2 previousInput;
    protected float xPercent;
    protected float yPercent;
    public int isCorner = 0;
    public float offset = 10.1f;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        inside = true;
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        inside = false;
    }

    private void Awake()
    {
        Vector3 origin = topLeft.searchCam.ScreenToWorldPoint(Vector3.zero);
        Vector3 end = topLeft.searchCam.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));

        xPercentInverse = (end.x - origin.x) / Screen.width;
        yPercentInverse = (end.y - origin.y) / Screen.height;

        xAnchorSize = topLeft.currentRect.position.x - topLeft.currentRect.position.x;
        yAnchorSize = topLeft.currentRect.position.y - topLeft.currentRect.position.y;
        newCenter = transform.localPosition;
        objectToResize.localPosition = new Vector3(0, 0, 0);
        updatePointsPosition(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        resolveInputOnEditor();
#else
            resolveInputOnDevice();
#endif
        newCenter = new Vector3(centerTop.currentRect.position.x, centerLeft.currentRect.position.y, 0);
        newSizeDelta = new Vector2(
            (centerRight.currentRect.position.x - centerLeft.currentRect.position.x) / xPercentInverse,
            (centerTop.currentRect.position.y - centerDown.currentRect.position.y) / yPercentInverse);
        if (touchDetected)
        {
            updatePointsPosition(objectToResize.position);
            updateCenterOfRects();
        }
        else
        {
            if (biggerThaMinSize())
            {
                objectToResize.position = new Vector3(newCenter.x, newCenter.y, 0);
                objectToResize.sizeDelta = newSizeDelta;
                updatePointsPosition(newCenter);
                updateCenterOfRects();
            }
        }
        objectToResize.localPosition = new Vector3(objectToResize.localPosition.x, objectToResize.localPosition.y, 0);
    }

    protected void resolveInputOnEditor()
    {
        if (Input.GetMouseButtonDown(0))
        {            
            previousInput = Input.mousePosition;
            evaluateInput(previousInput);
        }
        else if (Input.GetMouseButton(0))
        {
            if (touchDetected)
            {
                adjustSelectorWithInput(Input.mousePosition);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            touchDetected = false;
        }
    }

    protected void resolveInputOnDevice()
    {
        if (Input.touchCount > 0)
        {

            if (touchDetected)
            {
                adjustSelectorWithInput(Input.GetTouch(0).position);
            }
            else
            {                
                previousInput = Input.GetTouch(0).position;
                evaluateInput(previousInput);
            }
        }
        else
        {
            touchDetected = false;            
        }
    }

    protected void evaluateInput(Vector3 input)
    {

        Vector3 thisCenter = searchCam.WorldToScreenPoint(transform.position);
        if ( inside )
        {
            touchDetected = true;
            centerXPos = thisCenter;
        }
    }

    protected void adjustSelectorWithInput(Vector3 input)
    {
        centerXPos.x += input.x - previousInput.x;
        xPercent = input.x / Screen.width;
        yPercent = input.y / Screen.height;
        centerXPos.y += input.y - previousInput.y;
        Vector3 nPos = searchCam.ScreenToWorldPoint(input);

        objectToResize.position = nPos;
        objectToResize.localPosition = new Vector3(objectToResize.localPosition.x, objectToResize.localPosition.y, 0);
        previousInput = input;
    }

    protected void updatePointsPosition(Vector2 moveTo)
    {
        topLeft.currentRect.position = new Vector2(moveTo.x - (((objectToResize.sizeDelta.x*0.5f))*xPercentInverse), moveTo.y + (((objectToResize.sizeDelta.y* 0.5f)) * yPercentInverse));
        topRight.currentRect.position = new Vector2(moveTo.x + (((objectToResize.sizeDelta.x * 0.5f)) * xPercentInverse), moveTo.y + (((objectToResize.sizeDelta.y * 0.5f)) * yPercentInverse));
        bottomRight.currentRect.position = new Vector2(moveTo.x + (((objectToResize.sizeDelta.x * 0.5f)) * xPercentInverse), moveTo.y - (((objectToResize.sizeDelta.y * 0.5f)) * yPercentInverse));
        bottomLeft.currentRect.position = new Vector2(moveTo.x - (((objectToResize.sizeDelta.x * 0.5f)) * xPercentInverse), moveTo.y - (((objectToResize.sizeDelta.y * 0.5f)) * yPercentInverse));
        centerTop.currentRect.position = new Vector2(moveTo.x, moveTo.y + (((objectToResize.sizeDelta.y * 0.5f)) * yPercentInverse));        
        centerRight.currentRect.position = new Vector2(moveTo.x + (((objectToResize.sizeDelta.x * 0.5f)) * xPercentInverse), moveTo.y);
        centerDown.currentRect.position = new Vector2(moveTo.x, moveTo.y - (((objectToResize.sizeDelta.y * 0.5f)) * yPercentInverse));
        centerLeft.currentRect.position = new Vector2(moveTo.x - (((objectToResize.sizeDelta.x * 0.5f)) * xPercentInverse), moveTo.y);

        topLeft.currentRect.localPosition = new Vector3(topLeft.currentRect.localPosition.x,topLeft.currentRect.localPosition.y,0);
        topRight.currentRect.localPosition = new Vector3(topRight.currentRect.localPosition.x,topRight.currentRect.localPosition.y,0);
        bottomRight.currentRect.localPosition = new Vector3(bottomRight.currentRect.localPosition.x,bottomRight.currentRect.localPosition.y,0);
        bottomLeft.currentRect.localPosition = new Vector3(bottomLeft.currentRect.localPosition.x,bottomLeft.currentRect.localPosition.y,0);
        centerTop.currentRect.localPosition = new Vector3(centerTop.currentRect.localPosition.x,centerTop.currentRect.localPosition.y,0);
        centerRight.currentRect.localPosition = new Vector3(centerRight.currentRect.localPosition.x,centerRight.currentRect.localPosition.y,0);
        centerDown.currentRect.localPosition = new Vector3(centerDown.currentRect.localPosition.x,centerDown.currentRect.localPosition.y,0);
        centerLeft.currentRect.localPosition = new Vector3(centerLeft.currentRect.localPosition.x, centerLeft.currentRect.localPosition.y, 0);

        updateCenterOfRects();
    }

    protected bool biggerThaMinSize()
    {
        if(newSizeDelta.x < minSize)
        {
            return false;
        }
        if(newSizeDelta.y < minSize * (aspectRatioX / aspectRatioY))
        {
            return false;
        }
        return true;
    }

    protected void updateCenterOfRects()
    {
        centerOfRectTopLeft = topLeft.currentRect.localPosition;
        centerOfRectTopRight = topRight.currentRect.localPosition;
        centerOfRectbottomLeft = bottomLeft.currentRect.localPosition;
        centerOfRectbottomRight = bottomRight.currentRect.localPosition;
        centerOfRectbottomCenter = centerDown.currentRect.localPosition;
        centerOfRectTopCenter = centerTop.currentRect.localPosition;
        centerOfRectRightCenter = centerRight.currentRect.localPosition;
        centerOfRectLeftCenter = centerLeft.currentRect.localPosition;
    }
}
