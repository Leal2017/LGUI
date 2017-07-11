using UnityEngine;
using UnityEngine.UI;

//fov tween
public class LTweenFOV : LTweener 
{
	public float from = 50;
	public float startFOV = 0;
	public float to = 0;
	public float endFOV = 0;
	
	[HideInInspector]
	public string type;
	
	[HideInInspector]
	public float value;

	[HideInInspector]
	public bool hasCamera = false;

	/// <summary>
	/// Sets the value that will be changed, when the from hasn't been set it will change to the starting value
	/// </summary>
	
	public override void SetValue()
	{
		if (this.GetComponent<Camera>() != null)
		{
			hasCamera = true;
			value = this.GetComponent<Camera>().fieldOfView;

			startFOV = from;
			endFOV = to;
		}
		else Debug.LogError(this.name+" is missing a Camera, Tween requires Camera component to do it's job.");
	}

	/// <summary>
	/// Changes value every frame
	/// </summary>
	
	public override void ChangeValue(float factor)
	{
		if (hasCamera)
		{
			value = Mathf.Lerp(startFOV, endFOV, factor);

			ObjectType();
		}
	}

	/// <summary>
	/// Changes the real value
	/// </summary>
	
	public override void ObjectType()
	{
		this.GetComponent<Camera>().fieldOfView = value;
	}

	/// <summary>
	/// Sets starting value to current value
	/// </summary>
	
	public override void SetStartValue()
	{
		startFOV = value;
		endFOV = to;
	}

	/// <summary>
	/// Sets end value to current value
	/// </summary>
	
	public override void SetEndValue()
	{
		startFOV = from;
		endFOV = value;
	}
}

