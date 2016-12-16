using UnityEngine;
using System.Collections;

public class Camara_Movement : MonoBehaviour {
	public Vector3 rotate;

	void Start () {
		
	}

	void Update () {
		rotate.x = -Input.GetAxis ("Mouse Y");
	}
}