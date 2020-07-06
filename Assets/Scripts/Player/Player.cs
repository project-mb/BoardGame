using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject CurrentTile { get; private set; }
	public ushort Position { get; private set; } = 0;
	public byte OffsetPosition { get; private set; } = 0;
	private Vector3 target = new Vector3();

	public void Init(GameObject tile)
	{
		CurrentTile = tile;
	}

	public void MoveTo(GameObject tile)
	{
		if (tile == CurrentTile) return;

		TileTemplate specTile = tile.GetComponent<Tile>().SpecificTile;

		// Get the next free position in target tile
		byte nextFreePos = 0;
		for (byte i = 0; i < specTile.OccupiedPositions.Length; i++)
		{
			if (!specTile.OccupiedPositions[i])
			{
				nextFreePos = i;
				break;
			}
		}

		Vector3 targetPos = new Vector3();

		transform.rotation = tile.transform.rotation;

		targetPos = new Vector3(tile.transform.position.x + specTile.PlayerPositions[nextFreePos].z, 0f, tile.transform.position.z + specTile.PlayerPositions[nextFreePos].x);


		CurrentTile.GetComponent<Tile>().SpecificTile.OccupiedPositions[OffsetPosition] = false;  // Could be an error

		specTile.OccupiedPositions[nextFreePos] = true;
		OffsetPosition = nextFreePos;

		target = targetPos;

		Position = specTile.ID;
		CurrentTile = tile;
	}

	private void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, target, 10.0f * Time.deltaTime);
	}
}
