using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LLocalize : MonoBehaviour {
	
	public string Key;
	[HideInInspector]
	public string filePath;

	void Awake()
	{
		LoadKey();
	}

	/// <summary>
	///初始化
	/// </summary>
	void Start(){}

	/// <summary>
	/// 加载文本从本地化文件夹和图片资源
	/// </summary>
	public void LoadKey()
	{
		if (this.GetComponent<Text>() != null) 
		{
			this.GetComponent<Text>().text = LLocalization.Get(this.gameObject, Key);
		}
		else if (this.GetComponent<Image>() != null)
		{
			filePath = LLocalization.Get (this.gameObject, "FilePath");
			Sprite tempSprite = Resources.Load<Sprite>(filePath+LLocalization.Get(this.gameObject, Key));
			this.GetComponent<Image>().sprite = tempSprite;
		}
		else if (this.GetComponent<RawImage>() != null) 
		{
			filePath = LLocalization.Get (this.gameObject, "FilePath");
			Texture tempTexture = Resources.Load<Texture>(filePath+LLocalization.Get(this.gameObject, Key));
			this.GetComponent<RawImage>().texture = tempTexture;
		}
	}
}
