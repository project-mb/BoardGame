using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
	public static ushort programState = 0;

	void Start()
	{
		// TODO: Generate Board object and call generation method
	}

	void Update()
	{
		switch(programState)
		{
			default:
				Debug.Log("Invalid programState"); // TODO: Handle error
				break;
		}
	}
}
