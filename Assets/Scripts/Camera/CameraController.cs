using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
	public Camera cam;
	public GameObject yPivot, xPivot;

	public Transform target;

	[Header("Offsets")]
	[Range(0.1f, 5)]
	public float yPivotOffset = 0.5f;

	[Header("Range")]
	public float minCamOffset = 5f;
	public float maxCamOffset = 15f;
	public float minCamXRot = 10f;
	public float maxCamXRot = 85f;

	[Header("Settings")]
	public float rotationSpeed = 500f;
	public float zoomSensitivity = 1f;
	public float focusSpeed = 1f;

	private Ray ray;
	private RaycastHit hitInfo;

	private Vector3 currentFocus;
	private Vector3 targetFocus;

	private float saveRot;

	private GameObject focus;

	private bool canRotX;
	//private bool canRotY;		// If wanted
	private bool isHit;

	private void Start()
	{
		targetFocus = yPivot.transform.position;
		xPivot.transform.Rotate(Vector3.right, (maxCamXRot - 90));
	}

	private void Update()
	{
		if (Main.programState > 0)
		{
			if (Input.GetMouseButton(1))
			{
				// Set the yPivot rotation with the Mouse-X Axis and the rotationSpeed
				yPivot.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime);
				// Set the xPivot rotation with the Mouse-Y Axis and the rotationSpeed
				xPivot.transform.Rotate(Vector3.right, -Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime);

				// TODO: Add rot constraints
			}

			if (Input.mouseScrollDelta.y > 0)
			{
				// Check if Cam to close to yPivot and move cam towards yPivot
				if (Vector3.Distance(cam.transform.position, yPivot.transform.position) < minCamOffset - 1) return;
				cam.transform.position = Vector3.MoveTowards(cam.transform.position, yPivot.transform.position, zoomSensitivity);
			}
			if (Input.mouseScrollDelta.y < 0)
			{
				// Check if Cam to far from yPivot and move cam -towards yPivot
				if (Vector3.Distance(cam.transform.position, yPivot.transform.position) > maxCamOffset - 1) return;
				cam.transform.position = Vector3.MoveTowards(cam.transform.position, yPivot.transform.position, -zoomSensitivity);
			}

			if (Input.GetKeyUp(KeyCode.F))
			{
				// Check Raycast hitInfo and set yPivot-Pos to hitInfo-Pos
				ray = cam.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out hitInfo))
				{
					isHit = true;
				}
				else
				{
					isHit = false;
				}
			}
		}
		if (isHit)
		{
			targetFocus = new Vector3(hitInfo.collider.transform.position.x, targetFocus.y, hitInfo.collider.transform.position.z);
			yPivot.transform.position = targetFocus;
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

		yPivot.transform.position = new Vector3(xPos, yPivotOffset, zPos);

		cam.transform.localPosition = new Vector3(0f, yPos, 0f);
	}

	public void Focus()
	{
		yPivot.transform.position = new Vector3(target.position.x, yPivot.transform.position.y, target.position.z);
	}
}