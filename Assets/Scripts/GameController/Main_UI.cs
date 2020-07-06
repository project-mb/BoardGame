using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_UI : MonoBehaviour
{
	public RawImage background;
	public Button menu;
	public Button resume;
	public Button mainMenu;
	public Button exit;

	public static bool menuOpen;

	private ushort previousProgramState;

	// Start is called before the first frame update
	void Awake()
	{
		menu.enabled = true;
		menuOpen = false;

		resume.enabled = false;
		mainMenu.enabled = false;
		exit.enabled = false;

		background.color = new Color(0, 0, 0, 0f);

		resume.GetComponentInChildren<Text>().color = new Color(
			resume.GetComponentInChildren<Text>().color.r,
			resume.GetComponentInChildren<Text>().color.g,
			resume.GetComponentInChildren<Text>().color.b,
			0f);
		mainMenu.GetComponentInChildren<Text>().color = new Color(
			mainMenu.GetComponentInChildren<Text>().color.r,
			mainMenu.GetComponentInChildren<Text>().color.g,
			mainMenu.GetComponentInChildren<Text>().color.b,
			0f);
		exit.GetComponentInChildren<Text>().color = new Color(
			exit.GetComponentInChildren<Text>().color.r,
			exit.GetComponentInChildren<Text>().color.g,
			exit.GetComponentInChildren<Text>().color.b,
			0f);

		resume.image.color = new Color(resume.image.color.r, resume.image.color.g, resume.image.color.b, 0f);
		mainMenu.image.color = new Color(mainMenu.image.color.r, mainMenu.image.color.g, mainMenu.image.color.b, 0f);
		exit.image.color = new Color(exit.image.color.r, exit.image.color.g, exit.image.color.b, 0f);
	}

	private void Update()
	{
		if(Input.GetKeyUp(KeyCode.Escape))
		{
			if (!menuOpen)
			{
				OpenCloseMenu(true, 0.5f, 1f);
			}
			else OpenCloseMenu(false, 0f, 0f);
		}
	}

	public void Menu()
	{
		if (!menuOpen)
		{
			OpenCloseMenu(true, 0.5f, 1f);
		}
		else
		{
			OpenCloseMenu(false, 0f, 0f);
		}
	}

	public void Resume()
	{
		OpenCloseMenu(false, 0f, 0f);
	}

	public void MainMenu()
	{
		SceneManager.LoadScene("Main Menu");
	}

	public void Exit()
	{
		Application.Quit();
	}

	private void OpenCloseMenu(bool open, float bgAlpha, float uiAlpha)
	{
		if (open)
		{
			menuOpen = true;
			previousProgramState = Main.programState;

			Main.programState = 0;

			resume.enabled = true;
			mainMenu.enabled = true;
			exit.enabled = true;
		}
		else
		{
			menuOpen = false;

			Main.programState = previousProgramState;

			resume.enabled = false;
			mainMenu.enabled = false;
			exit.enabled = false;
		}


		background.color = new Color(0, 0, 0, bgAlpha);

		resume.GetComponentInChildren<Text>().color = new Color(
			resume.GetComponentInChildren<Text>().color.r,
			resume.GetComponentInChildren<Text>().color.g,
			resume.GetComponentInChildren<Text>().color.b,
			uiAlpha);
		mainMenu.GetComponentInChildren<Text>().color = new Color(
			mainMenu.GetComponentInChildren<Text>().color.r,
			mainMenu.GetComponentInChildren<Text>().color.g,
			mainMenu.GetComponentInChildren<Text>().color.b,
			uiAlpha);
		exit.GetComponentInChildren<Text>().color = new Color(
			exit.GetComponentInChildren<Text>().color.r,
			exit.GetComponentInChildren<Text>().color.g,
			exit.GetComponentInChildren<Text>().color.b,
			uiAlpha);

		resume.image.color = new Color(resume.image.color.r, resume.image.color.g, resume.image.color.b, uiAlpha);
		mainMenu.image.color = new Color(mainMenu.image.color.r, mainMenu.image.color.g, mainMenu.image.color.b, uiAlpha);
		exit.image.color = new Color(exit.image.color.r, exit.image.color.g, exit.image.color.b, uiAlpha);
	}
}