using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	public ITile TileType { get; set; }
}

public interface ITile
{
	void Action();
}

public class StartTile : ITile
{
	public void Action()
	{
		Debug.Log("I am a StartTile");
	}
}

public class PlotTile : ITile
{
	public void Action()
	{
		Debug.Log("I am a PlotTile");
	}
}
