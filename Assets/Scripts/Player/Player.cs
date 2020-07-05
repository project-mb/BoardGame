using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject CurrentTile { get; private set; }
	public ushort Position { get; private set; }

	public int offset;

	public void MoveTo(GameObject tile)
	{
		TileTemplate specTile = tile.GetComponent<Tile>().SpecificTile;
		transform.rotation = tile.transform.rotation;
		Vector3 targetPos = new Vector3(tile.transform.position.x + specTile.PlayerPositions[offset, 1], 0f, tile.transform.position.z + specTile.PlayerPositions[offset, 0]);
		transform.position = targetPos;
		Position = specTile.ID;
		CurrentTile = tile;
	}
}
