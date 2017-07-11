using UnityEngine;
using UnityEngine.UI;

//height tween 
public class LTweenHeight : LTweener 
{
	public float from = 100;
	public float startHeight = 0;
	public float to = 0;
	public float endHeight = 0;
	
	[HideInInspector]
	public string type;
	
	[HideInInspector]
	public float value;

	/// <summary>
	/// Sets the value that will be changed
	/// </summary>
	
	public override void SetValue()
	{
		value = this.GetComponent<RectTransform>().sizeDelta.y;

		startHeight = from;
		endHeight = to;
	}

	/// <summary>
	/// Changes value every frame
	/// </summary>
	
	public override void ChangeValue(float factor)
	{
		value = Mathf.Lerp(startHeight, endHeight, factor);

		ObjectType();
	}

	/// <summary>
	/// Changes the real value
	/// </summary>
	
	public override void ObjectType()
	{
		Vector2 tempMovement = this.GetComponent<RectTransform>().sizeDelta;
		tempMovement.y = value;
		this.GetComponent<RectTransform>().sizeDelta = tempMovement;
	}

	/// <summary>
	/// Sets starting value to current value
	/// </summary>
	
	public override void SetStartValue()
	{
		startHeight = value;
		endHeight = to;
	}

	/// <summary>
	/// Sets end value to current value
	/// </summary>
	
	public override void SetEndValue()
	{
		startHeight = from;
		endHeight = value;
	}
}
