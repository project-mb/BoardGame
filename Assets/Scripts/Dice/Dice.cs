using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Dice : MonoBehaviour
{
	public Transform diceSpawn;

	public Camera cam;

	public GameObject dice;

	public float offset;
	public float delay;
	public float force;

	private Vector3 pos;
	private Vector3 ranTorqueRot;

	private Quaternion ranRot;

	private GameObject temp;
	private GameObject die1;
	private GameObject die2;

	private float ranX, ranY, ranZ;

	private bool isValid;

	private void Update()
	{
		if (Input.GetMouseButtonUp(0) && Main.programState > 0 && !EventSystem.current.IsPointerOverGameObject() && die2 == null)
		{
			pos = diceSpawn.position;

			ranX = UnityEngine.Random.Range(0, 360);
			ranY = UnityEngine.Random.Range(0, 360);
			ranZ = UnityEngine.Random.Range(0, 360);
			ranRot = new Quaternion(ranX, ranY, ranZ, Quaternion.identity.w);
			ranTorqueRot = new Vector3(ranX, ranY, ranZ);
			die1 = Instantiate(dice, pos + Vector3.left * 0.1f, ranRot);
			die1.transform.rotation = Quaternion.Euler(Quaternion.identity.x, -15f, Quaternion.identity.z);
			die1.GetComponent<Rigidbody>().AddForce(diceSpawn.forward * force * Time.deltaTime * 66f);
			die1.GetComponent<Rigidbody>().AddTorque(ranTorqueRot * UnityEngine.Random.Range(-360, 360) * 66f);

			ranX = UnityEngine.Random.Range(0, 360);
			ranY = UnityEngine.Random.Range(0, 360);
			ranZ = UnityEngine.Random.Range(0, 360);
			ranRot = new Quaternion(ranX, ranY, ranZ, Quaternion.identity.w);
			ranTorqueRot = new Vector3(ranX, ranY, ranZ);
			die2 = Instantiate(dice, pos + Vector3.right * 0.1f, ranRot);
			die2.transform.rotation = Quaternion.Euler(Quaternion.identity.x, 15f, Quaternion.identity.z);
			die2.GetComponent<Rigidbody>().AddForce(diceSpawn.forward * force * Time.deltaTime * 66f);
			die2.GetComponent<Rigidbody>().AddTorque(ranTorqueRot * UnityEngine.Random.Range(-360, 360) * 66f);

			RollDice();
		}

		if(die1 != null)
		{
			// TODO: Get Up Face
			Debug.DrawRay(die1.transform.position, die1.transform.up);
			Debug.DrawRay(die1.transform.position, die1.transform.right);
			Debug.DrawRay(die1.transform.position, die1.transform.forward);
			Debug.DrawRay(die1.transform.position, -die1.transform.up);
			Debug.DrawRay(die1.transform.position, -die1.transform.right);
			Debug.DrawRay(die1.transform.position, -die1.transform.forward);

			Debug.DrawRay(die2.transform.position, die2.transform.up);
			Debug.DrawRay(die2.transform.position, die2.transform.right);
			Debug.DrawRay(die2.transform.position, die2.transform.forward);
			Debug.DrawRay(die2.transform.position, -die2.transform.up);
			Debug.DrawRay(die2.transform.position, -die2.transform.right);
			Debug.DrawRay(die2.transform.position, -die2.transform.forward);

			if (die1.GetComponent<Rigidbody>().IsSleeping() && die2.GetComponent<Rigidbody>().IsSleeping())
			{
				isValid = true;
				Destroy(die1, delay);
				Destroy(die2, delay);
				isValid = false;
			}
			else
			{
				Destroy(die1, delay + 5);
				Destroy(die2, delay + 5);
				isValid = false;
			}
		}
	}

	float a, b;
	bool c;

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
