using UnityEngine;
using UnityEngine.UI;

//transform tween
public class LTweenTransform : LTweener 
{
	public GameObject from = null;
	private RectTransform fromRect = null;
	private Transform fromTrans = null;
	public GameObject to = null;
	private RectTransform toRect = null;
	private Transform toTrans = null;

	private Vector3 fromPosition,fromRotation,fromScale,toPosition,toRotation,toScale;
	private Vector2 fromSize,toSize;

	[HideInInspector]
	public string type;
	
	[HideInInspector]
	public RectTransform valueRect = null;
	[HideInInspector]
	public Transform valueTrans = null;
	
	/// <summary>
	/// Sets the value that will be changed, when the from hasn't been set it will change to the starting value
	/// </summary>
	public override void SetValue()
	{
		if (this.GetComponent<RectTransform>() != null)	valueRect = this.GetComponent<RectTransform>();
		else valueTrans = this.transform;

		if (from == null) SetFromValues(this.gameObject);
		else SetFromValues(from);

		if (to == null) SetToValues(this.gameObject);
		else SetToValues(to);
	}

	/// <summary>
	/// Changes value every frame
	/// </summary>
	public override void ChangeValue(float factor)
	{
		Vector3 tempPosition = Vector3.Lerp (fromPosition, toPosition, factor);
		Vector3 tempRotation = Vector3.Lerp (fromRotation, toRotation, factor);
		Vector3 tempScale = Vector3.Lerp (fromScale, toScale, factor);

		if (valueRect != null)
		{
			if (fromRect != null || toRect != null)
			{
				Vector2 tempSize = Vector2.Lerp (fromSize, toSize, factor);
				valueRect.sizeDelta = tempSize;
			}
			valueRect.position = tempPosition;
			valueRect.eulerAngles = tempRotation;
			valueRect.localScale = tempScale;
		}
		else
		{
			valueTrans.position = tempPosition;
			valueTrans.eulerAngles = tempRotation;
			valueTrans.localScale = tempScale;
		}
		
		ObjectType();
	}
	
	/// <summary>
	/// Changes the real value
	/// </summary>
	public override void ObjectType()
	{
		if (valueRect != null)
		{
			Vector3 tempPosition = valueRect.position;
			Vector2 tempSize = valueRect.sizeDelta;
			Vector3 tempRotation = valueRect.eulerAngles;
			Vector3 tempScale = valueRect.localScale;

			this.GetComponent<RectTransform>().position = tempPosition;
			this.GetComponent<RectTransform>().sizeDelta = tempSize;
			this.GetComponent<RectTransform>().eulerAngles = tempRotation;
			this.GetComponent<RectTransform>().localScale = tempScale;
		}
		else
		{
			Vector3 tempPosition = valueTrans.position;
			Vector3 tempRotation = valueTrans.eulerAngles;
			Vector3 tempScale = valueTrans.localScale;

			this.transform.position = tempPosition;
			this.transform.eulerAngles = tempRotation;
			this.transform.localScale = tempScale;
		}
	}
	
	/// <summary>
	/// Sets starting value to current value
	/// </summary>
	public override void SetStartValue()
	{
		from = this.gameObject;
	}
	
	/// <summary>
	/// Sets end value to current value
	/// </summary>
	public override void SetEndValue()
	{
		to = this.gameObject;
	}

	/// <summary>
	/// Sets all the from values
	/// </summary>
	public void SetFromValues(GameObject obj)
	{
		if (obj.GetComponent<RectTransform>() != null)
		{
			fromRect = obj.GetComponent<RectTransform>();
			fromPosition = fromRect.position;
			fromSize = fromRect.sizeDelta;
			fromRotation = fromRect.eulerAngles;
			fromScale = fromRect.localScale;
		}
		else
		{
			fromTrans = obj.transform;
			fromPosition = fromTrans.position;
			fromSize = new Vector2(100, 100);
			fromRotation = fromTrans.eulerAngles;
			fromScale = fromTrans.localScale;
		}
	}

	/// <summary>
	/// Sets all the to values
	/// </summary>
	public void SetToValues(GameObject obj)
	{
		if (obj.GetComponent<RectTransform>() != null)
		{
			toRect = obj.GetComponent<RectTransform>();
			toPosition = toRect.position;
			toSize = toRect.sizeDelta;
			toRotation = toRect.eulerAngles;
			toScale = toRect.localScale;
		}
		else
		{
			toTrans = obj.transform;
			toPosition = toTrans.position;
			toSize = new Vector2(100, 100);
			toRotation = toTrans.eulerAngles;
			toScale = toTrans.localScale;
		}
	}
}
