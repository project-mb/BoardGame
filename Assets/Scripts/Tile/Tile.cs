using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tile : MonoBehaviour
{
	public TileTemplate SpecificTile { get; set; }
}

public abstract class TileTemplate
{
	public ushort ID { get; set; }  // Identifier of tile
	public TilePosition Position { get; set; }  // Indicates if tiles is at the bottom, left, top or right
	public Vector3[] PlayerPositions { get; set; }  // Contains offset positions of players on tile
	public Vector3[] PlayerPositionsCorner { get; private set; }  // Contains position on corner tiles
	public bool[] OccupiedPositions { get; set; } = { false, false, false, false, false, false, false, false };  // True, if the the position is already occupied by a player

	// Set if tile is CornerTile
	public bool IsCornerTile
	{
		get { return _IsCornerTile; }
		set
		{
			_IsCornerTile = value;

			// Modify PlayerPositions if tile is a corner tile
			if (value)
				PlayerPositions = SetCornerPlayerPositions();
		}
	}

	public abstract void Action();  // Contains action, which will be executed on player entering the tile

	private bool _IsCornerTile;  // Private backing field for IsCornerTile

	public TileTemplate()
	{
		// Set offset positions for normal tiles
		PlayerPositions = new Vector3[]
		{
			new Vector3(-0.3f, 0f, 0.25f),
			new Vector3(-0.3f, 0f, 0.083333f),
			new Vector3(-0.3f, 0f, -0.083333f),
			new Vector3(-0.3f, 0f, -0.25f),
			new Vector3(0f, 0f, 0.25f),
			new Vector3(0f, 0f, 0.083333f),
			new Vector3(0f, 0f, -0.083333f),
			new Vector3(0f, 0f, -0.25f)
		};
	}

	// Returns offset positions for corner tiles
	private Vector3[] SetCornerPlayerPositions()
	{
		return new Vector3[]
		{
			new Vector3(0.25f, 0f, 0.35f),
			new Vector3(0.25f, 0f, 0.116667f),
			new Vector3(0.25f, 0f, -0.11667f),
			new Vector3(0.25f, 0f, -0.35f),
			new Vector3(-0.25f, 0f, 0.35f),
			new Vector3(-0.25f, 0f, 0.116667f),
			new Vector3(-0.25f, 0f, -0.11667f),
			new Vector3(-0.25f, 0f, -0.35f)
		};
	}
}

public class StartTile : TileTemplate
{
	public override void Action()
	{
		Debug.Log(ID + "I am a StartTile");
	}
}

public class PlotTile : TileTemplate
{
	public override void Action()
	{
		Debug.Log(ID + "I am a PlotTile");
	}
}

public class GoToJailTile : TileTemplate
{
	public override void Action()
	{
		Debug.Log(ID + "I am a GoToJailTile");
	}
}

public class JailTile : TileTemplate
{
	public override void Action()
	{
		Debug.Log(ID + "I am a JailTile");
	}
}

public class ChanceTile : TileTemplate
{
	public override void Action()
	{
		Debug.Log(ID + "I am a ChanceTile");
	}
}

public class SpecialPlotTile : TileTemplate
{
	public override void Action()
	{
		Debug.Log(ID + "I am a SpecialPlotTile");
	}
}

public class FreeParkingTile : TileTemplate
{
	public override void Action()
	{
		Debug.Log(ID + "I am a FreeParkingTile");
	}
}

public class ChestTile : TileTemplate
{
	public override void Action()
	{
		Debug.Log(ID + "I am a ChestTile");
	}
}

public class TaxTile : TileTemplate
{
	public override void Action()
	{
		Debug.Log(ID + "I am a TaxTile");
	}
}

public enum TilePosition
{
	Bottom, Left, Right, Top, Corner
}
