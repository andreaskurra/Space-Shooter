using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour 
{
	public GameObject[] gos = null;

	public bool showScrollLabel = false;

	private int index = -1;

	private void Awake()
	{
		Show(true);
	}

	private void OnGUI()
	{
		Color c = GUI.color;
		GUI.color = Color.red;

		float btnSize = Screen.height / 4;
		if(GUI.Button(new Rect(0,0,btnSize,btnSize), "Next"))
		{
			Show(true);
		}
		if(GUI.Button(new Rect(0,btnSize,btnSize,btnSize), "Prev"))
		{
			Show(false);
		}

		if(showScrollLabel)
		{
			GUI.Label(new Rect(btnSize*0.5f,btnSize*2.5f, Screen.width, btnSize), "Scroll down to view the whole image.");
		}

		GUI.color = c;
	}

	private void Show(bool isNext)
	{
		if(isNext)
		{
			++index;
			if(index >= gos.Length)
			{
				index = 0;
			}
		}
		else
		{
			--index;
			if(index < 0)
			{
				index = gos.Length - 1;
			}
		}

		if(index < 0 || index >= gos.Length)
		{
			return;
		}

		for(int i = 0; i < gos.Length; ++i)
		{
			gos[i].SetActive(i == index);
		}
	}
}
