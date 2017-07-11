
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;

//本地化,设置语言
public static class LLocalization 
{
	// text file 文件夹路径
	public static string filePath = "Localization/";
	// the localization information
	static Dictionary<string, string> dictionary = new Dictionary<string, string>();
	// 是否处于加载情况
	public static bool localizationLoaded = false;
	// 使用了那种语言，English,China，and so on
	static string currentLanguage = "Example";
	
	
	public static string language
	{
		get { return currentLanguage; }
		set
		{
			currentLanguage = value;
			// 加载新的文件
			localizationLoaded = false;
			Load ();
			// 重新加载所有的objects LLocalize
			foreach(GameObject obj in localizeGameObjects)
			{
				if (obj != null) obj.GetComponent<LLocalize>().LoadKey();
			}
		}
	}
	
	// 加载语言被更改的物体
	static List<GameObject> localizeGameObjects = new List<GameObject>();
	
	/// <summary>
	///LoadLocalization并添加到字典
	/// </summary>
	public static void LoadLocalization()
	{
		if (!localizationLoaded)
		{
			dictionary.Clear();
			// split text file on ENTER
			string[] separator = new string[] {"\n"};
			// Load text file
			TextAsset textAsset = (TextAsset)Resources.Load(filePath+currentLanguage, typeof(TextAsset));
			// Check if textfile is succesfully loaded
			
			if (textAsset == null)
			{
				Debug.LogWarning("[Localization] Localization not found: "+filePath+currentLanguage);
				return;
			}
			List<string> tempString = textAsset.text.Split(separator, System.StringSplitOptions.None).ToList();
			// 以 ' = '作为分割符
			separator = new string[] {" = "};
			// 检查key是否存在的集合
			List<string> doubleKeyCheck = new List<string>();
		
			foreach (string str in tempString)
			{
				string[] tString = str.Split(separator, System.StringSplitOptions.None).ToArray();
				if (tString.Length > 2)
				{
					foreach (string tempStr in tString)
					{
						if (tempStr != tString[0] && tempStr != tString[1]) 
						{
							tString[1] = tString[1] +" = "+ tempStr;
						}
					}
				}
			
				if (tString.Length > 1)	
				{
					tString[1] = tString[1].Replace("\\n", System.Environment.NewLine);
					
					if (!doubleKeyCheck.Contains(tString[0])) 
					{
						doubleKeyCheck.Add(tString[0]);
						dictionary.Add(tString[0], tString[1]);
					}
					else Debug.LogWarning("[Localization] Identical keys ("+tString[0]+") found while loading localization");
				}
			}
			//localization已经加载了
			localizationLoaded = true;
		}
	}

	/// <summary>
	/// 加载本地化
	/// </summary>
	public static void Load()
	{
		LoadLocalization();
	}

	/// <summary>
	/// 获取key值
	/// </summary>
	public static string Get(string key) 
	{ 
		
		if (!localizationLoaded) Debug.LogWarning("[Localization] Localization hasn't been loaded yet.");//Load ();
		if (localizationLoaded)
		{
			//return value
			if (dictionary.ContainsKey(key))
			{
				return dictionary[key];
			}
			Debug.LogWarning("[Localization] Could not find the key "+key);
		}
		return key;
	}
	
	
	/// <summary>
	///获取key值和存储该object在list集合里
	/// </summary>
	public static string Get(GameObject currentGameObject, string key)
	{
		bool objCheck = true;
		foreach (GameObject obj in localizeGameObjects) if (obj == currentGameObject) objCheck = false;
		if (objCheck) localizeGameObjects.Add(currentGameObject);

		if (!localizationLoaded) Debug.LogWarning("[Localization] Localization hasn't been loaded yet.");//Load ();
		if (localizationLoaded)
		{
			if (dictionary.ContainsKey(key))
			{			
				return dictionary[key];
			}
			Debug.LogWarning("[Localization] Could not find the key "+key);
		}
		return key;
	}

	/// <summary>
	/// 设置key
	/// </summary>
	public static void Set(string key, string value)
	{
		
		if (!localizationLoaded) Load ();
		
		if (dictionary.ContainsKey(key))//是否字典中已经存在该值了
		{
			
			Dictionary<string, string> tempDic = new Dictionary<string, string>(dictionary);//临时dic
			List<string> tempList = new List<string>(dictionary.Keys);
			dictionary.Clear();//清空
			foreach(string str in tempList)
			{
				if (str == key) dictionary.Add(key, value);
				else dictionary.Add(str, tempDic[str]);
			}
			return;
		}
		
		dictionary.Add(key, value);//往字典添加值
	}
}
