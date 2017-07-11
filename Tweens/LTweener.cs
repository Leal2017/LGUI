using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//the parent of all tween
public abstract class LTweener : MonoBehaviour {

	[HideInInspector]
	public bool tweenerVisible = true;

	[HideInInspector]
	public enum Style {Once, Loop, PingPong};
	[HideInInspector]
	public Style playStyle = Style.Once;
	
	// Curve
	[HideInInspector]
	public AnimationCurve animationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
	// Duration
	[HideInInspector]
	public float duration = 1;
	// Start delay
	[HideInInspector]
	public float startDelay = 0;
	// Ignore Timescale
	[HideInInspector]
	public bool ignoreTimescale = true;
	[HideInInspector]
	public float tweenTimer;
	// boolean used to reverse the tween
	[HideInInspector]
	public int reverse = 0;
	// boolean used to start and stop the tween
	[HideInInspector]
	public bool playing = true;
	[HideInInspector]
	public bool resetAndPlayOnEnable = true;

	/// <summary>
	/// Restart the tween if this component gets enabled
	/// </summary>
	void OnEnable()
	{
		if (resetAndPlayOnEnable)
		{
			SetValue(); 
			Sample(0); 
			playing = true;
			tweenTimer = -startDelay;
		}
	}

	/// <summary>
	/// Set the value of the tween, sets the start delay,
    ///	if duration = 0 sets the tween immediatly, does an update at the start
	/// </summary>

	void Start() 
	{ 
		SetValue();

		if (reverse != -1) 
		{
			if (startDelay < 0) startDelay = Mathf.Abs(startDelay);
			tweenTimer = -startDelay;
		}
	}

	/// <summary>
	/// Check ignore timescale, if playing, start delay, tweentimer duration 
	/// </summary>
	
	void Update()
	{
		if (!ignoreTimescale && Time.timeScale < 1) return;
		if (!playing) return;

		float deltaTime = Mathf.Min(0.2f, Time.unscaledDeltaTime);
		if (tweenTimer < 0) 
		{ 
			tweenTimer += deltaTime;  
			return; 
		}
		else
		{
			if (duration == 0) 
			{
				if (reverse != -1) ChangeValue(1);
				else ChangeValue(0);
			}
		}

		/// <summary>
		/// reverse the timer if necessary, apply deltatime, Change value, (End, Loop, Reverse) the tween if done
		/// </summary>
		if (tweenTimer < duration)
		{
			if (reverse == -1) tweenTimer += -deltaTime;
			else tweenTimer += deltaTime;

			float factor = (tweenTimer / (duration / 100)) / 100;
			factor = (tweenTimer / (duration / 100)) / 100;
			factor = animationCurve.Evaluate(factor);
			ChangeValue(factor);

			if (tweenTimer >= duration || tweenTimer < 0)
			{
				switch(playStyle)
				{
				case LTweener.Style.Once:
					playing = false;
					tweenTimer = duration;
					SetActive(false);
					break;
				case LTweener.Style.Loop:
					if (reverse == -1) tweenTimer = duration - 0.01f;
					else tweenTimer = 0;
					break;
				case LTweener.Style.PingPong:
					if (reverse == 0) reverse = 1;
					reverse = -reverse;
					if (tweenTimer >= duration) tweenTimer = duration - 0.01f;
					break;
				}
			}
		}
	}

	/// <summary>
	/// Reset the complete tween
	/// </summary>
	public void ResetToBegin()
	{
		playing = true;
		tweenTimer = 0;
		reverse = 0;
		SetStartValue();
	}

	/// <summary>
	/// Stop the tween
	/// </summary>
	public void Stop()
	{
		playing = false;
	}

	/// <summary>
	/// Play the tween
	/// </summary>
	public void Play()
	{
		playing = true;
		SetStartValue();
	}

	/// <summary>
	/// Play the tween from the begin
	/// </summary>
	public void PlayForward()
	{
		playing = true;
		tweenTimer = 0;
		reverse = 0;
		SetStartValue();
	}

	/// <summary>
	/// Play the tween from the end
	/// </summary>
	public void PlayReverse()
	{
		playing = true;
		reverse = -1;
		tweenTimer = duration - 0.01f;
		SetEndValue();
	}

	/// <summary>
	/// Put the Tween in reverse and do the opposite from what it did
	/// </summary>
	public void PlayBack()
	{
		playing =  true;
		if (reverse == -1)
		{
			reverse = 0;
			tweenTimer = 0;
			SetStartValue();
		}
		else
		{
			reverse = -1;
			tweenTimer = duration - 0.01f;
			SetEndValue();
		}
	}

	/// <summary>
	/// Set the tween stuck on a specific time in the tween
	/// </summary>
	public void Sample(float factor)
	{
		playing = false;
		ChangeValue(factor);
	}

	/// <summary>
	/// Set the playstyle
	/// </summary>
	public void SetPlayStyle(LTweener.Style style)
	{
		playStyle = style;
	}

	/// <summary>
	/// Enable or Disable the component
	/// </summary>
	public void SetActive(bool aBool)
	{
		this.enabled = aBool; 
	}

	// Set the value on the start of the tween
	public virtual void SetValue() {}
	
	// Function called to change the value every frame
	public virtual void ChangeValue(float factor) {}
	
	// Check the object it needs to get
	public virtual void ObjectType() {}
	
	// Change the start 
	public virtual void SetStartValue() {}
	
	// Change the end 
	public virtual void SetEndValue() {}
}
