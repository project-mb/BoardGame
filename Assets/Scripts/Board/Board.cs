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

	private float tileWidth, tileHeight, cornerTileWidth, cornerTileHeight;

	public GameObject tile, cornerTile;  // Prefabs

	public void GenerateNew(TextAsset jsonBoardTiles) // TODO: Fix generation of rectangular boards
	{
		if (Generated) return;  // Return if Board already Generated

		JsonTiles jsonTiles = JsonUtility.FromJson<JsonTiles>(jsonBoardTiles.text);

		if (!CheckJson(jsonTiles, out width, out height)) return;  // Validate json

		List<int> cornerTiles = new List<int>();  // Contains all corner Tiles, used at  

		GameObject tempTile, tempCorner;

		Vector3 pos;

		tileWidth = tile.transform.localScale.z;
		tileHeight = tile.transform.localScale.x;
		cornerTileWidth = cornerTile.transform.localScale.z;
		cornerTileHeight = cornerTile.transform.localScale.x;

		tempCorner = cornerTile;
		tempTile = tile;

		int idx = 0;  // Tile index

		#region Instanitiate Bottom Right Corner

		tempCorner.name = "tile" + idx.ToString();
		cornerTiles.Add(idx);
		Tiles.Add(Instantiate(tempCorner, gameObject.transform));
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
		cornerTiles.Add(idx);
		tempCorner.name = "tile" + idx.ToString();
		Tiles.Add(Instantiate(tempCorner, pos, Quaternion.identity, gameObject.transform));
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

		#region Set TileTypes

		ushort id = 0;
		foreach (JsonTile current in jsonTiles.tileTypes)
		{
			switch (current.tileType)
			{
				case "StartTile":
					Tiles[id].GetComponent<Tile>().SpecificTile = new StartTile() { ID = id };
					break;
				case "PlotTile":
					Tiles[id].GetComponent<Tile>().SpecificTile = new PlotTile() { ID = id };
					break;
				case "GoToJailTile":
					Tiles[id].GetComponent<Tile>().SpecificTile = new GoToJailTile() { ID = id };
					break;
				case "JailTile":
					Tiles[id].GetComponent<Tile>().SpecificTile = new JailTile() { ID = id };
					break;
				case "ChanceTile":
					Tiles[id].GetComponent<Tile>().SpecificTile = new ChanceTile() { ID = id };
					break;
				case "SpecialPlotTile":
					Tiles[id].GetComponent<Tile>().SpecificTile = new SpecialPlotTile() { ID = id };
					break;
				case "FreeParkingTile":
					Tiles[id].GetComponent<Tile>().SpecificTile = new FreeParkingTile() { ID = id };
					break;
				case "ChestTile":
					Tiles[id].GetComponent<Tile>().SpecificTile = new ChestTile() { ID = id };
					break;
				case "TaxTile":
					Tiles[id].GetComponent<Tile>().SpecificTile = new TaxTile() { ID = id };
					break;
			}

			if (cornerTiles.Contains(id))
			{
				Tiles[id].GetComponent<Tile>().SpecificTile.IsCornerTile = true;
				Debug.Log(Tiles[id].GetComponent<Tile>().SpecificTile.ID);
			}

			id++;
		}

		foreach(GameObject current in Tiles)
		{
			Debug.Log(current.GetComponent<Tile>().SpecificTile.ID);
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

		//Destroy GameObjects and clear tiles List
		Tiles.ForEach(current => Destroy(current));
		Tiles.Clear();

		//Unset boardGenerated
		Generated = false;
	}

	//TODO: Generate from Load-File (? .json)
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
