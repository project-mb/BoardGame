using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour
{
	public List<GameObject> TileObjects { get; } = new List<GameObject>();
	public List<TileTemplate> Tiles { get; } = new List<TileTemplate>();

	public bool Generated { get; private set; } = false;

	private float tileWidth, tileHeight, cornerTileWidth, cornerTileHeight;

	public GameObject tile, cornerTile;
	public TextAsset boardTiles;

	public void GenerateNew(ushort width, ushort height) // TODO: Fix generation of rectangular boards
	{
		if (Generated) return;
		int idx = 0;

		GameObject tempTile, tempCorner;

		Vector3 pos;

		tileWidth = tile.transform.localScale.z;
		tileHeight = tile.transform.localScale.x;
		cornerTileWidth = cornerTile.transform.localScale.z;
		cornerTileHeight = cornerTile.transform.localScale.x;

		tempCorner = cornerTile;
		tempTile = tile;

		//Instanitiate BottomRight Corner
		tempCorner.name = "tile" + (idx++).ToString();
		TileObjects.Add(Instantiate(tempCorner, gameObject.transform));

		//Instanitiate Bottom Tiles
		for (ushort bottom = 0; bottom < width; bottom++)
		{
			pos = new Vector3(0f, 0f, (cornerTileWidth / 2) + (tileWidth / 2) + (bottom * tileWidth));
			tempTile.name = "tile" + (idx++).ToString();
			TileObjects.Add(Instantiate(tempTile, pos, Quaternion.identity, gameObject.transform));
		}

		//Instanitiate BottomLeft Corner
		idx++;
		pos = new Vector3(0f, 0f, cornerTileWidth + (width * tileWidth));
		tempCorner.name = "tile" + (idx++).ToString();
		TileObjects.Add(Instantiate(tempCorner, pos, Quaternion.identity, gameObject.transform));

		//Instanitiate Left Tiles
		for (ushort left = 0; left < height; left++)
		{
			pos = new Vector3((cornerTileHeight / 2) + (tileWidth / 2) + (left * tileWidth), 0f, cornerTileWidth + (width * tileWidth));
			tempTile.name = "tile" + (idx++).ToString();
			TileObjects.Add(Instantiate(tempTile, pos, Quaternion.Euler(0f, 90f, 0f), gameObject.transform));
		}

		//Instanitiate TopLeft Corner
		idx++;
		pos = new Vector3(cornerTileHeight + (height * tileWidth), 0f, cornerTileWidth + (width * tileWidth));
		tempCorner.name = "tile" + (idx++).ToString();
		TileObjects.Add(Instantiate(tempCorner, pos, Quaternion.identity, gameObject.transform));

		//Instanitiate Top Tiles
		for (ushort top = 0; top < width; top++)
		{
			pos = new Vector3(cornerTileHeight + (height * tileWidth), 0f, (cornerTileWidth / 2) + (width * tileWidth) - (tileWidth / 2) - (top * tileWidth));
			tempTile.name = "tile" + (idx++).ToString();
			TileObjects.Add(Instantiate(tempTile, pos, Quaternion.Euler(0f, 180f, 0f), gameObject.transform));
		}

		//Instanitiate TopRight Corner
		idx++;
		pos = new Vector3(cornerTileHeight + (height * tileWidth), 0f, 0f);
		tempCorner.name = "tile" + (idx++).ToString();
		TileObjects.Add(Instantiate(tempCorner, pos, Quaternion.identity, gameObject.transform));

		//Instanitiate Right Tiles
		for (ushort right = 0; right < height; right++)
		{
			pos = new Vector3((cornerTileHeight / 2) + (height * tileWidth) - tileWidth / 2 - (right * tileWidth), 0f, 0f);
			tempTile.name = "tile" + (idx++).ToString();
			TileObjects.Add(Instantiate(tempTile, pos, Quaternion.Euler(0f, 270f, 0f), gameObject.transform));
		}

		//Set TileType
		JsonTiles tiles = JsonUtility.FromJson<JsonTiles>(boardTiles.text);

		ushort id = 0;
		foreach (JsonTile current in tiles.tileTypes)
		{
			switch (current.tileType)
			{
				case "StartTile":
					Tiles.Add(new StartTile() { ID = id });
					break;
				case "PlotTile":
					Tiles.Add(new PlotTile() { ID = id });
					break;
				case "GoToJailTile":
					Tiles.Add(new GoToJailTile() { ID = id });
					break;
				case "JailTile":
					Tiles.Add(new JailTile() { ID = id });
					break;
				case "ChanceTile":
					Tiles.Add(new ChanceTile() { ID = id });
					break;
				case "SpecialPlotTile":
					Tiles.Add(new SpecialPlotTile() { ID = id });
					break;
				case "FreeParkingTile":
					Tiles.Add(new FreeParkingTile() { ID = id });
					break;
				case "ChestTile":
					Tiles.Add(new ChestTile() { ID = id });
					break;
				case "TaxTile":
					Tiles.Add(new TaxTile() { ID = id });
					break;
			}

			id++;
		}

		//TODO: Remove debugging code
		Tiles.ForEach(current => current.Action());

		//Set boardGenerated
		Generated = true;
	}

	public void Clear()
	{
		if (!Generated) return;

		//Destroy GameObjects and clear tiles List
		TileObjects.ForEach(current => Destroy(current));
		TileObjects.Clear();

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
