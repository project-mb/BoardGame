using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Camera cam;
	public GameObject pivot;

	public void Centralize()
	{
		float xPos, yPos, zPos;

		List<GameObject> cornerTiles = new List<GameObject>();

		foreach (GameObject obj in Main.board.Tiles)
		{
			if (obj.GetComponent<Tile>().SpecificTile.IsCornerTile)
			{
				Debug.LogWarning(obj);
				cornerTiles.Add(obj);
			}
			else
			{
				Debug.Log(obj);
			}
		}

		xPos = (cornerTiles[0].transform.position.x + cornerTiles[1].transform.position.x) / 2;
		zPos = (cornerTiles[0].transform.position.z + cornerTiles[3].transform.position.x) / 2;
		yPos = 0;/*(myGame. > myGame.boardHeight) ? myGame.boardWidth : myGame.boardHeight;*/

		Debug.Log(xPos + "/" + yPos + "/" + zPos);

		cam.transform.position = new Vector3(xPos, yPos, zPos);
	}
}