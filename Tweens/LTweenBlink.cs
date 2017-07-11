using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LTweenBlink : LTweener 
{
	public float from = 0.5f;
	private float onTime = 0;
	public float to = 0.75f;
	private float offTime = 0;
	public int count = -1;
	private int blinkTimes = 0;
	public bool deactivateWhenDone = false;
	public bool includeChildren = true;
	
	[HideInInspector]
	public string type;
	
	[HideInInspector]
	public bool value = false;
	
	/// <summary>
	/// Sets the value that will be changed, when the from hasn't been set it will change to the starting value
	/// </summary>	
	public override void SetValue()
	{
		if (this.GetComponent<Image>() != null)			{ value = this.GetComponent<Image>().enabled; type = "Image"; }
		else if (this.GetComponent<RawImage>() != null)	{ value = this.GetComponent<RawImage>().enabled; type = "RawImage"; }
		else if (this.GetComponent<Text>() != null)		{ value = this.GetComponent<Text>().enabled; type = "Text"; }

		onTime = from;
		offTime = to;
		duration = from + to;
		blinkTimes = count;
	}
	
	/// <summary>
	/// Changes value every frame
	/// </summary>
	
	public override void ChangeValue(float factor)
	{
		if (value)
		{
			onTime -= Time.unscaledDeltaTime;
			if (onTime <= 0)
			{
				value = false;
				if (blinkTimes == 0)
				{
					value = true;
					if (deactivateWhenDone) value = false;
					Stop();
				}
				else if (blinkTimes > 0) blinkTimes--;
				onTime = from;
				tweenTimer = 0;
				ObjectType();
				return;
			}
		}
		else
		{
			offTime -= Time.unscaledDeltaTime;
			if (offTime <= 0)
			{
				value = true;
				if (blinkTimes == 0) 
				{
					if (deactivateWhenDone) value = false;
					Stop();
				}
				offTime = to;
				tweenTimer = 0;
				ObjectType();
				return;
			}
		}

	}
	
	/// <summary>
	/// Changes the real value
	/// </summary>
	
	public override void ObjectType()
	{
		if (includeChildren)
		{
			foreach(Image img in this.GetComponentsInChildren<Image>())	img.enabled = value;
			foreach(RawImage rimg in this.GetComponentsInChildren<RawImage>()) rimg.enabled = value;
			foreach(Text txt in this.GetComponentsInChildren<Text>()) txt.enabled = value;
		}
		switch(type)
		{
		case "Text":
			this.GetComponent<Text>().enabled = value;
			break;
		case "Image":
			this.GetComponent<Image>().enabled = value;
			break;
		case "RawImage":
			this.GetComponent<RawImage>().enabled = value;
			break;
		}
	}
	
	/// <summary>
	/// Sets starting value to current value
	/// </summary>
	
	public override void SetStartValue()
	{
		from = 0;
	}
	
	/// <summary>
	/// Sets end value to current value
	/// </summary>
	
	public override void SetEndValue()
	{
		to = 0;
	}
}

