using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
	public RawImage background;
	public Button menu;
	public Button resume;
	public Button mainMenu;
	public Button exit;

	public static bool menuOpen;

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

	public void Menu()
	{
		if (!menuOpen)
		{
			menuOpen = true;

			resume.enabled = true;
			mainMenu.enabled = true;
			exit.enabled = true;

			background.color = new Color(0, 0, 0, 0.5f);

			resume.GetComponentInChildren<Text>().color = new Color(
				resume.GetComponentInChildren<Text>().color.r,
				resume.GetComponentInChildren<Text>().color.g,
				resume.GetComponentInChildren<Text>().color.b,
				1f);
			mainMenu.GetComponentInChildren<Text>().color = new Color(
				mainMenu.GetComponentInChildren<Text>().color.r,
				mainMenu.GetComponentInChildren<Text>().color.g,
				mainMenu.GetComponentInChildren<Text>().color.b,
				1f);
			exit.GetComponentInChildren<Text>().color = new Color(
				exit.GetComponentInChildren<Text>().color.r,
				exit.GetComponentInChildren<Text>().color.g,
				exit.GetComponentInChildren<Text>().color.b,
				1f);

			resume.image.color = new Color(resume.image.color.r, resume.image.color.g, resume.image.color.b, 1f);
			mainMenu.image.color = new Color(mainMenu.image.color.r, mainMenu.image.color.g, mainMenu.image.color.b, 1f);
			exit.image.color = new Color(exit.image.color.r, exit.image.color.g, exit.image.color.b, 1f);
		}
		else
		{
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
	}
}