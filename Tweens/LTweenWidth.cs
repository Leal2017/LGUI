using UnityEngine;
using UnityEngine.UI;

//width tween
public class LTweenWidth : LTweener 
{
	public float from = 100;
	public float startWidth = 0;
	public float to = 0;
	public float endWidth = 0;
	
	[HideInInspector]
	public string type;
	
	[HideInInspector]
	public float value;

	/// <summary>
	/// Sets the value that will be changed, when the from hasn't been set it will change to the starting value
	/// </summary>
	public override void SetValue()
	{
		value = this.GetComponent<RectTransform>().sizeDelta.x;

		startWidth = from;
		endWidth = to;
	}

	/// <summary>
	/// Changes value every frame
	/// </summary>
	public override void ChangeValue(float factor)
	{
		value = Mathf.Lerp(startWidth, endWidth, factor);

		ObjectType();
	}

	/// <summary>
	/// Changes the real value
	/// </summary>
	public override void ObjectType()
	{
		Vector2 tempMovement = this.GetComponent<RectTransform>().sizeDelta;
		tempMovement.x = value;
		this.GetComponent<RectTransform>().sizeDelta = tempMovement;
	}

	/// <summary>
	/// Sets starting value to current value
	/// </summary>
	public override void SetStartValue()
	{
		startWidth = value;
		endWidth = to;
	}

	/// <summary>
	/// Sets end value to current value
	/// </summary>
	public override void SetEndValue()
	{
		startWidth = from;
		endWidth = value;
	}
}

