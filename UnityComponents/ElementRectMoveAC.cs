using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementRectMoveAC : AnimatedElementController 
{
	public bool autoStartOnEnable = true;
	public bool setStartingAnchorAndPositionAtStart = true;
	public bool setTargetAnchorAndPositionAtStop = true;

    public Vector2 originMinAnchor = Vector3.zero;
	public Vector2 originMaxAnchor = Vector3.zero;
	public Vector2 destinyMinAnchor = Vector3.zero;
	public Vector2 destinyMaxAnchor = Vector3.zero;

	private Vector2 diffMinAnchor;
	private Vector2 diffMaxAnchor;

	public float duration = 1.0f;
	public float delay = 0.0f;
	private float speed = 1.0f;
	public AnimationCurve curve;
	private bool isPlayingBackwards = false;
	public bool isAnimationFinished = false;

	public bool setOriginAnchorsOnEnable = false;
	public bool setDestinyPositionAsOriginAnchorOffset = false;
    public bool setAnchorsFromOriginOnEnable = false;
    public bool autoResetOnDisable = false;
    private RectTransform cachedRectTransform;

	public void SetStartAndEndPositions(Vector2 startMinAnchor, Vector2 startMaxAnchor, Vector2 endMinAnchor, Vector2 endMaxAnchor)
	{
		originMinAnchor = startMinAnchor;
		originMaxAnchor = startMaxAnchor;
		destinyMinAnchor = endMinAnchor;
		destinyMaxAnchor = endMaxAnchor;
	}

   

	public override void OnEnable ()
	{
        if(cachedRectTransform == null)
        {
            cachedRectTransform = GetComponent<RectTransform>();
        }

		if(setOriginAnchorsOnEnable)
		{
			originMinAnchor = cachedRectTransform.anchorMin;
			originMaxAnchor = cachedRectTransform.anchorMax;
		}
        if (setAnchorsFromOriginOnEnable)
        {
            cachedRectTransform.anchorMin = originMinAnchor;
            cachedRectTransform.anchorMax = originMaxAnchor;
            cachedRectTransform.ResetOffsets();
        }

		if(setDestinyPositionAsOriginAnchorOffset)
		{
			destinyMinAnchor = originMinAnchor + destinyMinAnchor;
			destinyMaxAnchor = originMaxAnchor + destinyMaxAnchor;
		}
       

		if(autoStartOnEnable)
		{
			SwitchAnimation(true);
		}
	}

	public override void OnDisable ()
	{
        if (cachedRectTransform == null)
        {
            cachedRectTransform = GetComponent<RectTransform>();
        }

        if (IsAnimating)
		{
			SwitchAnimation(false);
		}

        if(autoResetOnDisable)
        {
            ResetToStartingPoint();
        }
	}

	public override void Reset ()
	{
		progress = 0.0f;
	}

    public override void ResetToStartingPoint()
    {
        Reset();
        cachedRectTransform.anchorMin = originMinAnchor;
        cachedRectTransform.anchorMax = originMaxAnchor;
        cachedRectTransform.ResetOffsets();
    }

    public override void Play ()
	{
		if(gameObject.activeInHierarchy)
		{
			speed = 1.0f/duration;
			diffMinAnchor = destinyMinAnchor - originMinAnchor;
			diffMaxAnchor = destinyMaxAnchor - originMaxAnchor;
			isAnimationFinished = false;
			isPlayingBackwards = false;
			if(setStartingAnchorAndPositionAtStart)
			{
				cachedRectTransform.anchorMin = originMinAnchor;
				cachedRectTransform.anchorMax = originMaxAnchor;
				cachedRectTransform.ResetOffsets();
			}
			StartCoroutine("Move");
		}
	}

	public override void Stop ()
	{
		isAnimationFinished = true;
		if(setTargetAnchorAndPositionAtStop)
		{
			cachedRectTransform.anchorMin = destinyMinAnchor;
			cachedRectTransform.anchorMax = destinyMaxAnchor;
			cachedRectTransform.ResetOffsets();
		}
		StopCoroutine("Move");
		StopCoroutine("MustWaitOtherAnimatedControllers");
	}

	IEnumerator Move()
	{
		//HACK to avoid first time playing on autostart
		yield return 0;

		yield return StartCoroutine("MustWaitOtherAnimatedControllers");

		if(delay > 0.0f)
		{
			yield return new WaitForSeconds(delay);
		}
		while(!isAnimationFinished)
		{
			switch(animationType)
			{
			case ELEMENT_ANIMATION_TYPE.LOOP:
				{
					progress += Time.deltaTime*speed;

					Vector2 newMinAnchor = originMinAnchor + diffMinAnchor*curve.Evaluate(progress);
					Vector2 newMaxAnchor = originMaxAnchor + diffMaxAnchor*curve.Evaluate(progress);

					cachedRectTransform.anchorMin = newMinAnchor;
					cachedRectTransform.anchorMax = newMaxAnchor;

					if(progress >= 1.0f)
					{
						cachedRectTransform.anchorMin = destinyMinAnchor;
						cachedRectTransform.anchorMax = destinyMaxAnchor;

						progress = 0.0f;
					}
					cachedRectTransform.ResetOffsets();
				}
				break;
			case ELEMENT_ANIMATION_TYPE.PINGPONG:
				{
					if(isPlayingBackwards)
					{
						progress -= Time.deltaTime*speed;

						Vector2 newMinAnchor = originMinAnchor + diffMinAnchor*curve.Evaluate(progress);
						Vector2 newMaxAnchor = originMaxAnchor + diffMaxAnchor*curve.Evaluate(progress);

						cachedRectTransform.anchorMin = newMinAnchor;
						cachedRectTransform.anchorMax = newMaxAnchor;

						if(progress <= 0.0f)
						{
							cachedRectTransform.anchorMin = originMinAnchor;
							cachedRectTransform.anchorMax = originMaxAnchor;
							progress = 0.0f;
							isPlayingBackwards = false;
						}
						cachedRectTransform.ResetOffsets();
					}
					else
					{
						progress += Time.deltaTime*speed;

						Vector2 newMinAnchor = originMinAnchor + diffMinAnchor*curve.Evaluate(progress);
						Vector2 newMaxAnchor = originMaxAnchor + diffMaxAnchor*curve.Evaluate(progress);

						cachedRectTransform.anchorMin = newMinAnchor;
						cachedRectTransform.anchorMax = newMaxAnchor;

						if(progress >= 1.0f)
						{
							cachedRectTransform.anchorMin = destinyMinAnchor;
							cachedRectTransform.anchorMax = destinyMaxAnchor;
							progress = 1.0f;
							isPlayingBackwards = true;
						}
						cachedRectTransform.ResetOffsets();
					}
				}
				break;
			case ELEMENT_ANIMATION_TYPE.PINGPONGONCE:
				if(isPlayingBackwards)
				{
					progress -= Time.deltaTime*speed;

					Vector2 newMinAnchor = originMinAnchor + diffMinAnchor*curve.Evaluate(progress);
					Vector2 newMaxAnchor = originMaxAnchor + diffMaxAnchor*curve.Evaluate(progress);

					cachedRectTransform.anchorMin = newMinAnchor;
					cachedRectTransform.anchorMax = newMaxAnchor;

					if(progress <= 0.0f)
					{
						cachedRectTransform.anchorMin = originMinAnchor;
						cachedRectTransform.anchorMax = originMaxAnchor;

						progress = 0.0f;
						isPlayingBackwards = false;
						isAnimationFinished = true;
					}
					cachedRectTransform.ResetOffsets();
				}
				else
				{
					progress += Time.deltaTime*speed;
					Vector2 newMinAnchor = originMinAnchor + diffMinAnchor*curve.Evaluate(progress);
					Vector2 newMaxAnchor = originMaxAnchor + diffMaxAnchor*curve.Evaluate(progress);

					cachedRectTransform.anchorMin = newMinAnchor;
					cachedRectTransform.anchorMax = newMaxAnchor;

					if(progress >= 1.0f)
					{
						cachedRectTransform.anchorMin = destinyMinAnchor;
						cachedRectTransform.anchorMax = destinyMaxAnchor;
						progress = 1.0f;
						isPlayingBackwards = true;
					}
					cachedRectTransform.ResetOffsets();
				}
				break;
			case ELEMENT_ANIMATION_TYPE.SINGLE:
				{
					progress += Time.deltaTime*speed;

					Vector2 newMinAnchor = originMinAnchor + diffMinAnchor*curve.Evaluate(progress);
					Vector2 newMaxAnchor = originMaxAnchor + diffMaxAnchor*curve.Evaluate(progress);

					cachedRectTransform.anchorMin = newMinAnchor;
					cachedRectTransform.anchorMax = newMaxAnchor;

					if(progress >= 1.0f)
					{
						cachedRectTransform.anchorMin = destinyMinAnchor;
						cachedRectTransform.anchorMax = destinyMaxAnchor;
						progress = 1.0f;
						isAnimationFinished = true;
					}
					cachedRectTransform.ResetOffsets();
				}
				break;
			}
			yield return 0;
		}
		SwitchAnimation(false);
	}
}
