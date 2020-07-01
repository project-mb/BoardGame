using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Main : MonoBehaviour
{
	public Board board;

	public ushort programState = 0; // TODO: Change to property after testing
	public ushort boardWidth, boardHeight;
	public byte numPlayers = 8;

	void Start()
	{
		board.GenerateNew(boardWidth, boardHeight);
	}

	void Update()
	{

		/*
		* ProgramStates:
		* 0: Do nothing
		* 1: Rolling dice
		*/

		switch (programState)
		{
			case 0:
				break;
			case 1:
				Dice dice = GameObject.Find("Dice").GetComponent<Dice>();
				if(Input.GetKey(KeyCode.Return))
				{
					int face1, face2;
					bool doubles = dice.Roll(out face1, out face2);
					Debug.Log($"{face1} + {face2} = {face1 + face2}" + (doubles ? ", DOUBLES" : ""));
					Thread.Sleep(100);
				}



				break;
			default:
				Debug.LogError("Invalid programState");  // TODO: Handle error
				break;
		}
	}
}
