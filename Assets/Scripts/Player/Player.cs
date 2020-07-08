using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public GameObject CurrentTile { get; private set; }  // Tile, which the player is currently standing on
	public ushort Position { get; private set; }  // Contains the ID of the current tile
	public byte OffsetPosition { get; private set; }  // Index of offset position of current tile
	private Vector3 target = new Vector3();  // Position to move to

	public void Init(GameObject tile)
	{
		CurrentTile = tile;
		TileTemplate specTile = CurrentTile.GetComponent<Tile>().SpecificTile;
		Position = specTile.ID;

		byte nextFreePos = specTile.OccupyNextFreePosition();  // Mark current position as occupied and store position
		OffsetPosition = nextFreePos;

		Vector3 offset = specTile.PlayerPositions[nextFreePos];  // Set offset position to join queue on tile

		target = new Vector3(tile.transform.position.x + offset.x, 0.07f, tile.transform.position.z + offset.z);
	}

	public void MoveTo(GameObject tile)
	{
		if (tile == CurrentTile) return;

		TileTemplate specTile = tile.GetComponent<Tile>().SpecificTile;
		TileTemplate currentSpecTile = CurrentTile.GetComponent<Tile>().SpecificTile;
		Position = specTile.ID;

		currentSpecTile.FreePosition(OffsetPosition);  // Mark last position as free
		byte nextFreePos = specTile.OccupyNextFreePosition();  // Mark current position as occupied and store position
		OffsetPosition = nextFreePos;

		transform.rotation = tile.transform.rotation;

		Vector3 offset = specTile.PlayerPositions[nextFreePos];  // Set offset position to join queue on tile

		target = new Vector3(tile.transform.position.x + offset.x, 0.07f, tile.transform.position.z + offset.z);

		CurrentTile = tile;
	}

	private void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, target, 10.0f * Time.deltaTime);
	}
}
