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
	private GameObject testPlayerInstance;
	private List<GameObject> testPlayerInstances = new List<GameObject>();
	private Player testPlayer;
	private List<Player> testPlayers = new List<Player>();
	public int playerPosition;
	public int playerPosition2;

	void Start()
	{
		programState = 1;

		board = GameObject.Find("Board").GetComponent<Board>();
		board.GenerateNew(jsonBoardTiles);

		// Focus cam onto boardFill
		cam = GameObject.Find("YPivot").GetComponent<CameraController>();
		cam.Centralize();

		testPlayerInstance = Instantiate(playerPrefab, gameObject.transform.position, Quaternion.identity);
		testPlayer = testPlayerInstance.GetComponent<Player>();
		testPlayer.Init(board.Tiles[0]);

		for (byte i = 0; i < 7; i++)
		{
			testPlayerInstances.Add(Instantiate(playerPrefab, gameObject.transform.position, Quaternion.identity));
			testPlayers.Add(testPlayerInstances[i].GetComponent<Player>());
			testPlayers[i].Init(board.Tiles[0]);
		}
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

				if (Input.GetKeyDown(KeyCode.RightArrow))
				{
					playerPosition++;
					if (playerPosition >= board.Tiles.Count)
					{
						playerPosition = 0;
					}
				}
				else if (Input.GetKeyDown(KeyCode.LeftArrow))
				{
					playerPosition--;
					if (playerPosition < 0)
					{
						playerPosition = board.Tiles.Count - 1;
					}
				}

				if (Input.GetKeyDown(KeyCode.D))
				{
					playerPosition2++;
					if (playerPosition2 >= board.Tiles.Count)
					{
						playerPosition2 = 0;
					}
				}
				else if (Input.GetKeyDown(KeyCode.A))
				{
					playerPosition2--;
					if (playerPosition2 < 0)
					{
						playerPosition2 = board.Tiles.Count - 1;
					}
				}


				testPlayer.MoveTo(board.Tiles[playerPosition]);
				foreach (Player current in testPlayers)
				{
					current.MoveTo(board.Tiles[playerPosition2]);
				}

				break;
			default:
				Debug.LogError("Invalid programState");  // TODO: Handle error
				break;
		}
	}
}
