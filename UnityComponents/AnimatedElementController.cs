using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AnimatedElementController : MonoBehaviour 
{


    public bool doessomethingWhen;
    public float targetPercentage;
    public UnityEvent whatToDo;
    protected bool doneOnce = false;

    public enum ELEMENT_ANIMATION_TYPE
	{
		SINGLE,
		LOOP,
		PINGPONG,
		PINGPONGONCE
	}

	public ELEMENT_ANIMATION_TYPE animationType = ELEMENT_ANIMATION_TYPE.SINGLE;
	private bool isAnimating = false;
	protected float progress = 0.0f;
	public abstract void OnEnable();
	public abstract void OnDisable();
	public abstract void Play();
	public abstract void Stop();
	public abstract void Reset();

    public abstract void ResetToStartingPoint();

	public RelatedAC[] relatedAnimatedControllers;

	public bool IsAnimating
	{
		get
		{
			return isAnimating;
		}
	}

	public void SwitchAnimation(bool enable)
	{
		isAnimating = enable;
		if(enable)
		{
			Reset();
			Play();
		}
		else
		{
			Stop();
			Reset();
		}
	}

	public float GetAnimationProgress()
	{
		return progress;
	}

	protected IEnumerator MustWaitOtherAnimatedControllers()
	{
		bool mustWait = MustWaitOtherACs();
		while(mustWait)
		{
			yield return 0;
			mustWait = MustWaitOtherACs();
		}
	}

	private bool MustWaitOtherACs()
	{
		bool result = false;
		for(int i = 0; i < relatedAnimatedControllers.Length ; i++)
		{
			result |= !relatedAnimatedControllers[i].IsRelatedControllerReady();
		}
		return result;
	}

    public void evaluateTargetProgress()
    {
        if (progress >= targetPercentage && !doneOnce && doessomethingWhen)
        {
            whatToDo.Invoke();
        }
    }

}    

[System.Serializable]
public class RelatedAC
{
	public AnimatedElementController relatedController;
	public float advanceNeeded = 0.0f;

	public bool IsRelatedControllerReady()
	{
		if(relatedController != null)
		{
			return relatedController.GetAnimationProgress() >= advanceNeeded;		
		}
		return true;
	}
}
