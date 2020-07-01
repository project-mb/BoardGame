using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	public TileTemplate TileType { get; set; }
}

public abstract class TileTemplate
{
	public ushort ID { get; set; }
	public float[,] PlayerPositions { get; } = new float[,]
	{
		{ }
	};

	public abstract void Action();
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
