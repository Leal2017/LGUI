using UnityEngine;
using System.Collections;
using UnityEditor;
[CanEditMultipleObjects]
[CustomEditor(typeof(LTweenColor))]
public class LTweenColorEditor : Editor 
{
	/// <summary>
	/// Changes the inspector
	/// </summary>
	public override void OnInspectorGUI ()
	{
		LTweenColor myTarget = (LTweenColor)target;

		myTarget.from = EditorGUILayout.ColorField("From", myTarget.from);
		myTarget.to = EditorGUILayout.ColorField("To", myTarget.to);
		myTarget.includeChildren = EditorGUILayout.Toggle("Include Children", myTarget.includeChildren);

		DrawTweener(myTarget);
	}

	/// <summary>
	/// Tweener values that belong in the inspector
	/// </summary>
	public void DrawTweener(LTweenColor myTarget)
	{
		myTarget.playStyle =(LTweener.Style)EditorGUILayout.EnumPopup("Play Style", myTarget.playStyle);
		myTarget.animationCurve = 			EditorGUILayout.CurveField("Animationcurve", myTarget.animationCurve);
		myTarget.duration = 				EditorGUILayout.FloatField("Duration", myTarget.duration);
		myTarget.startDelay = 				EditorGUILayout.FloatField("Start Delay", myTarget.startDelay);
		myTarget.ignoreTimescale = 			EditorGUILayout.Toggle("Ignore Timescale", myTarget.ignoreTimescale);
	}
}
