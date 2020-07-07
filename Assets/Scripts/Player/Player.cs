using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject CurrentTile { get; private set; }  // Tile, which the player is currently standing on
	public ushort Position { get; private set; } = 0;  // Contains the ID of the current tile
	public byte OffsetPosition { get; private set; } = 0;  // Index of offset position of current tile
	private Vector3 target = new Vector3();  // Position to move to

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

		Vector3 offset = specTile.PlayerPositions[nextFreePos];  // Set offset position to join queue on tile

		targetPos = new Vector3(tile.transform.position.x + offset.x, 0f, tile.transform.position.z + offset.z);

		CurrentTile.GetComponent<Tile>().SpecificTile.OccupiedPositions[OffsetPosition] = false;  // Mark last position as free

		specTile.OccupiedPositions[nextFreePos] = true;  // Mark current position as occupied
		OffsetPosition = nextFreePos;

		target = targetPos;

		Position = specTile.ID;
		CurrentTile = tile;
	}

	private void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, target, 5.0f * Time.deltaTime);
	}
}
