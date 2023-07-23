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

	bool hovering = false;
	bool pressed = false;

	private void Start()
	{
		//text = GetComponentInChildren<TMP_Text>();
		//fontSize_ = text.fontSize;
	}
    private void Update()
    {
        if(hovering || pressed)
        {
			lightOff.SetActive(false);
			lightOn.SetActive(true);
			return;
		}
        else
		{
			lightOff.SetActive(true);
			lightOn.SetActive(false);
		}
	}
    public void SetHover(bool hovering_)
	{
		hovering = hovering_;
		//LeanTween.value(fontSize_, fontSize_ * scaleFactor, scaleTime).setEaseOutQuart().setOnUpdate(TweenText);
	}
    public void SetPressed(bool pressed_)
    {
		pressed = pressed_;
    }
    public void TweenText(float size)
	{
		//text.fontSize = size;
	}
}
