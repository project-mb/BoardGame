using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Dice : MonoBehaviour
{
	public Camera cam;
	public GameObject dice;
	public float offset;
	public float force;

	private Vector3 pos;
	private GameObject temp;

	float a, b;
	bool c;

	private void Update()
	{
		if (Input.GetMouseButton(0) && Main.programState > 0 && !EventSystem.current.IsPointerOverGameObject())
		{
			pos = cam.transform.position + cam.transform.forward * offset;

			temp = Instantiate(dice, pos, Quaternion.identity);
			temp.GetComponent<Rigidbody>().AddForce(cam.transform.forward * force * Time.deltaTime * 66f);

			RollDice();
		}
	}

	public void RollDice()
	{
		c = Roll(out a, out b);
		Debug.Log(c + ": " + a + "/" + b);
	}

	public bool Roll(out float face1, out float face2)
	{
		face1 = Mathf.Round(UnityEngine.Random.Range(1f, 6f));
		face2 = Mathf.Round(UnityEngine.Random.Range(1f, 6f));



		if (face1 == face2) return true;
		else return false;
	}
}
