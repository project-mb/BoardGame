using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour
{
	public static ushort width, height;

	public List<GameObject> Tiles { get; } = new List<GameObject>();

	public bool Generated { get; private set; } = false;

	private float tileWidth, tileHeight, cornerTileWidth, cornerTileHeight, tileDepth, cornerTileDepth;

	public GameObject tile, cornerTile, boardFill;  // Prefabs

	public void GenerateNew(TextAsset jsonBoardTiles) // TODO: Fix generation of rectangular boards
	{
		if (Generated) return;  // Return if Board already Generated

		JsonTiles jsonTiles = JsonUtility.FromJson<JsonTiles>(jsonBoardTiles.text);
		float boardFillPosX, boardFillPosY, boardFillPosZ;

		if (!CheckJson(jsonTiles, out width, out height)) return;  // Validate json

		List<int> cornerTiles = new List<int>();  // Contains all corner Tiles, used at  

		GameObject tempTile, tempCorner;

		Vector3 pos;

		tileWidth = tile.transform.localScale.z;
		tileHeight = tile.transform.localScale.x;
		tileDepth = tile.transform.localScale.y;
		cornerTileWidth = cornerTile.transform.localScale.z;
		cornerTileHeight = cornerTile.transform.localScale.x;
		cornerTileDepth = cornerTile.transform.localScale.y;

		tempCorner = cornerTile;
		tempTile = tile;

		int idx = 0;  // Tile index

		#region Instanitiate Bottom Right Corner

		pos = new Vector3(0f, 0f, 0f);

		// Used for BoardFill Position
		boardFillPosX = -pos.x;
		boardFillPosZ = -pos.z;

		tempCorner.name = "tile" + idx.ToString();
		cornerTiles.Add(idx);
		Tiles.Add(Instantiate(tempCorner, pos, Quaternion.identity, gameObject.transform));
		idx++;

		#endregion

		#region Instanitiate Bottom Tiles
		for (ushort bottom = 0; bottom < width; bottom++)
		{
			pos = new Vector3(0f, 0f, (cornerTileWidth / 2) + (tileWidth / 2) + (bottom * tileWidth));
			tempTile.name = "tile" + idx.ToString();
			Tiles.Add(Instantiate(tempTile, pos, Quaternion.identity, gameObject.transform));
			idx++;
		}
		#endregion

		#region Instanitiate Bottom Left Corner

		pos = new Vector3(0f, 0f, cornerTileWidth + (width * tileWidth));

		// Used for BoardFill Position
		boardFillPosZ += pos.z;

		cornerTiles.Add(idx);
		tempCorner.name = "tile" + idx.ToString();
		Tiles.Add(Instantiate(tempCorner, pos, Quaternion.identity, gameObject.transform));
		idx++;

		#endregion

		#region Instantiate Left Tiles

		for (ushort left = 0; left < height; left++)
		{
			pos = new Vector3((cornerTileHeight / 2) + (tileWidth / 2) + (left * tileWidth), 0f, cornerTileWidth + (width * tileWidth));
			tempTile.name = "tile" + idx.ToString();
			Tiles.Add(Instantiate(tempTile, pos, Quaternion.Euler(0f, 90f, 0f), gameObject.transform));
			idx++;
		}

		#endregion

		#region Instanitiate TopLeft Corner

		pos = new Vector3(cornerTileHeight + (height * tileWidth), 0f, cornerTileWidth + (width * tileWidth));
		cornerTiles.Add(idx);
		tempCorner.name = "tile" + idx.ToString();
		Tiles.Add(Instantiate(tempCorner, pos, Quaternion.identity, gameObject.transform));
		idx++;

		#endregion

		#region Instanitiate Top Tiles

		for (ushort top = 0; top < width; top++)
		{
			pos = new Vector3(cornerTileHeight + (height * tileWidth), 0f, (cornerTileWidth / 2) + (width * tileWidth) - (tileWidth / 2) - (top * tileWidth));
			tempTile.name = "tile" + idx.ToString();
			Tiles.Add(Instantiate(tempTile, pos, Quaternion.Euler(0f, 180f, 0f), gameObject.transform));
			idx++;
		}

		#endregion

		#region Instanitiate TopRight Corner

		pos = new Vector3(cornerTileHeight + (height * tileWidth), 0f, 0f);

		// Used for BoardFill Position
		boardFillPosX += pos.x;

		cornerTiles.Add(idx);
		tempCorner.name = "tile" + idx.ToString();
		Tiles.Add(Instantiate(tempCorner, pos, Quaternion.identity, gameObject.transform));

		transform.GetChild(1).position = new Vector3(pos.x, transform.GetChild(1).position.y + cornerTileDepth, pos.z);

		idx++;

		#endregion

		#region Instanitiate Right Tiles

		for (ushort right = 0; right < height; right++)
		{
			pos = new Vector3((cornerTileHeight / 2) + (height * tileWidth) - tileWidth / 2 - (right * tileWidth), 0f, 0f);
			tempTile.name = "tile" + idx.ToString();
			Tiles.Add(Instantiate(tempTile, pos, Quaternion.Euler(0f, 270f, 0f), gameObject.transform));
			idx++;
		}

		#endregion

		#region Instantiate BoardFill

		// Can be variable if wanted
		boardFillPosY = 0;
		// Set position to middle of board
		boardFill.transform.position = new Vector3(
			boardFillPosX / 2,
			boardFillPosY - (tileDepth / 10 - boardFill.transform.localScale.y) / 2,
			boardFillPosZ / 2);
		// Set scale to fill board hole
		boardFill.transform.localScale = new Vector3(tileWidth * width, boardFill.transform.localScale.y, tileWidth * height);
		Instantiate(boardFill, gameObject.transform);

		#endregion

		#region Set TileTypes and Positions

		ushort id = 0;
		TilePosition tilePos = TilePosition.Corner;  // Tile position enum
		byte cornersPassed = 0;

		foreach (JsonTile current in jsonTiles.tileTypes)
		{
			// Create specific tile classes according to json
			switch (current.tileType)
			{
				case "StartTile":
					Tiles[id].GetComponent<Tile>().SpecificTile = new StartTile() { ID = id };
					Tiles[id].GetComponentInChildren<TextMeshPro>().text = current.tileType;
					break;
				case "PlotTile":
					Tiles[id].GetComponent<Tile>().SpecificTile = new PlotTile() { ID = id };
					Tiles[id].GetComponentInChildren<TextMeshPro>().text = current.tileType;
					break;
				case "GoToJailTile":
					Tiles[id].GetComponent<Tile>().SpecificTile = new GoToJailTile() { ID = id };
					Tiles[id].GetComponentInChildren<TextMeshPro>().text = current.tileType;
					break;
				case "JailTile":
					Tiles[id].GetComponent<Tile>().SpecificTile = new JailTile() { ID = id };
					Tiles[id].GetComponentInChildren<TextMeshPro>().text = current.tileType;
					break;
				case "ChanceTile":
					Tiles[id].GetComponent<Tile>().SpecificTile = new ChanceTile() { ID = id };
					Tiles[id].GetComponentInChildren<TextMeshPro>().text = current.tileType;
					break;
				case "SpecialPlotTile":
					Tiles[id].GetComponent<Tile>().SpecificTile = new SpecialPlotTile() { ID = id };
					Tiles[id].GetComponentInChildren<TextMeshPro>().text = current.tileType;
					break;
				case "FreeParkingTile":
					Tiles[id].GetComponent<Tile>().SpecificTile = new FreeParkingTile() { ID = id };
					Tiles[id].GetComponentInChildren<TextMeshPro>().text = current.tileType;
					break;
				case "ChestTile":
					Tiles[id].GetComponent<Tile>().SpecificTile = new ChestTile() { ID = id };
					Tiles[id].GetComponentInChildren<TextMeshPro>().text = current.tileType;
					break;
				case "TaxTile":
					Tiles[id].GetComponent<Tile>().SpecificTile = new TaxTile() { ID = id };
					Tiles[id].GetComponentInChildren<TextMeshPro>().text = current.tileType;
					break;
			}

			// Determine cornerTiles and if normal Tiles are at the bottom, left, top or right
			if (cornerTiles.Contains(id))
			{
				Tiles[id].GetComponent<Tile>().SpecificTile.IsCornerTile = true;
				switch (cornersPassed)
				{
					case 0:
						tilePos = TilePosition.Bottom;
						cornersPassed++;
						break;

					case 1:
						tilePos = TilePosition.Left;
						cornersPassed++;
						break;

					case 2:
						tilePos = TilePosition.Top;
						cornersPassed++;
						break;

					case 3:
						tilePos = TilePosition.Right;
						cornersPassed++;
						break;

					default:
						tilePos = TilePosition.Corner;
						cornersPassed++;
						break;
				}
			}

			// Set Position of Tile, if it is not a cornerTile
			if (!Tiles[id].GetComponent<Tile>().SpecificTile.IsCornerTile)
				Tiles[id].GetComponent<Tile>().SpecificTile.Position = tilePos;
			else
				Tiles[id].GetComponent<Tile>().SpecificTile.Position = TilePosition.Corner;

			id++;
		}

		#endregion

		#region Rotate PlayerPositions

		foreach (GameObject tile in Tiles)
		{
			byte i = 0;
			switch (tile.GetComponent<Tile>().SpecificTile.Position)
			{
				// PlayerPositions already rotated corrctly
				case TilePosition.Bottom:
					break;

				// Rotate PlayerPositions by 90 degrees for left tiles
				case TilePosition.Left:
					foreach (Vector3 current in tile.GetComponent<Tile>().SpecificTile.PlayerPositions)
					{
						tile.GetComponent<Tile>().SpecificTile.PlayerPositions[i] = Quaternion.Euler(0f, 90f, 0f) * current;
						i++;
					}
					break;

				// Rotate PlayerPositions by 180 degrees for top tiles
				case TilePosition.Top:
					foreach (Vector3 current in tile.GetComponent<Tile>().SpecificTile.PlayerPositions)
					{
						tile.GetComponent<Tile>().SpecificTile.PlayerPositions[i] = Quaternion.Euler(0f, 180f, 0f) * current;
						i++;
					}
					break;

				// Rotate PlayerPositions by -90 degrees for right tiles
				case TilePosition.Right:
					foreach (Vector3 current in tile.GetComponent<Tile>().SpecificTile.PlayerPositions)
					{
						tile.GetComponent<Tile>().SpecificTile.PlayerPositions[i] = Quaternion.Euler(0f, -90f, 0f) * current;
						i++;
					}
					break;
			}
		}

		#endregion

		// Set Generated flag
		Generated = true;
	}

	private bool CheckJson(JsonTiles tiles, out ushort width, out ushort height)
	{
		width = 9; height = 9;
		return true;
	}

	public void Clear()
	{
		if (!Generated) return;

		// Destroy GameObjects and clear tiles List
		Tiles.ForEach(current => Destroy(current));
		Tiles.Clear();

		// Unset boardGenerated
		Generated = false;
	}

	// TODO: Generate from Load-File (? .json)
}

[Serializable]
public class JsonTiles
{
	public List<JsonTile> tileTypes;
}

[Serializable]
public class JsonTile
{
	public string tileType;
}
