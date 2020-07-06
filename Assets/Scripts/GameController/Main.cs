using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Main : MonoBehaviour
{
	public static Board board;
	public static CameraController cam;

	public static ushort programState = 1; // TODO: Change to property after testing
	public static byte numPlayers = 8;
	public TextAsset jsonBoardTiles;


	// TODO: Remove debug code
	public GameObject playerPrefab;
	private GameObject playerInstance;
	private GameObject playerInstance2;
	private Player player;
	private Player player2;
	public int playerPosition;
	public int playerPosition2;

	void Start()
	{
		board = GameObject.Find("Board").GetComponent<Board>();
		board.GenerateNew(jsonBoardTiles);

		cam = GameObject.Find("YPivot").GetComponent<CameraController>();
		cam.Centralize();

		playerInstance = Instantiate(playerPrefab, gameObject.transform.position, Quaternion.identity);
		player = playerInstance.GetComponent<Player>();
		player.Init(board.Tiles[0]);

		playerInstance2 = Instantiate(playerPrefab, gameObject.transform.position, Quaternion.identity);
		player2 = playerInstance2.GetComponent<Player>();
		player2.Init(board.Tiles[0]);
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
					if (playerPosition >= board.Tiles.Count)
					{
						playerPosition = 0;
					}
					Thread.Sleep(100);
				}
				else if (Input.GetKey(KeyCode.LeftArrow))
				{
					playerPosition--;
					if (playerPosition < 0)
					{
						playerPosition = board.Tiles.Count - 1;
					}
					Thread.Sleep(100);
				}
				
				if (Input.GetKey(KeyCode.D))
				{
					playerPosition2++;
					if (playerPosition2 >= board.Tiles.Count)
					{
						playerPosition2 = 0;
					}
					Thread.Sleep(100);
				}
				else if (Input.GetKey(KeyCode.A))
				{
					playerPosition2--;
					if (playerPosition2 < 0)
					{
						playerPosition2 = board.Tiles.Count - 1;
					}
					Thread.Sleep(100);
				}

				player.MoveTo(board.Tiles[playerPosition]);
				player2.MoveTo(board.Tiles[playerPosition2]);

				break;
			default:
				Debug.LogError("Invalid programState");  // TODO: Handle error
				break;
		}
	}
}
