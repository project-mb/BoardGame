using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	public TileTemplate SpecificTile { get; set; }
}

public abstract class TileTemplate
{
	public ushort ID { get; set; }  // Identifier of Tile
	public bool IsCornerTile { get; set; } = false;  // Set if Tile is CornerTile
	public TilePosition Position { get; set; }
	public Vector3[] PlayerPositions { get; private set; }  // Contains position on normal tiles
	public Vector3[] PlayerPositionsCorner { get; private set; }  // Contains position on corner tiles
	public bool[] OccupiedPositions { get; set; } = { false, false, false, false, false, false, false, false };  // True, if the the position is already occupied by a player

	public abstract void Action();  // Contains Action, which will be executed on Player entering the Tile

	public TileTemplate()
	{
		PlayerPositions = new Vector3[]
		{
			new Vector3(-0.35f, 0f, 0.2f),
			new Vector3(-0.35f, 0f, 0.066667f),
			new Vector3(-0.35f, 0f, -0.06667f),
			new Vector3(-0.35f, 0f, -0.2f),
			new Vector3(-0.1f, 0f, 0.2f),
			new Vector3(-0.1f, 0f, 0.066667f),
			new Vector3(-0.1f, 0f, -0.06667f),
			new Vector3(-0.1f, 0f,-0.2f)
		};

		PlayerPositionsCorner = new Vector3[]
		{

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
