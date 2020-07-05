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

		// Get CornerTiles
		foreach (GameObject obj in Main.board.Tiles)
		{
			if (obj.GetComponent<Tile>().SpecificTile.IsCornerTile)
			{
				cornerTiles.Add(obj);
			}
		}

		Debug.LogWarning(cornerTiles[1].name);
		Debug.LogWarning(cornerTiles[0].name);
		Debug.LogWarning(cornerTiles[3].name);
		Debug.LogWarning(cornerTiles[0].name);

		// Get Camera-xPos (middle of 2nd CornerTile - 1st CornerTile in xDirection)
		xPos = (cornerTiles[1].transform.position.x - cornerTiles[0].transform.position.x) / 2;
		// Get Camera-zPos (middle of 4th CornerTile - 1st CornerTile in zDirection)
		zPos = (cornerTiles[3].transform.position.z - cornerTiles[0].transform.position.z) / 2;
		// Get Camera-yPos acordingly to Board width and height
		yPos = (Board.width > Board.height) ? Board.width : Board.height;

		Debug.Log(xPos + "/" + yPos + "/" + zPos);

		cam.transform.position = new Vector3(xPos, yPos, zPos);
	}
}