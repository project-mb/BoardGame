using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Board : MonoBehaviour
{
	public GameObject tile;
	public GameObject cornerTile;
	public static bool bla;

	// Start is called before the first frame update
	void Awake()
	{
		Debug.Log("Instance Created");
	}

	public void GenerateNew(ushort width, ushort height)
	{

	}

	//TODO: Generate from Load-File (? .json)
}