using UnityEngine;
using UnityEngine.UI;

//scale tween
public class LTweenScale : LTweener 
{
	public Vector3 from = new Vector3(1, 1, 1);
	public Vector3 startScale = new Vector3(1, 1, 1);
	public Vector3 to = new Vector3(1, 1, 1);
	public Vector3 endScale = new Vector3(1, 1, 1);
	
	[HideInInspector]
	public string type;
	
	[HideInInspector]
	public Vector3 value;

	/// <summary>
	/// Sets the value that will be changed, when the from hasn't been set it will change to the starting value
	/// </summary>
	public override void SetValue()
	{
		value = this.GetComponent<RectTransform>().localScale;

		startScale = from;
		endScale = to;
	}

	/// <summary>
	/// Changes value every frame
	/// </summary>
	public override void ChangeValue(float factor)
	{
		value = Vector3.Lerp(startScale, endScale, factor);

		ObjectType();
	}

	/// <summary>
	/// Changes the real value
	/// </summary>
	public override void ObjectType()
	{
		Vector3 tempMovement = transform.localScale;
		tempMovement = value;
		transform.localScale = tempMovement;
	}

	/// <summary>
	/// Sets starting value to current value
	/// </summary>
	public override void SetStartValue()
	{
		startScale = value;
		endScale = to;
	}

	/// <summary>
	/// Sets end value to current value
	/// </summary>
	public override void SetEndValue()
	{
		startScale = from;
		endScale = value;
	}
}
