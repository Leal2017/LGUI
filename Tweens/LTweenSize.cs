using UnityEngine;
using UnityEngine.UI;

//size tween
public class LTweenSize : LTweener 
{
	public Vector2 from = new Vector2(100, 100);
	public Vector2 startSize = new Vector2(0, 0);
	public Vector2 to = new Vector2(0, 0);
	public Vector2 endSize = new Vector2(0, 0);
	
	[HideInInspector]
	public string type;
	
	[HideInInspector]
	public Vector2 value;
	
	/// <summary>
	/// Sets the value that will be changed, when the from hasn't been set it will change to the starting value
	/// </summary>
	
	public override void SetValue()
	{
		value = this.GetComponent<RectTransform>().sizeDelta;

		startSize = from;
		endSize = to;
	}
	
	/// <summary>
	/// Changes value every frame
	/// </summary>
	
	public override void ChangeValue(float factor)
	{
		value = Vector2.Lerp(startSize, endSize, factor);
		
		ObjectType();
	}
	
	/// <summary>
	/// Changes the real value
	/// </summary>
	
	public override void ObjectType()
	{
		Vector2 tempMovement = this.GetComponent<RectTransform>().sizeDelta;
		tempMovement = value;
		this.GetComponent<RectTransform>().sizeDelta = tempMovement;
	}
	
	/// <summary>
	/// Sets starting value to current value
	/// </summary>
	
	public override void SetStartValue()
	{
		startSize = value;
		endSize = to;
	}
	
	/// <summary>
	/// Sets end value to current value
	/// </summary>
	
	public override void SetEndValue()
	{
		startSize = from;
		endSize = value;
	}
}
