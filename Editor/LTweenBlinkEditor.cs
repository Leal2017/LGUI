using UnityEngine;
using System.Collections;
using UnityEditor;
[CanEditMultipleObjects]
[CustomEditor(typeof(LTweenBlink))]
public class LTweenBlinkEditor : Editor 
{
	/// <summary>
	/// Changes the inspector
	/// </summary>
	public override void OnInspectorGUI ()
	{
		LTweenBlink myTarget = (LTweenBlink)target;
		
		myTarget.from = EditorGUILayout.FloatField("On Time", myTarget.from);
		myTarget.to = EditorGUILayout.FloatField("Off Time", myTarget.to);
		myTarget.count = EditorGUILayout.IntField("Count", myTarget.count);
		myTarget.deactivateWhenDone = EditorGUILayout.Toggle("Deactivate On Done", myTarget.deactivateWhenDone);
		myTarget.includeChildren = EditorGUILayout.Toggle("Include Children", myTarget.includeChildren);
		
		DrawTweener(myTarget);
	}

	/// <summary>
	/// Tweener values that belong in the inspector
	/// </summary>
	public void DrawTweener(LTweenBlink myTarget)
	{
		myTarget.startDelay = 				EditorGUILayout.FloatField("Start Delay", myTarget.startDelay);
		myTarget.ignoreTimescale = 			EditorGUILayout.Toggle("Ignore Timescale", myTarget.ignoreTimescale);
	}
}
