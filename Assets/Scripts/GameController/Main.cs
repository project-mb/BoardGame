using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Main : MonoBehaviour
{
	public Board board;

	public ushort programState = 0;
	public ushort boardWidth, boardHeight;

	void Start()
	{
		board.GenerateNew(boardWidth, boardHeight);
	}

	void Update()
	{
		switch (programState)
		{
			case 0:
				break;
			default:
				Debug.LogError("Invalid programState");  // TODO: Handle error
				break;
		}
	}
}
