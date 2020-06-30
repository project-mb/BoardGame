using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour
{
	public GameObject tile;
	public GameObject cornerTile;

	public List<GameObject> tiles;

	private float tileWidth, tileHeight, cornerTileWidth, cornerTileHeight;

	private GameObject tempTile;
	private GameObject tempCorner;

	public void GenerateNew(ushort width, ushort height) // TODO: Fix generation of rectangular boards
	{
		int idx = 1;

		Vector3 pos;

		tileWidth = tile.transform.localScale.z;
		tileHeight = tile.transform.localScale.x;
		cornerTileWidth = cornerTile.transform.localScale.z;
		cornerTileHeight = cornerTile.transform.localScale.x;

		tempCorner = cornerTile;
		tempTile = tile;

		//Instanitiate BottomRight Corner
		tempCorner.name = "Start";
		tiles.Add(Instantiate(tempCorner, gameObject.transform));

		//Instanitiate Bottom Tiles
		for (ushort bottom = 0; bottom < width; bottom++)
		{
			pos = new Vector3(0f, 0f, (cornerTileWidth / 2) + (tileWidth / 2) + (bottom * tileWidth));
			tempTile.name = (idx++).ToString();
			tiles.Add(Instantiate(tempTile, pos, Quaternion.identity, gameObject.transform));
		}

		//Instanitiate BottomLeft Corner
		idx++;
		pos = new Vector3(0f, 0f, cornerTileWidth + (width * tileWidth));
		tempCorner.name = "Prision";
		tiles.Add(Instantiate(tempCorner, pos, Quaternion.identity, gameObject.transform));

		//Instanitiate Left Tiles
		for (ushort left = 0; left < height; left++)
		{
			pos = new Vector3((cornerTileHeight / 2) + (tileWidth / 2) + (left * tileWidth), 0f, cornerTileWidth + (width * tileWidth));
			tempTile.name = (idx++).ToString();
			tiles.Add(Instantiate(tempTile, pos, Quaternion.Euler(0f, 90f, 0f), gameObject.transform));
		}

		//Instanitiate TopLeft Corner
		idx++;
		pos = new Vector3(cornerTileHeight + (height * tileWidth), 0f, cornerTileWidth + (width * tileWidth));
		tempCorner.name = "Park";
		tiles.Add(Instantiate(tempCorner, pos, Quaternion.identity, gameObject.transform));

		//Instanitiate Top Tiles
		for (ushort top = 0; top < width; top++)
		{
			pos = new Vector3(cornerTileHeight + (height * tileWidth), 0f, (cornerTileWidth / 2) + (width * tileWidth) - (tileWidth / 2) - (top * tileWidth));
			tempTile.name = (idx++).ToString();
			tiles.Add(Instantiate(tempTile, pos, Quaternion.Euler(0f, 180f, 0f), gameObject.transform));
		}

		//Instanitiate TopRight Corner
		idx++;
		pos = new Vector3(cornerTileHeight + (width * tileWidth), 0f, 0f);
		tempCorner.name = "ReRoll";
		tiles.Add(Instantiate(tempCorner, pos, Quaternion.identity, gameObject.transform));

		//Instanitiate Right Tiles
		for (ushort right = 0; right < height; right++)
		{
			pos = new Vector3((cornerTileHeight / 2) + (height * tileWidth) - tileWidth / 2 - (right * tileWidth), 0f, 0f);
			tempTile.name = (idx++).ToString();
			tiles.Add(Instantiate(tempTile, pos, Quaternion.Euler(0f, 270f, 0f), gameObject.transform));
		}
	}

	//TODO: Generate from Load-File (? .json)
}