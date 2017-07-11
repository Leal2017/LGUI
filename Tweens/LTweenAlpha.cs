using UnityEngine;
using UnityEngine.UI;

public class LTweenAlpha : LTweener 
{
	public float from = 1;
	public float startAlpha = 0;
	public float to = 0;
	public float endAlpha = 0;
	public bool includeChildren = true;
	
	[HideInInspector]
	public string type;
	
	[HideInInspector]
	public float value;

	
	
	public override void SetValue()
	{
		if (this.GetComponent<Image>() != null) 		{ value = this.GetComponent<Image>().color.a; 	type = "Image"; }
		else if (this.GetComponent<RawImage>() != null) { value = this.GetComponent<RawImage>().color.a;  type = "RawImage"; }
		else if (this.GetComponent<Text>() != null) 	{ value = this.GetComponent<Text>().color.a;  	type = "Text"; }
		else value = 1;

		startAlpha = from;
		endAlpha = to;
	}


	//改变值，每一帧都执行
	public override void ChangeValue(float factor)
	{
		value = Mathf.Lerp(startAlpha, endAlpha, factor);

		ObjectType();
	}

	
	//改变真实值
	public override void ObjectType()
	{
		Color tempColor = Color.white;
		switch(type)
		{
		case "Text":
			tempColor = this.GetComponent<Text>().color;
			tempColor.a = value;
			this.GetComponent<Text>().color = tempColor;
			break;
		case "Image":
			tempColor = this.GetComponent<Image>().color;
			tempColor.a = value;
			this.GetComponent<Image>().color = tempColor;
			break;
		case "RawImage":
			tempColor = this.GetComponent<RawImage>().color;
			tempColor.a = value;
			this.GetComponent<RawImage>().color = tempColor;
			break;
		}
		if (includeChildren)
		{
			foreach(Image img in this.GetComponentsInChildren<Image>())	
			{
				tempColor = img.color;
				tempColor.a = value;
				img.color = tempColor;
			}
			foreach(RawImage rimg in this.GetComponentsInChildren<RawImage>()) 
			{
				tempColor = rimg.color;
				tempColor.a = value;
				rimg.color = tempColor;
			}
			foreach(Text txt in this.GetComponentsInChildren<Text>()) 
			{
				tempColor = txt.color;
				tempColor.a = value;
				txt.color = tempColor;
			}
		}
	}

	/// <summary>
	///初始值设置为当前值
	/// </summary>
	public override void SetStartValue()
	{
		startAlpha = value;
		endAlpha = to;
	}

	/// <summary>
	/// 最终值设置为当前值
	/// </summary>
	public override void SetEndValue()
	{
		startAlpha = from;
		endAlpha = value;
	}
}
