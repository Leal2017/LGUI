using UnityEngine;
using UnityEngine.UI;

// rotation tween
public class LTweenRotation : LTweener 
{
	public Vector3 from = new Vector3(0, 0, 0);
	public Vector3 startRotation = new Vector3(0, 0, 0);
	public Vector3 to = new Vector3(0, 0, 0);
	public Vector3 endRotation = new Vector3(0, 0, 0);
	
	[HideInInspector]
	public string type;
	
	[HideInInspector]
	public Vector3 value;

	/// <summary>
	/// Sets the value that will be changed, when the from hasn't been set it will change to the starting value
	/// </summary>

	public override void SetValue()
	{
		value = this.GetComponent<RectTransform>().eulerAngles;

		startRotation = from;
		endRotation = to;
	}

	/// <summary>
	/// Changes value every frame
	/// </summary>
	
	public override void ChangeValue(float factor)
	{
		value = Vector3.Lerp(startRotation, endRotation, factor);

		ObjectType();
	}

	/// <summary>
	/// Changes the real value
	/// </summary>
	
	public override void ObjectType()
	{
		Vector3 tempMovement = transform.eulerAngles;
		tempMovement = value;
		transform.eulerAngles = tempMovement;
	}

	/// <summary>
	/// Sets starting value to current value
	/// </summary>
	
	public override void SetStartValue()
	{
		startRotation = value;
		endRotation = to;
	}

	/// <summary>
	/// Sets end value to current value
	/// </summary>
	
	public override void SetEndValue()
	{
		startRotation = from;
		endRotation = value;
	}
}
