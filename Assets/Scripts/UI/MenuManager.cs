using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;

	public Menu[] menus;

	private void Awake()
	{
		instance = this;
	}
	private void Start()
	{
		for (int i = 0; i < menus.Length; i++)
		{
			if (menus[i].open)
			{
				OpenMenu(menus[i]);
				return;
			}
		}
	}

	public void OpenMenu(string menuName)
	{
		for (int i = 0; i < menus.Length; i++)
		{
			if(menus[i].menuName == menuName)
			{
				OpenMenu(menus[i]);
			}
			else if(menus[i].open)
			{
				CloseMenu(menus[i]);
			}
		}
	}


	public void OpenMenu(Menu menu)
	{
		for (int i = 0; i < menus.Length; i++)
		{
			CloseMenu(menus[i]);
		}
		menu.Open();
	}


	public void CloseMenu(Menu menu)
	{
		menu.Close();
	}
}
