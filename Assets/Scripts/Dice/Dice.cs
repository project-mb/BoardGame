using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UIElements;

public class Dice : MonoBehaviour
{
	public GameObject dice;
	public float force;
	public float offset;

	Vector3 pos;
	GameObject temp;

	float a, b;
	bool c;

	private void Update()
	{
		if(Input.GetMouseButton(0) && Main.programState > 0)
		{
			pos = Camera.main.transform.position + Camera.main.transform.forward * offset;

			temp = Instantiate(dice, pos, Quaternion.identity);
			temp.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * force);

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
