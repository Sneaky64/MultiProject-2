using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonHover : MonoBehaviour
{
	public float scaleFactor;
	public float scaleTime;
	float fontSize_;

	TMP_Text text;

	private void Start()
	{
		text = GetComponentInChildren<TMP_Text>();
		fontSize_ = text.fontSize;
	}
	public void ScalingMouseOver()
	{
		LeanTween.value(fontSize_, fontSize_ * scaleFactor, scaleTime).setEaseOutExpo().setOnUpdate(TweenText);
	}
	public void ScalingMouseOut()
	{
		LeanTween.value(text.fontSize, fontSize_, scaleTime).setEaseOutExpo().setOnUpdate(TweenText);
	}
	public void TweenText(float size)
	{
		text.fontSize = size;
	}
}
