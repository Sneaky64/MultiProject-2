using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ButtonHover : MonoBehaviour
{
	//public float scaleFactor;
	//public float scaleTime;
	//float fontSize_;

	//TMP_Text text;

	public GameObject lightOff;
	public GameObject lightOn;

	private void Start()
	{
		//text = GetComponentInChildren<TMP_Text>();
		//fontSize_ = text.fontSize;
	}
	public void MouseOver()
	{
		lightOff.SetActive(false);
		lightOn.SetActive(true);
		//LeanTween.value(fontSize_, fontSize_ * scaleFactor, scaleTime).setEaseOutQuart().setOnUpdate(TweenText);
	}
	public void MouseOut()
	{
		lightOff.SetActive(true);
		lightOn.SetActive(false);
		//LeanTween.value(text.fontSize, fontSize_, scaleTime).setEaseOutQuart().setOnUpdate(TweenText);
	}
	public void TweenText(float size)
	{
		text.fontSize = size;
	}
}
