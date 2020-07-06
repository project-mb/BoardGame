using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
	[Range(0.1f, 5)]
	public float offset = 0.5f;

	public Camera cam;
	public GameObject pivot;

	public Transform target;

	public float rotationSpeed = 250f;
	public float zoomSensitivity = 0.75f;

	private Vector3 startPos, endPos;
	private float deltaX, deltaY;
	private float dragDelta, rotDelta;

	private Vector3 camDir;

	private int interval = 10;

	private void Update()
	{
		if (Main.programState != 0)
		{
			if (Input.GetMouseButton(Convert.ToInt32(MouseButton.LeftMouse)))
			{
				cam.transform.RotateAround(pivot.transform.position, Vector3.up, Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime);
				//cam.transform.RotateAround(pivot.transform.position, Vector3.right, -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime);
				//cam.transform.localRotation = Quaternion.Euler(Quaternion.identity.x, Quaternion.identity.y, 0f);

				//if (Time.frameCount % interval == 0)
				//{
				//	startPos = cam.ScreenToWorldPoint(Input.mousePosition);
				//	Debug.Log(Input.mousePosition);
				//}
				//else if (Time.frameCount % interval == 59)
				//{
				//	endPos = cam.ScreenToWorldPoint(Input.mousePosition);
				//	Debug.DrawRay(cam.transform.position, startPos, Color.blue);
				//	Debug.Log(Input.mousePosition);
				//	Debug.Log(startPos + "\n" + endPos);

				//	//deltaX = Math.Abs(endPos.x - startPos.x);
				//	//deltaY = Math.Abs(endPos.y - startPos.y);
				//	//Debug.Log(deltaX + "\n" + deltaY);
				//	//dragDelta = Vector3.Angle(startPos, endPos);
				//	//Debug.LogWarning(dragDelta);
				//}
			}

			if (Input.mouseScrollDelta.y > 0)
			{
				pivot.transform.position = new Vector3(pivot.transform.position.x, pivot.transform.position.y - zoomSensitivity, pivot.transform.position.z);
			}
			if (Input.mouseScrollDelta.y < 0)
			{
				pivot.transform.position = new Vector3(pivot.transform.position.x, pivot.transform.position.y + zoomSensitivity, pivot.transform.position.z);
			}
		}
	}

	public void Centralize()
	{
		float xPos, yPos, zPos;

		List<GameObject> cornerTiles = new List<GameObject>();

		// Get CornerTiles
		foreach (GameObject obj in Main.board.Tiles)
		{
			if (obj.GetComponent<Tile>().SpecificTile.IsCornerTile)
			{
				cornerTiles.Add(obj);
			}
		}

		// Get Camera-xPos (middle of 2nd CornerTile - 1st CornerTile in xDirection)
		xPos = (cornerTiles[3].transform.position.x - cornerTiles[0].transform.position.x) / 2;
		// Get Camera-zPos (middle of 4th CornerTile - 1st CornerTile in zDirection)
		zPos = (cornerTiles[1].transform.position.z - cornerTiles[0].transform.position.z) / 2;
		// Get Camera-yPos acordingly to Board width and height
		yPos = (Board.width > Board.height) ? Board.width : Board.height;

		pivot.transform.position = new Vector3(xPos, offset, zPos);

		cam.transform.localPosition = new Vector3(0f, yPos, 0f);
	}

	public void Focus()
	{
		pivot.transform.position = new Vector3(target.position.x, pivot.transform.position.y, target.position.z);
	}
}