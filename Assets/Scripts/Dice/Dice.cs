using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
	public bool Roll(out int face1, out int face2)
	{
		face1 = Random.Range(1, 6);
		face2 = Random.Range(1, 6);

		if (face1 == face2) return true;
		else return false;
	}
}
