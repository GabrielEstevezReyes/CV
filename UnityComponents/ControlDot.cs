using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDot : MonoBehaviour
{
    public bool independentConrner;
    public Camera searchCam;
    public RectTransform currentRect;
    public ResizableComponent whoAmIresizing;
    public bool canMoveOnX;
    public bool canMoveOnY;
    public BorderMaskManager border;
    public ControlDot partnerX;
    public ControlDot partnerX2;
    public ControlDot partnerY;
    public ControlDot partnerY2;
    public ControlDot recenterPartner1;
    public ControlDot recenterPartner2;

    public Vector3 previousPoint = new Vector3(0, 0, 0);

    protected Vector2 WorldUnitsInCamera;
    protected Vector2 WorldToPixelAmount;

    protected bool touchDetected;
    public Vector3 previousInput;

    private void Awake()
    {
        WorldUnitsInCamera.y = searchCam.orthographicSize * 2;
        WorldUnitsInCamera.x = WorldUnitsInCamera.y * Screen.width / Screen.height;

        WorldToPixelAmount.x = Screen.width / WorldUnitsInCamera.x;
        WorldToPixelAmount.y = Screen.height / WorldUnitsInCamera.y;
    }

    void Update()
    {
        #if UNITY_EDITOR
            resolveInputOnEditor();
        #else
            resolveInputOnDevice();
        #endif
        currentRect.sizeDelta = new Vector2(Screen.width * 0.05f, Screen.width * 0.05f);
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
        if (input.x >= (thisCenter.x - (currentRect.sizeDelta.x)) && input.x < (thisCenter.x + (currentRect.sizeDelta.x))
            && input.y > (thisCenter.y - (currentRect.sizeDelta.y)) && input.y < (thisCenter.y + (currentRect.sizeDelta.y)))
        {
            touchDetected = true;
        }
    }

    protected void recenter()
    {
        if (canMoveOnX)
        {
            currentRect.position = new Vector3(currentRect.position.x, (partnerX.currentRect.position.y + partnerX2.currentRect.position.y) * 0.5f, 0);
        }
        else if (canMoveOnY)
        {
            currentRect.position = new Vector3((partnerX.currentRect.position.x + partnerX2.currentRect.position.x) * 0.5f, currentRect.position.y, 0);
        }
        currentRect.localPosition = new Vector3(currentRect.localPosition.x, currentRect.localPosition.y, 0);
    }

    protected void recenterFromCorner()
    {
        if (canMoveOnX)
        {
            currentRect.position = new Vector3((partnerX.currentRect.position.x + partnerX2.currentRect.position.x) * 0.5f, (partnerX.currentRect.position.y + partnerX2.currentRect.position.y) * 0.5f, 0);
        }
        else if (canMoveOnY)
        {
            currentRect.position = new Vector3((partnerX.currentRect.position.x + partnerX2.currentRect.position.x) * 0.5f, (partnerX.currentRect.position.y + partnerX2.currentRect.position.y) * 0.5f, 0);
        }
        currentRect.localPosition = new Vector3(currentRect.localPosition.x, currentRect.localPosition.y, 0);
    }

    public void adjustWithVector(Vector3 moveTo)
    {
        Vector3 nPos = searchCam.ScreenToWorldPoint(moveTo);
        currentRect.position = nPos;
        currentRect.localPosition = new Vector3(currentRect.localPosition.x, currentRect.localPosition.y, 0);
        previousInput = moveTo;
        previousPoint = currentRect.position;
    }

    public void adjustSelectorWithInput(Vector3 input)
    {
        Vector3 nPos = searchCam.ScreenToWorldPoint(input);
        Vector3 newDelta = currentRect.position - nPos;
        if (input.y < (Screen.height * 0.12f) || input.y > (Screen.height * 0.88f))
        {
            return;
        }
        if (!independentConrner)
        {
            if (canMoveOnX && canMoveOnY)
            {
                currentRect.position = nPos;
                partnerX.currentRect.position = new Vector3(nPos.x, partnerX.currentRect.position.y, 0);
                partnerX2.currentRect.position = new Vector3(nPos.x, partnerX2.currentRect.position.y, 0);
                partnerY.currentRect.position = new Vector3(partnerY.currentRect.position.x, nPos.y, 0);
                partnerY2.currentRect.position = new Vector3(partnerY2.currentRect.position.x, nPos.y, 0);
                partnerX.currentRect.localPosition = new Vector3(partnerX.currentRect.localPosition.x, partnerX.currentRect.localPosition.y, 0);
                partnerX2.currentRect.localPosition = new Vector3(partnerX2.currentRect.localPosition.x, partnerX2.currentRect.localPosition.y, 0);
                partnerY.currentRect.localPosition = new Vector3(partnerY.currentRect.localPosition.x, partnerY.currentRect.localPosition.y, 0);
                partnerY2.currentRect.localPosition = new Vector3(partnerY2.currentRect.localPosition.x, partnerY2.currentRect.localPosition.y, 0);
            }
            else if (canMoveOnX)
            {
                currentRect.position = new Vector3(nPos.x, currentRect.position.y);
            }
            else if (canMoveOnY)
            {
                currentRect.position = new Vector3(currentRect.position.x, nPos.y);
            }
        }
        else
        {
            if (canMoveOnX && canMoveOnY)
            {
                currentRect.position = nPos;
                recenterPartner2.recenterFromCorner();
                recenterPartner1.recenterFromCorner();
            }
            else if (canMoveOnX)
            {
                currentRect.position = new Vector3(nPos.x, currentRect.position.y);
                partnerX.currentRect.position = new Vector3(partnerX.currentRect.position.x - newDelta.x, partnerX.currentRect.position.y, 0);
                partnerX2.currentRect.position = new Vector3(partnerX2.currentRect.position.x - newDelta.x, partnerX2.currentRect.position.y, 0);
                partnerX.currentRect.localPosition = new Vector3(partnerX.currentRect.localPosition.x, partnerX.currentRect.localPosition.y, 0);
                partnerX2.currentRect.localPosition = new Vector3(partnerX2.currentRect.localPosition.x, partnerX2.currentRect.localPosition.y, 0);
                recenterPartner2.recenter();
                recenterPartner1.recenter();                
            }
            else if (canMoveOnY)
            {
                currentRect.position = new Vector3(currentRect.position.x, nPos.y);
                partnerX.currentRect.position = new Vector3(partnerX.currentRect.position.x, partnerX.currentRect.position.y - newDelta.y, 0);
                partnerX2.currentRect.position = new Vector3(partnerX2.currentRect.position.x, partnerX2.currentRect.position.y - newDelta.y, 0);
                partnerX.currentRect.localPosition = new Vector3(partnerX.currentRect.localPosition.x, partnerX.currentRect.localPosition.y, 0);
                partnerX2.currentRect.localPosition = new Vector3(partnerX2.currentRect.localPosition.x, partnerX2.currentRect.localPosition.y, 0);
                recenterPartner2.recenter();
                recenterPartner1.recenter();
            }            
        }
        previousInput = input;
        previousPoint = currentRect.position;
        currentRect.localPosition = new Vector3(currentRect.localPosition.x, currentRect.localPosition.y, 0);
        border.border.setLines();
    }

    public void positionToPixels()
    {
        Vector3 npos = searchCam.WorldToScreenPoint(currentRect.position);
        previousInput = npos;
    }
}
