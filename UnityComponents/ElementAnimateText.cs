using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElementAnimateText : AnimatedElementController
{
    public TextMeshProUGUI AnimateText;
    public string completeText;
    public int maxNonAnimatedCharacter = 0;

    public bool autoStartOnEnable = true;
    public bool setStartingTextAtStart = true;

    public bool setStartingTextAtStop = false;
    public bool setTargetTextAtStop = true;
    public float durationInSecondsPerCharacter = 1.0f;
    public float delay = 0.0f;
    private float speed = 1.0f;
    private int diff;
    public AnimationCurve curve;
    private bool isPlayingBackwards = false;
    private bool isAnimationFinished = false;

    public override void OnEnable()
    {
        completeText += " ";
        if (autoStartOnEnable)
        {            
            SwitchAnimation(true);
        }
    }   

    public override void OnDisable()
    {
        if (IsAnimating)
        {
            SwitchAnimation(false);
        }
    }

    public void SetStartingText()
    {
        AnimateText.text = completeText.Substring(0, maxNonAnimatedCharacter);
    }

    public void SetEndingText()
    {
        AnimateText.text = completeText;
    }

    public override void Reset()
    {
        progress = 0.0f;
    }

    public override void ResetToStartingPoint()
    {
        Reset();
        SetStartingText();
    }

    public string getSubString(int maxIndex)
    {
        return completeText.Substring(0, (maxNonAnimatedCharacter + maxIndex));
    }

    public override void Play()
    {
        if (gameObject.activeInHierarchy)
        {
            speed = 1.0f / durationInSecondsPerCharacter;
            diff = completeText.Length - maxNonAnimatedCharacter;
            isAnimationFinished = false;
            isPlayingBackwards = false;
            if (setStartingTextAtStart)
            {
                SetStartingText();
            }
            StartCoroutine("TextUp");
        }
    }

    public override void Stop()
    {
        isAnimationFinished = true;
        if (setStartingTextAtStop)
        {
            SetStartingText();
        }
        else if (setTargetTextAtStop)
        {
            SetEndingText();
        }
        StopCoroutine("TextUp");
        StopCoroutine("MustWaitOtherAnimatedControllers");
    }

    IEnumerator TextUp()
    {
        //HACK to avoid first time playing on autostart
        yield return 0;

        yield return StartCoroutine("MustWaitOtherAnimatedControllers");

        if (delay > 0.0f)
        {
            yield return new WaitForSeconds(delay);
        }
        while (!isAnimationFinished)
        {
            switch (animationType)
            {
                case ELEMENT_ANIMATION_TYPE.LOOP:
                    {
                        progress += Time.deltaTime * speed;
                        int maxLenght = (int)(diff * curve.Evaluate(progress));
                        AnimateText.text = getSubString(maxLenght);
                        if (progress >= 1.0f)
                        {
                            progress = 0.0f;
                        }
                    }
                    break;                           
                case ELEMENT_ANIMATION_TYPE.SINGLE:
                    {
                        progress += Time.deltaTime * speed;
                        int maxLenght = (int)(diff * curve.Evaluate(progress));
                        AnimateText.text = getSubString(maxLenght);
                        if (progress >= 1.0f)
                        {
                            progress = 1.0f;
                            isAnimationFinished = true;
                        }
                    }
                    break;
            }
            yield return 0;
        }
        SwitchAnimation(false);
    }
}
