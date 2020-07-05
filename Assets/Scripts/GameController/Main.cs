using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Main : MonoBehaviour
{
	public static Board board;

	public static ushort programState = 0; // TODO: Change to property after testing
	public static byte numPlayers = 8;
	public TextAsset jsonBoardTiles;


	// TODO: Remove debug code
	public GameObject playerPrefab;
	private GameObject playerInstance;
	private Player player;
	public int playerPosition;
	public float x, z;

	void Start()
	{
		board = GameObject.Find("Board").GetComponent<Board>();
		board.GenerateNew(jsonBoardTiles);
		playerInstance = Instantiate(playerPrefab, gameObject.transform.position, Quaternion.identity);
		player = playerInstance.GetComponent<Player>();
	}

	void Update()
	{

		/*
		* ProgramStates:
		* 0: Do nothing
		* 1: Player active
		*/

		switch (programState)
		{
			case 0:
				break;
			case 1:

				if (Input.GetKey(KeyCode.RightArrow))
				{
					playerPosition++;
					if(playerPosition >= board.Tiles.Count)
					{
						playerPosition = 0;
					}
				}
				else if (Input.GetKey(KeyCode.LeftArrow))
				{
					playerPosition--;
					if (playerPosition < 0)
					{
						playerPosition = board.Tiles.Count - 1;
					}
				}
				player.MoveTo(board.Tiles[playerPosition]);
				break;
			default:
				Debug.LogError("Invalid programState");  // TODO: Handle error
				break;
		}
	}
}
